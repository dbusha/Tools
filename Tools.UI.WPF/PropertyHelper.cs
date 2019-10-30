using System.ComponentModel;

namespace Tools.UI.WPF
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(object sender, params string[] propertyNames)
        {
            foreach (var name in propertyNames)
                PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(name));
        }
        

        protected void RaisePropertyChanged(params string[] propertyNames)
        { RaisePropertyChanged(this, propertyNames); }
    }
    
    
    public class PropertyHelper : PropertyChangedBase
    {
        protected void SetValue<T>(ref T backingField, T value, params string[] names)
        {
            if (backingField != null && backingField.Equals(value))
                return;
            backingField = value;
            RaisePropertyChanged(names);
        }
    }
}