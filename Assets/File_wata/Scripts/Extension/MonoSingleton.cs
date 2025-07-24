using System;
using UnityEngine;

namespace Wata.Extension {
    public abstract class MonoSingleton<T> : MonoBehaviour 
        where T: MonoBehaviour
    {
        public static T Instance { get; private set; }
        /// <summary>
        /// Do you use this single on just one scene?
        /// </summary>
        protected abstract bool IsNarrowSingleton { set; get; } 
        
        public void Awake() {
            
            if (Instance != null) {

                if (Instance != this as T) {

                    Debug.Log(
                        $"{gameObject.name} was deleted,\n" 
                        + "singleton can exist just one\n"
                        + $"(Current Singleton: {Instance.gameObject})"
                    );
                    Destroy(gameObject);
                }
                       
                return;
            }

            var type = GetType();
            if (type != typeof(T))
                throw new ArgumentException($"T must be {type}");
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}