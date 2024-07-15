using GenshinImpactMovementSystem;
using UnityEngine;

namespace Adventurer
{
	public class PlayerUnpacker : MonoBehaviour
	{
    	[SerializeField] private Player player;

		private float destructionTime = 0.1f;

		public Player Unpack()
		{
			while (transform.childCount > 0)
			{
				transform.GetChild(0).SetParent(null);
			}

			Destroy(gameObject, destructionTime);

			return player;
		}
	}
}