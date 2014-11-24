namespace RegiRide.Utils
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Reflection;

    using GalaSoft.MvvmLight;

    public class PropertyChangeAndChangingEventHandlerBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging = delegate { }; 
         
        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property which is changed.</param>
        public virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnPropertyChanging(string propertyName)
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.PropertyChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="RaisePropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyInfo">The property info of the property that has changed.</param>
        protected void RaisePropertyChanged(MemberInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }

            this.CheckPropertyInfo(propertyInfo);
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyInfo.Name));
        }

        [Conditional("DEBUG")]
        private void CheckPropertyInfo(MemberInfo propertyInfo)
        {
            if (propertyInfo.DeclaringType != null && !propertyInfo.DeclaringType.IsInstanceOfType(this))
            {
                throw new ArgumentException("The passed propertyInfo is from the wrong DeclaringType.", "propertyInfo");
            }
        }

    }
}
