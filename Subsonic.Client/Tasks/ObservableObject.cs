using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Subsonic.Client.Tasks
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            VerifyPropertyName(propertyName);

            if (PropertyChanged == null)
                return;

            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,
            // public, instance property on this object.

            if (GetType().GetRuntimeProperties().FirstOrDefault(p => p.Name == propertyName) != null)
                return;

            throw new Exception($"Invalid property name: {propertyName}");
        }
    }
}
