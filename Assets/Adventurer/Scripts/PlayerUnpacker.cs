using EvolveGames;
using GenshinImpactMovementSystem;
using UnityEngine;

namespace Adventurer
{
	public class PlayerUnpacker : MonoBehaviour
	{
    	[SerializeField] private ThirdViewPlayer player;
    	[SerializeField] private FirstPersonPlayer playerController;

		private float destructionTime = 1f;

		public (ThirdViewPlayer, FirstPersonPlayer) Unpack()
		{
			while (transform.childCount > 0)
			{
				transform.GetChild(0).SetParent(null);
			}

			Destroy(gameObject, destructionTime);

			return (player, playerController);
		}
	}
}