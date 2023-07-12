using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project._Scripts.Core.EventSystem
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type of the sent data</typeparam>
    /// <typeparam name="U">Type of the response data</typeparam>
    public class TypedGameEvent<T,U>
    {
        private readonly string _name;
        private readonly List<Func<Component, T, U>> _actions = new();

        public TypedGameEvent(string name)
        {
            _name = name;
        }

        public U[] Invoke(Component sender, T inputData)
        {
            string logMessage = CreateLogMessage(sender, inputData);
            Debug.Log(logMessage);

            List<U> results = new List<U>();

            foreach (Func<Component, T, U> action in _actions)
                results.Add(action.Invoke(sender, inputData));

            return results.ToArray();
        }

        public void AddListener(Func<Component, T, U> listener)
        {
            if (!_actions.Contains(listener))
                _actions.Add(listener);
        }

        public void RemoveListener(Func<Component, T, U> listener)
        {
            if (_actions.Contains(listener))
                _actions.Remove(listener);
        }

        private string CreateLogMessage(Object sender, T inputData)
        {
            StringBuilder sb = new();
            sb.AppendLine($"<color=#FF7300>Event <b>\"{_name}\"</b> sent</color>");
            sb.AppendLine($"<b>Sender:</b>    {sender.name}");
            sb.AppendLine($"<b>Data:</b>      {inputData}");
            sb.AppendLine("<b>Responses:</b>");

            foreach (Func<Component, T, U> action in _actions)
                sb.AppendLine($"\t- {action.Method.DeclaringType?.Name}.<i>{action.Method.Name}()</i>");

            return sb.ToString();
        }
    }
}