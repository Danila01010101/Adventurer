using EvolveGames;
using GenshinImpactMovementSystem;
using System;
using Unity.VisualScripting;
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
        private bool isInitialized = false;

        public static Action PlayerViewInitialized;

        public enum ViewType { FPV, TPV }

        [Inject]
        private void Construct(FirstPersonPlayer firstPersonView, ThirdViewPlayer thirdPersonView, IItemHandler itemHandler)
        {
            this.firstPersonView = firstPersonView;
            this.thirdPersonView = thirdPersonView;

            thirdPersonView.Deactivate();
            firstPersonView.Deactivate();

            if (itemHandler.GetCurrentItemType() == ItemType.Gun)
            {
                SetView(ViewType.FPV);
            }
            else
            {
                SetView(ViewType.TPV);
            }

            isInitialized = true;
            PlayerViewInitialized?.Invoke();
        }

        public void SetView(ViewType viewType)
        {
            if (isInitialized == true && currentView == viewType)
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
            //добавить смену вида по нажатию клавиши для теста
        }
    }
}