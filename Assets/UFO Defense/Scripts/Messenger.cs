// Messenger.cs v1.0 by Magnus Wolffelt, magnus.wolffelt@gmail.com
// Version 1.4 by Julie Iaccarino, biscuitWizard @ github.com
//
// Inspired by and based on Rod Hyde's Messenger:
// http://www.unifycommunity.com/wiki/index.php?title=CSharpMessenger
//
// This is a C# messenger (notification center). It uses delegates
// and generics to provide type-checked messaging between event producers and
// event consumers, without the need for producers or consumers to be aware of
// each other. The major improvement from Hyde's implementation is that
// there is more extensive error detection, preventing silent bugs.
//
// Usage example:
// Messenger<float>.AddListener("myEvent", MyEventHandler);
// ...
// Messenger<float>.Broadcast("myEvent", 1.0f);
//
// Callback example:
// Messenger<float>.AddListener<string>("myEvent", MyEventHandler);
// private string MyEventHandler(float f1) { return "Test " + f1; }
// ...
// Messenger<float>.Broadcast<string>("myEvent", 1.0f, MyEventCallback);
// private void MyEventCallback(string s1) { Debug.Log(s1"); }
//
// If preferred, change DEFAULT_MODE to not require listeners.

using System;
using System.Collections.Generic;
using System.Linq;

namespace UFO_Defense.Scripts
{
    public enum MessengerMode
    {
        DontRequireListener,
        RequireListener,
    }

    internal static class MessengerInternal
    {
        private static readonly Dictionary<string, Delegate> EventTable = new();
        public const MessengerMode DefaultMode = MessengerMode.RequireListener;

        public static void AddListener(string eventType, Delegate callback)
        {
            OnListenerAdding(eventType, callback);
            EventTable[eventType] = Delegate.Combine(EventTable[eventType], callback);
        }

        public static void RemoveListener(string eventType, Delegate handler)
        {
            OnListenerRemoving(eventType, handler);
            EventTable[eventType] = Delegate.Remove(EventTable[eventType], handler);
            OnListenerRemoved(eventType);
        }

        public static T[] GetInvocationList<T>(string eventType)
        {
            Delegate d;
            if (EventTable.TryGetValue(eventType, out d))
            {
                try
                {
                    return d.GetInvocationList().Cast<T>().ToArray();
                }
                catch
                {
                    throw CreateBroadcastSignatureException(eventType);
                }
            }

            return Array.Empty<T>();
        }

        private static void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
        {
            if (!EventTable.ContainsKey(eventType))
            {
                EventTable.Add(eventType, null);
            }

            var d = EventTable[eventType];
            if (d != null && d.GetType() != listenerBeingAdded.GetType())
            {
                throw new ListenerException(string.Format(
                    "Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}",
                    eventType, d.GetType().Name, listenerBeingAdded.GetType().Name));
            }
        }

        private static void OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
        {
            if (EventTable.ContainsKey(eventType))
            {
                var d = EventTable[eventType];

                if (d == null)
                {
                    throw new ListenerException(
                        $"Attempting to remove listener with for event type {eventType} but current listener is null.");
                }
                else if (d.GetType() != listenerBeingRemoved.GetType())
                {
                    throw new ListenerException(
                        $"Attempting to remove listener with inconsistent signature for event type {eventType}. Current listeners have type {d.GetType().Name} and listener being removed has type {listenerBeingRemoved.GetType().Name}");
                }
            }
            else
            {
                throw new ListenerException(
                    $"Attempting to remove listener for type {eventType} but Messenger doesn't know about this event type.");
            }
        }

        private static void OnListenerRemoved(string eventType)
        {
            if (EventTable[eventType] == null)
            {
                EventTable.Remove(eventType);
            }
        }

        public static void OnBroadcasting(string eventType, MessengerMode mode)
        {
            if (mode == MessengerMode.RequireListener && !EventTable.ContainsKey(eventType))
            {
                throw new BroadcastException(
                    $"Broadcasting message {eventType} but no listener found.");
            }
        }

        private static BroadcastException CreateBroadcastSignatureException(string eventType)
        {
            return new BroadcastException(
                $"Broadcasting message {eventType} but listeners have a different signature than the broadcaster.");
        }

        private class BroadcastException : Exception
        {
            public BroadcastException(string msg) : base(msg)
            {
            }
        }

