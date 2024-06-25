using System;
using UnityEngine;
using Zenject;

namespace Adventurer
{
    public class DesctopInput : IInput, ITickable
    {
        public event Action<Vector3> ClickDown;
        public event Action<Vector3> ClickUp;
        public event Action<Vector3> Drag;

        public const int LeftMouseButton = 0;

        private Vector3 previousMousePosition;
        private bool isSwiping;

        public void Tick()
        {
            ProcessClickDown();
            ProcessClickUp();
            ProcessSwipe();
        }

        private void ProcessSwipe()
        {
            if (isSwiping == false)
                return;

            if (Input.mousePosition != previousMousePosition)
                Drag?.Invoke(Input.mousePosition);

            previousMousePosition = Input.mousePosition;
        }

        private void ProcessClickDown()
        {
            if (Input.GetMouseButtonDown(LeftMouseButton))
            {
                isSwiping = true;
                ClickUp?.Invoke(Input.mousePosition);
            }
        }

        private void ProcessClickUp()
        {
            if (Input.GetMouseButtonUp(LeftMouseButton))
            {
                isSwiping = false;
                ClickUp?.Invoke(Input.mousePosition);
            }
        }
    }
}