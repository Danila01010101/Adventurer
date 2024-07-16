using EvolveGames;
using GenshinImpactMovementSystem;
using UnityEngine;
using Zenject;

namespace Adventurer
{
    public class PlayerViewSwitcher : ITickable
	{
		[Header("ThirdPersonView")]
		private Player player;
		private PlayerInput input;

        [Header("FirstPersonView")]
		private PlayerController firstPersonViewController;

		[Inject]
		private void Construct(Player player, PlayerController firstPersonViewController)
		{
            this.firstPersonViewController = firstPersonViewController;
			this.player = player;
			input = player.Input;
        }

		public void Tick()
        {

        }
    }
}