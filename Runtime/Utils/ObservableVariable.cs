#region

using System;
using UnityEngine;
using UnityEngine.Events;

#endregion

namespace Klem.Utils
{
    /// <summary>
    ///     A simple object that contains a value and can be observed for changes.
    /// </summary>
    /// <typeparam name="T">The type of the observed value</typeparam>
    [Serializable]
    public class ObservableVariable<T>
    {
        /// <summary>
        ///     The current value.
        /// </summary>
        [SerializeField] private T value;

        /// <summary>
        ///     The event that is invoked when the value changes.
        /// </summary>
        [NonSerialized] public UnityAction<T, T> OnValueChanged;


        /// <summary>
        ///     The current value.
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
        ///     Empty ctor
        /// </summary>
        public ObservableVariable()
        {
        }

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="value"></param>
        public ObservableVariable(T value)
        {
            Value = value;
        }

        /// <summary>
        ///     Ctor with callback
        /// </summary>
        /// <param name="value"></param>
        /// <param name="onValueChanged"></param>
        public ObservableVariable(T value, UnityAction<T, T> onValueChanged)
        {
            OnValueChanged = onValueChanged;
            Value = value;
        }
    }


    /// <summary>
    ///     A simple object that contains a two values and can be observed for changes.
    /// </summary>
    /// <typeparam name="T0"></typeparam>
    /// <typeparam name="T1"></typeparam>
    [Serializable]
    public class ObservableVariable<T0, T1> : ObservableVariable<T0>
    {
        /// <summary>
        ///     The current value.
        /// </summary>
        [SerializeField] private T1 value1;

        /// <summary>
        ///     The event that is invoked when the value changes.
        /// </summary>
        [NonSerialized] public UnityAction<T1, T1> OnValueChanged1;

        public ObservableVariable(T0 value0, T1 value1) : base(value0)
        {
            this.value1 = value1;
        }

        public T1 Value1
        {
            get => value1;
            set
            {
                var previous = value1;
                value1 = value;
                OnValueChanged1?.Invoke(previous, value1);
            }
        }
    }
}