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
    public class SqliteSettings : SqliteSettingsBase
    {
        public SqliteSettings(string section) : base(null, section, "settings.db") { }

        public SqliteSettings(string databasePath, string section) : base(databasePath, section, null) { }


        protected override void CreateDatabase_()
        {
            const string query = "CREATE TABLE IF NOT EXISTS Settings (id INTEGER, name VARCHAR, value VARCHAR, section VARCHAR, clr_type VARCHAR )";
            using (var cmd = new SqliteCommand(query, connection_)) {
                cmd.ExecuteNonQuery();
            }
        }


        protected override Dictionary<string, object> ReadSettings()
        {
            var settings = new Dictionary<string, object>();
            var query = "SELECT * FROM settings";
            using (SqliteCommand cmd = new SqliteCommand(query, connection_)) {
                var reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    var name = reader["name"].ToString();
                    var section = reader["section"]?.ToString();
                    var type = reader["clr_type"].ToString();
                    var value = GetValueAsType_(reader["value"].ToString(), type);
                    
                    if (!section.IsNullOrWhitespace())
                        name = $"{section}:{name}";
                    settings.Add(name, value);
                }
            }

            return settings;
        }

        protected override void WriteSettings(string name, object value)
        {
            var type = value.GetType();
            WriteSettings(name, value, type);
        }
       
        
        protected override void WriteSettings(string name, object value, Type type)
        {
            var sectionValue = section_.ValueOrDBNull();
            var existenceQuery = $"SELECT COUNT(*) FROM settings WHERE name='{name}' AND (section='{sectionValue}' OR section IS NULL)";
            bool doesSettingExist;
            using (var cmd = new SqliteCommand(existenceQuery, connection_)) {
                doesSettingExist = (long) cmd.ExecuteScalar() > 0;
            }

            var typeName = type.Name.ToLowerInvariant();
            var insertQuery = $"INSERT INTO settings(name, value, section, clr_type) VALUES ('{name}', '{value}', '{sectionValue}', '{typeName}')";
            var updateQuery = $"UPDATE settings SET value = '{value}' WHERE name='{name}' AND (section = '{sectionValue}' OR section IS NULL)";

            using (var insertCmd = new SqliteCommand {Connection = connection_}) {
                insertCmd.CommandText = doesSettingExist ?  updateQuery : insertQuery;
                insertCmd.ExecuteNonQuery();
            }
        }
    }
}