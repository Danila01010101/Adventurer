using EvolveGames;
using GenshinImpactMovementSystem;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Adventurer
{
    public class PlayerViewSwitcher : IDisposable
	{
		[Header("ThirdPersonView")]
        private ThirdViewPlayer thirdPersonView;
        private FirstPersonPlayer firstPersonView;

        private ViewType currentView;
        private bool isInitialized = false;

        public static Action PlayerViewInitialized;

        public enum ViewType { FPV, TPV }

        [Inject]
        private void Construct(ThirdViewPlayer thirdPersonView, FirstPersonPlayer firstPersonView, IItemHandler itemHandler)
        {
            this.thirdPersonView = thirdPersonView;
            this.firstPersonView = firstPersonView;

            thirdPersonView.ChangeToFirstPersonView();

            if (itemHandler.GetCurrentItemType() == ItemType.Gun)
            {
                SetView(ViewType.FPV);
            }
            else
            {
                SetView(ViewType.TPV);
            }

            thirdPersonView.Input.PlayerActions.ViewSwitch.started += SwitchState;
            isInitialized = true;
            PlayerViewInitialized?.Invoke();
        }

        public void SetView(ViewType viewType)
        {
            Cursor.lockState = CursorLockMode.Locked;
            switch (viewType)
            {
                case ViewType.FPV:
                    currentView = viewType;
                    firstPersonView.enabled = true;
                    thirdPersonView.ChangeToFirstPersonView();
                    break;
                case ViewType.TPV:
                    currentView = viewType;
                    firstPersonView.enabled = false;
                    thirdPersonView.ChangeToThirdPersonView();
                    break;
                default:
                    throw new ArgumentException("This player view is not registered.");
            }
        }

        private void SwitchState(InputAction.CallbackContext context)
        {
            if (thirdPersonView.IsThirdViewActive)
            {
                SetView(ViewType.FPV);
            }
            else
            {
                SetView(ViewType.TPV);
            }
        }

        public void Dispose()
        {
            thirdPersonView.Input.PlayerActions.ViewSwitch.started -= SwitchState;
        }
    }
}