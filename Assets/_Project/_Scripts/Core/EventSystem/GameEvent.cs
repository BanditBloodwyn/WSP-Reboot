﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project._Scripts.Core.EventSystem
{
    public class GameEvent
    {
        private readonly List<Func<Component, object, object>> _actions = new();

        public object[] Invoke(Component sender, object inputData)
        {
            string logMessage = CreateLogMessage(sender);
            Debug.Log(logMessage);

            List<object> results = new List<object>();
           
            foreach (Func<Component, object, object> action in _actions)
                results.Add(action.Invoke(sender, inputData));

            return results.ToArray();
        }

        public void AddListener(Func<Component, object, object> listener)
        {
            if(!_actions.Contains(listener)) 
                _actions.Add(listener);
        }

        public void RemoveListener(Func<Component, object, object> listener)
        {
            if (_actions.Contains(listener))
                _actions.Remove(listener);
        }

        private string CreateLogMessage(Object sender)
        {
            StringBuilder sb = new();
            sb.AppendLine("<color=#FF7300>Event sent</color>");
            sb.AppendLine($"<b>Sender:</b>    {sender.name}");
            sb.AppendLine("<b>Responses:</b>");

            foreach (Func<Component, object, object> action in _actions)
                sb.AppendLine($"\t- {action.Method.DeclaringType?.Name}.<i>{action.Method.Name}()</i>");

            return sb.ToString();
        }
    }
}