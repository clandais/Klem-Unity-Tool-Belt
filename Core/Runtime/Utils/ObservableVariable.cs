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
        /// Empty ctor
        /// </summary>
        public ObservableVariable()
        {
        }

        /// <summary>
        ///  Ctor
        /// </summary>
        /// <param name="value"></param>
        public ObservableVariable(T value)
        {
            Value = value;
        }

        /// <summary>
        ///  Ctor with callback
        /// </summary>
        /// <param name="value"></param>
        /// <param name="onValueChanged"></param>
        public ObservableVariable(T value, UnityAction<T, T> onValueChanged)
        {
            OnValueChanged = onValueChanged;
            Value = value;
        }



        public static implicit operator ObservableVariable<T>(T value) => new(value);

        public static implicit operator T(ObservableVariable<T> observableVariable) => observableVariable.Value;
    }
}