using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShortCutter
{
    public class ObservableModel : INotifyPropertyChanged
    {
        /// <summary>
        /// left: property that is observed, right: if the left property changes his value is reevaluated.
        /// Example: DependencyProperties["StorageLocation"] = new string[] { "IsStoreplanVisible" };
        /// If "StorageLocation" changes, "IsStoreplanVisible" is reevaluated
        /// </summary>
        protected Dictionary<string, string[]> DependencyProperties;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableModel()
        {
            DependencyProperties = new Dictionary<string, string[]>();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            string[] depProps;
            if (DependencyProperties.TryGetValue(propertyName, out depProps))
            {
                foreach (var prop in depProps)
                {
                    OnPropertyChanged(prop);
                }
            }
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
