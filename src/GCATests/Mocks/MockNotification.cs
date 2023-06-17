using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using PluginManager.Abstractions;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    public sealed class MockNotification : INotificationService
    {
        private readonly List<INotificationListener> _registeredListeners = new();
        private readonly Dictionary<string, List<object>> _events = new();

        public List<INotificationListener> Listeners => _registeredListeners;

        public Dictionary<string, List<object>> Events => _events;

        public bool RaiseEvent(in string eventId, in object param1, in object param2, ref object result)
        {
            AddEventToDictionary(eventId, param1);
            return true;
        }

        public void RaiseEvent(in string eventId, in object param1, in object param2)
        {
            AddEventToDictionary(eventId, param1);
        }

        public void RaiseEvent(in string eventId, in object param1)
        {
            AddEventToDictionary(eventId, param1);
        }

        public void RaiseEvent(in string eventId)
        {
            AddEventToDictionary(eventId, null);
        }

        public bool RegisterListener(in INotificationListener listener)
        {
            _registeredListeners.Add(listener);
            return _registeredListeners.Contains(listener);
        }

        public bool UnregisterListener(in INotificationListener listener)
        {
            _registeredListeners.Remove(listener);
            return !_registeredListeners.Contains(listener);
        }

        private void AddEventToDictionary(string eventName, in object param1)
        {
            if (!_events.ContainsKey(eventName))
            {
                _events[eventName] = new List<object>();
            }

            _events[eventName].Add(param1);
        }
    }
}