        private class ListenerException : Exception
        {
            public ListenerException(string msg) : base(msg)
            {
            }
        }
    }

// No parameters
    public static class Messenger
    {
        public static void AddListener(string eventType, Action handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public static void AddListener<TReturn>(string eventType, Func<TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public static void RemoveListener(string eventType, Action handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public static void RemoveListener<TReturn>(string eventType, Func<TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public static void Broadcast(string eventType)
        {
            Broadcast(eventType, MessengerInternal.DefaultMode);
        }

        public static void Broadcast<TReturn>(string eventType, Action<TReturn> returnCall)
        {
            Broadcast(eventType, returnCall, MessengerInternal.DefaultMode);
        }

        public static void Broadcast(string eventType, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action>(eventType);

            foreach (var callback in invocationList)
                callback.Invoke();
        }

        public static void Broadcast<TReturn>(string eventType, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<TReturn>>(eventType);

            foreach (var result in invocationList.Select(del => del.Invoke()))
            {
                returnCall.Invoke(result);
            }
        }
    }

// One parameter
    public static class Messenger<T>
    {
        public static void AddListener(string eventType, Action<T> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public static void AddListener<TReturn>(string eventType, Func<T, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public static void RemoveListener(string eventType, Action<T> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public static void RemoveListener<TReturn>(string eventType, Func<T, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public static void Broadcast(string eventType, T arg1)
        {
            Broadcast(eventType, arg1, MessengerInternal.DefaultMode);
        }

        public static void Broadcast<TReturn>(string eventType, T arg1, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, returnCall, MessengerInternal.DefaultMode);
        }

        public static void Broadcast(string eventType, T arg1, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T>>(eventType);

            foreach (var callback in invocationList)
                callback.Invoke(arg1);
        }

        public static void Broadcast<TReturn>(string eventType, T arg1, Action<TReturn> returnCall, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, TReturn>>(eventType);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1)))
            {
                returnCall.Invoke(result);
            }
        }
    }


// Two parameters
    public static class Messenger<T, U>
    {
        public static void AddListener(string eventType, Action<T, U> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public static void AddListener<TReturn>(string eventType, Func<T, U, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public static void RemoveListener(string eventType, Action<T, U> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public static void RemoveListener<TReturn>(string eventType, Func<T, U, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public static void Broadcast(string eventType, T arg1, U arg2)
        {
            Broadcast(eventType, arg1, arg2, MessengerInternal.DefaultMode);
        }

        public static void Broadcast<TReturn>(string eventType, T arg1, U arg2, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, arg2, returnCall, MessengerInternal.DefaultMode);
        }

        public static void Broadcast(string eventType, T arg1, U arg2, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T, U>>(eventType);

            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2);
        }

        public static void Broadcast<TReturn>(string eventType, T arg1, U arg2, Action<TReturn> returnCall,
            MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, U, TReturn>>(eventType);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2)))
            {
                returnCall.Invoke(result);
            }
        }
    }


// Three parameters
    public static class Messenger<T, U, V>
    {
        public static void AddListener(string eventType, Action<T, U, V> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public static void AddListener<TReturn>(string eventType, Func<T, U, V, TReturn> handler)
        {
            MessengerInternal.AddListener(eventType, handler);
        }

        public static void RemoveListener(string eventType, Action<T, U, V> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public static void RemoveListener<TReturn>(string eventType, Func<T, U, V, TReturn> handler)
        {
            MessengerInternal.RemoveListener(eventType, handler);
        }

        public static void Broadcast(string eventType, T arg1, U arg2, V arg3)
        {
            Broadcast(eventType, arg1, arg2, arg3, MessengerInternal.DefaultMode);
        }

        public static void Broadcast<TReturn>(string eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall)
        {
            Broadcast(eventType, arg1, arg2, arg3, returnCall, MessengerInternal.DefaultMode);
        }

        public static void Broadcast(string eventType, T arg1, U arg2, V arg3, MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Action<T, U, V>>(eventType);

            foreach (var callback in invocationList)
                callback.Invoke(arg1, arg2, arg3);
        }

        public static void Broadcast<TReturn>(string eventType, T arg1, U arg2, V arg3, Action<TReturn> returnCall,
            MessengerMode mode)
        {
            MessengerInternal.OnBroadcasting(eventType, mode);
            var invocationList = MessengerInternal.GetInvocationList<Func<T, U, V, TReturn>>(eventType);

            foreach (var result in invocationList.Select(del => del.Invoke(arg1, arg2, arg3)))
            {
                returnCall.Invoke(result);
            }
        }
    }
}