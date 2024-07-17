using System;
using UnityEngine;
using Zenject;

namespace Adventurer
{
    public class PlayerViewSwitcher : ITickable
	{
		[Header("ThirdPersonView")]
        private IPlayerView thirdPersonView;

        [Header("FirstPersonView")]
		private IPlayerView firstPersonView;

        private ViewType currentView;

        public static Action PlayerViewInitialized;

        public enum ViewType { FPV, TPV }

        public PlayerViewSwitcher(IPlayerView firstPersonView, IPlayerView thirdPersonView)
        {
            this.firstPersonView = firstPersonView;
            this.thirdPersonView = thirdPersonView;

            if (firstPersonView.IsActive == true)
                firstPersonView.Deactivate();

            if (thirdPersonView.IsActive == true)
                thirdPersonView.Deactivate();
        }

        [Inject]
        private void Construct(IItemHandler itemHandler)
        {
            if (itemHandler.GetCurrentItemType() == ItemType.Gun)
                SetView(ViewType.FPV);
            else
                SetView(ViewType.TPV);
            PlayerViewInitialized?.Invoke();
        }

        public void SetView(ViewType viewType)
        {
            if (currentView == viewType)
                return;

            switch (viewType)
            {
                case ViewType.FPV:
                    currentView = viewType;
                    thirdPersonView.Deactivate();
                    firstPersonView.Activate();
                    break;
                case ViewType.TPV:
                    currentView = viewType;
                    firstPersonView.Deactivate();
                    thirdPersonView.Activate();
                    break;
                default:
                    throw new ArgumentException("This player view is not registered.");
            }
        }

		public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {

            }
        }
    }
}