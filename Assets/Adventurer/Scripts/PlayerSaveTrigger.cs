using EvolveGames;
using System;
using UnityEngine;

namespace Adventurer
{
    public class PlayerSaveTrigger : MonoBehaviour
	{
        public static Action PlayerEnteredSaveZone;

        private bool isSaved = false;

        private void OnTriggerEnter(Collider other)
        {
            if (isSaved == false && other.gameObject.GetComponent<PlayerController>())
            {
                PlayerEnteredSaveZone?.Invoke();
                isSaved = true;
                Debug.Log("Save zone entered");
            }
        }
    }
}