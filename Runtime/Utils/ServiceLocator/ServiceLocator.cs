using System;
using System.Collections.Generic;
using UnityEngine;

namespace Klem.Utils.ServiceLocator
{
   /// <summary>
    /// A simple service locator for <see cref="IGameService"/>
    /// </summary>
    public static class ServiceLocator
   {


       /// <summary>
       /// Currently Registered services
       /// </summary>
       private static readonly Dictionary<string, IGameService> Services = new Dictionary<string, IGameService>();
        
        
        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance</returns>
        /// <exception cref="InvalidOperationException">If the service is not found.</exception>
        public static T Get<T>() where T : IGameService
        {
            var key = typeof(T).Name;
            if (Services.TryGetValue(key, out var service))
            {
                return (T) service;
            }

            Debug.LogError($"{key} is not registered with ServiceLocator.");
            throw new InvalidOperationException();
        }

        public static bool TryGet<T>(out T service) where T : IGameService
        {
            var key = typeof(T).Name;
            if (Services.TryGetValue(key, out var result))
            {
                service = (T) result;
                return true;
            }
            service = default;
            return false;
        }

        /// <summary>
        /// Registers a service instance.
        /// </summary>
        /// <typeparam name="T"> Service type </typeparam>
        /// <param name="service"> Service instance </param>
        public static void Register<T>(T service) where T : IGameService
        {
            var key = typeof(T).Name;
            if (Services.ContainsKey(key))
            {
                Debug.LogError($"{key} is already registered with ServiceLocator.");
                throw new InvalidOperationException();
            }

            Services.Add(key, service);
        }
        
        /// <summary>
        /// Unregisters a service instance.
        ///  </summary>
        ///  <typeparam name="T"> Service type </typeparam>
        ///  <param name="service"> Service instance </param>
        ///  <exception cref="InvalidOperationException">If the service is not found.</exception>
        public static void Unregister<T>(T service) where T : IGameService
        {
            var key = typeof(T).Name;
            if (!Services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister {key} but it is not registered with ServiceLocator.");
                throw new InvalidOperationException();
            }
            
            Services.Remove(key);
        }
    }
}