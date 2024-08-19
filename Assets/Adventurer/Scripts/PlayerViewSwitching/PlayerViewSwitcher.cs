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

        [Header("FirstPersonView")]
		private IPlayerView firstPersonView;

        private ViewType currentView;
        private bool isInitialized = false;

        public static Action PlayerViewInitialized;

        public enum ViewType { FPV, TPV }

        [Inject]
        private void Construct(FirstPersonPlayer firstPersonView, ThirdViewPlayer thirdPersonView, IItemHandler itemHandler)
        {
            this.firstPersonView = firstPersonView;
            this.thirdPersonView = thirdPersonView;

            thirdPersonView.ChangeToFirstPersonView();
            firstPersonView.ChangeToFirstPersonView();

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
            switch (viewType)
            {
                case ViewType.FPV:
                    currentView = viewType;
                    thirdPersonView.ChangeToFirstPersonView();
                    break;
                case ViewType.TPV:
                    currentView = viewType;
                    thirdPersonView.ChangeToThirdPersonView();
                    break;
                default:
                    throw new ArgumentException("This player view is not registered.");
            }
        }

        private void SwitchState(InputAction.CallbackContext context)
        {
            if (thirdPersonView.IsActive)
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