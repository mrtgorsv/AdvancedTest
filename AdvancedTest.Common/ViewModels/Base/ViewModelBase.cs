using System;
using System.ComponentModel;

namespace AdvancedTest.Common.ViewModels.Base
{
    /// <summary>
    /// Базовый класс для всех моделей представления
    /// </summary>
    [Serializable]
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
