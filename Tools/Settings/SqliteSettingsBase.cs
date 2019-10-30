using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Sqlite;
using NLog;
using Tools.ExtensionMethods;

namespace Tools.Settings
{
    public abstract class SqliteSettingsBase
    {
        private Dictionary<string, object> settings_ = new Dictionary<string, object>();
        private readonly string databasePath_;
        
        protected readonly Logger logger_ = LogManager.GetCurrentClassLogger();
        protected SqliteConnection connection_;
        protected readonly string section_;
        
        
        protected SqliteSettingsBase(string section) : this(null, section) {}
        protected SqliteSettingsBase(string section, string databaseName) : this(null, section, databaseName) {}
        
        
        protected SqliteSettingsBase(string databasePath, string section, string databaseName = "settings.db")
        {
            databasePath_ = databasePath;
            section_ = section;
            if (databasePath == null) {
                var location = Assembly.GetEntryAssembly()?.Location ?? Path.GetTempPath();
                databasePath_ = Path.Combine(new FileInfo(location).DirectoryName, databaseName);
            }

            ConfigureDatabase();
            WriteDefaultValues_();
        }


        protected object this[string name]
        {
            get {
                var fullName = GetFullName_(name);
                return settings_.ContainsKey(fullName) ? settings_[fullName] : null;
            }
            set {
                WriteSettings(name, value);
                var fullName = GetFullName_(name);
                settings_[fullName] = value;
            }
        }


        private string GetFullName_(string name) => section_.IsNullOrWhitespace() ? name : $"{section_}:{name}";
        
        
        private void ConfigureDatabase()
        {
            var cxnBuilder = new SqliteConnectionStringBuilder { DataSource = databasePath_ };
            connection_ = new SqliteConnection(cxnBuilder.ToString());
            connection_.Open();
            logger_.Debug($"Data source: {connection_.DataSource}");
            CreateDatabase_();
            settings_ = ReadSettings();
        }
        
        
        private void WriteDefaultValues_()
        {
           foreach (var property in GetType().GetProperties().Where(p => p.GetAccessors().Any(a => a.IsPublic))) 
           {
               if (this[property.Name] != null) 
                   continue;
               foreach (var att in property.GetCustomAttributes(typeof(DefaultValueAttribute), false)) {
                   var defaultValue = ((DefaultValueAttribute) att);
                   WriteSettings(property.Name, defaultValue.Value, defaultValue.Value.GetType());
               }
           }
        }


        protected abstract void CreateDatabase_();
        protected abstract Dictionary<string, object> ReadSettings();
        protected abstract void WriteSettings(string name, object value);
        protected abstract void WriteSettings(string name, object value, Type type);
       
        
        protected object GetValueAsType_(string value, string type)
        {
            switch (type.ToLowerInvariant()) {
                case "int32": return Convert.ToInt32(value);
                case "int64": return Convert.ToInt64(value);
                case "boolean": return Convert.ToBoolean(value);
                case "decimal": return Convert.ToDecimal(value);
                case "datetime": return Convert.ToDateTime(value);
                default: return value;
            } 
        }
    }
}