using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpaceWarsHex.Interfaces;
using SpaceWarsHex.Interfaces.Lobby;
using SpaceWarsHex.Serialization;
using System;
using System.Collections.Generic;

namespace SpaceWarsHex.Bridges
{
    public static class DependencyFactory
    {
        private enum Lifestyle
        {
            Singleton = 0,
            Transient = 1,
        }

        private class Component (Func<object> factory, Lifestyle lifestyle = Lifestyle.Singleton)
        {
            private object? _instance;
            public Func<object> Factory { get; } = factory;
            public Lifestyle Lifestyle { get; } = lifestyle;
            
            public object Instantiate()
            {
                if (Lifestyle == Lifestyle.Transient)
                {
                    return Factory();
                } else {
                    _instance ??= Factory();
                    return _instance;
                }
            }
        }

        private static readonly Dictionary<Type, Component> _components = [];

        public static T Create<T>()
        {
            if (_components.TryGetValue(typeof(T), out var component))
            {
                return (T)component.Instantiate();
            }
            else
            {
                throw new KeyNotFoundException(typeof(T).ToString());
            }
        }

        private static void Register<T>(Func<object> factory, Lifestyle lifestyle = Lifestyle.Singleton)
        {
            _components[typeof(T)] = new Component(factory, lifestyle);
        }

        private static void RegisterSerialization()
        {
            Register<JsonSerializer>(() =>
            {
                var serializer = new JsonSerializer();
                serializer.Converters.Add(new StringEnumConverter());
                return serializer;
            }, Lifestyle.Singleton);
            Register<IPrototypeSerializer>(() => new PrototypeSerializer(Create<JsonSerializer>()));
            Register<IPrototypeCache>(() => new PrototypeDatabase());
        }

        private static void RegisterLobby()
        {
            Register<ILobby>(() => new Lobby.Lobby(Create<IPrototypeSerializer>(), Create<IPrototypeCache>()));
        }

        static DependencyFactory()
        {
            RegisterSerialization();
            RegisterLobby();
        }
    }
}
