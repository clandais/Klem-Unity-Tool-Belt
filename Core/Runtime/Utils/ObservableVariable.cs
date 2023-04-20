using System;
using UnityEngine;
using UnityEngine.Events;

namespace Klem.Core.Utils
{
    /// <summary>
    /// A simple object that contains a value and can be observed for changes.
    /// </summary>
    /// <typeparam name="T">The type of the observed value</typeparam>
    [Serializable]
    public class ObservableVariable<T>
    {
        /// <summary>
        /// The current value.
        /// </summary>
        [SerializeField] private T value;

        /// <summary>
        /// The event that is invoked when the value changes.
        /// </summary>
        [NonSerialized] public UnityAction<T, T> OnValueChanged;

        
        /// <summary>
        ///  The current value.
        /// </summary>
        public T Value
        {
            get => value;
            set
            {
                var previous = this.value;
                this.value = value;
                OnValueChanged?.Invoke(previous, this.value);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public ObservableVariable(T value)
        {
            this.value = value;
        }

        public ObservableVariable()
        {

        }
    }
}