using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace StubbUnity.Unity.Utils
{
    public static class EventBus
    {
        private static readonly Dictionary<IEventChannelGeneric, IEventChannelGeneric> Channels = new Dictionary<IEventChannelGeneric, IEventChannelGeneric>(10);

        public static void Register(IEventChannelGeneric channel)
        {
            if (Channels.ContainsKey(channel)) return;
            
            Channels.Add(channel, channel);
        }
        
        public static void Unregister(IEventChannelGeneric channel)
        {
            if (Channels.TryGetValue(channel, out var genericChannel))
            {
                Channels.Remove(genericChannel);
            }
        }
        
        public static void ClearAll(bool purgePools = false)
        {
            foreach (var channel in Channels)
            {
                channel.Value.Clear(purgePools);
            }
        }

        public static void DisposeALl()
        {
            foreach (var channel in Channels)
            {
                channel.Value.Dispose();
            }
        }
    }
    
    public static class EventBus<T>
    {
        private static IEventChannel<T> _instance;
        
        public static int NumListeners => _IsChannelExist() ? _instance.Count : 0; 

        public static void AddListener(Action<T> listener)
        {
            _GetChannel().Add(listener);
        }

        public static void Dispatch(T t)
        {
            if (_IsChannelExist())
                _instance.Dispatch(ref t);
        }

        public static void RemoveListener(Action<T> listener)
        {
            if (_IsChannelExist())
                _instance.Remove(listener);
        }

        public static void Clear(bool purgePool = false)
        {
            if (_IsChannelExist())
                _instance.Clear(purgePool);    
        }

        public static void Dispose()
        {
            if (_IsChannelExist())
            {
                EventBus.Unregister(_instance);
                _instance.Dispose();
                _instance = null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool _IsChannelExist() => _instance != null && !_instance.IsDisposed;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEventChannel<T> _GetChannel()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new EventChannel<T>();
                EventBus.Register(_instance);
            }

            return _instance;
        } 
    }

    internal class EventChannel<T> : IEventChannel<T>
    {
        private LinkedList<Action<T>> _handlers = new LinkedList<Action<T>>();
        private Stack<LinkedListNode<Action<T>>> _poolNodes = new Stack<LinkedListNode<Action<T>>>(5);
        private Dictionary<Action<T>, LinkedListNode<Action<T>>> _mapHandlerToNode = new Dictionary<Action<T>, LinkedListNode<Action<T>>>(5);

        public int Count => _handlers?.Count ?? 0;
        public bool IsDisposed => _handlers == null;
        
        public void Add(Action<T> handler)
        {
            if (_mapHandlerToNode.ContainsKey(handler)) return;

            var node = _poolNodes.Count > 0 ? _poolNodes.Pop() : new LinkedListNode<Action<T>>(handler);
            node.Value = handler;
            _handlers.AddLast(node);
            _mapHandlerToNode.Add(handler, node);
        }

        public void Remove(Action<T> handler)
        {
            if (_mapHandlerToNode.TryGetValue(handler, out var node))
            {
                _handlers.Remove(node);
                _mapHandlerToNode.Remove(handler);
            }
        }

        public bool Has(Action<T> handler)
        {
            return _mapHandlerToNode.ContainsKey(handler);
        }

        public void Clear(bool purgePool = false)
        {
            _handlers.Clear();
            _mapHandlerToNode.Clear();
            
            if (purgePool)
                _poolNodes.Clear();
        }

        public void Dispatch(ref T t)
        {
            var node = _handlers.First;
            
            while (node != null)
            {
                var current = node;
                node = node.Next;
                
                current.Value.Invoke(t);
            }
        }
        
        public void Dispose()
        {
            Clear(true);

            _handlers = null;
            _poolNodes = null;
            _mapHandlerToNode = null;
        }
    }
    
    internal interface IEventChannel<T> : IEventChannelGeneric
    {
        void Add(Action<T> handler);
        void Remove(Action<T> handler);
        void Dispatch(ref T t);
        bool Has(Action<T> handler);
    }

    public interface IEventChannelGeneric
    {
        int Count { get; }
        void Clear(bool purgePool = false);
        void Dispose();
        bool IsDisposed { get; }
    }
}