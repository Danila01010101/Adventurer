using System;
using UnityEngine;

namespace Adventurer
{
    public interface IInput
    {
        event Action<Vector3> ClickDown;
        event Action<Vector3> ClickUp;
        event Action<Vector3> Drag;
    }
}