﻿using System.Runtime.CompilerServices;

namespace TWYK.Core.Infrastructure
{
    public class EngineContext
    {
        /// <summary>
        /// Initializes a static instance of the AthleticShop factory.
        /// </summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate) {
            if (Singleton<IEngine>.Instance == null || forceRecreate) {
                Singleton<IEngine>.Instance = new AtsEngine();
                Singleton<IEngine>.Instance.Initialize();
            }

            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine) {
            Singleton<IEngine>.Instance = engine;
        }

        /// <summary>
        /// Gets the singleton AthleticShop engine used to access AthleticShop services.
        /// </summary>
        public static IEngine Current {
            get {
                if (Singleton<IEngine>.Instance == null) Initialize(false);
                return Singleton<IEngine>.Instance;
            }
        }
    }
}