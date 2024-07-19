using EvolveGames;
using System;
using Zenject;

namespace Adventurer
{
    public class PlayerUIMediator : IDisposable
	{
        private FirstPersonPlayer playerController;
        private ItemChange itemChange;

        [Inject]
        private void Construct(ItemChange itemChange, MENU menu, 
            FirstPersonPlayer playerController, IHandAnimatable handAnimatable)
        {
            this.playerController = playerController;
            this.itemChange = itemChange;
            Subscribe();
        }

        private void Subscribe()
        {
            playerController.ItemHide += itemChange.Hide;
            MENU.GamePaused += playerController.EnterPause;
            MENU.GameStarted += playerController.ExitPause;
        }

        public void Dispose()
        {
            playerController.ItemHide -= itemChange.Hide;
            MENU.GamePaused -= playerController.EnterPause;
            MENU.GameStarted -= playerController.ExitPause;
        }
	}
}