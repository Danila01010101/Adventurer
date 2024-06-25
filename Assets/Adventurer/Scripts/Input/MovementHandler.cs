using System;
using UnityEngine;

namespace Adventurer
{
    public class MovementHandler : IDisposable
    {
        private IInput input;

        public MovementHandler(IInput input)
        {
            this.input = input;

            Debug.Log(input.GetType() + " подключён!");

            input.ClickDown += OnClickDown;
            input.ClickUp += OnClickUp;
            input.Drag += OnDrag;
        }

        private void OnClickDown(Vector3 position)
        {
            Debug.Log("ClickDown");
        }

        private void OnClickUp(Vector3 position)
        {
            Debug.Log("ClickUp");
        }

        private void OnDrag(Vector3 position)
        {
            Debug.Log("Drag");
        }

        public void Dispose()
        {

        }
    }
}