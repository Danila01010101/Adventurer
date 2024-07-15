using EvolveGames;
using GenshinImpactMovementSystem;
using UnityEngine;
using Zenject;

namespace Adventurer
{
    public class PlayerViewStateMachine : StateMachine, ITickable
	{
		[Header("ThirdPersonView")]
		private Player player;
		private PlayerInput input;

        [Header("FirstPersonView")]
		private PlayerController firstPersonViewController;

		private FirstViewState firstViewState;
		private ThirdViewState thirdViewState;

		[Inject]
		private void Construct(Player player, PlayerController firstPersonViewController)
		{
            this.firstPersonViewController = firstPersonViewController;
			this.player = player;
			input = player.Input;
            firstViewState = new FirstViewState(this);
            thirdViewState = new ThirdViewState(this);
        }

		public void Tick()
        {
			Update();
			HandleInput();
        }
    }
}