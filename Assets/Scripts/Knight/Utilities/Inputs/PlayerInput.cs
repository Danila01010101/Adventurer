using Adventurer;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GenshinImpactMovementSystem
{
    public class PlayerInput
    {
        public PlayerInputActions InputActions { get; private set; }
        public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

        private CoroutineStarter coroutineStarter;

        [Inject]
        public PlayerInput(CoroutineStarter coroutineStarter)
        {
            this.coroutineStarter = coroutineStarter;

            InputActions = new PlayerInputActions();

            PlayerActions = InputActions.Player;

            InputActions.Enable();
        }

        private void OnEnable()
        {
            InputActions.Enable();
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }

        public void DisableActionFor(InputAction action, float seconds)
        {
            coroutineStarter.StartCoroutine(DisableAction(action, seconds));
        }

        private IEnumerator DisableAction(InputAction action, float seconds)
        {
            action.Disable();

            yield return new WaitForSeconds(seconds);

            action.Enable();
        }
    }
}