using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Project._Scripts.Core.EventSystem
{
    [Serializable]
    public class CustomGameEvent : UnityEvent<Component, object>
    { }
}