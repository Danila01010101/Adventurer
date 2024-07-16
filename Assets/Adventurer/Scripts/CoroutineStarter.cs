using System.Collections;
using UnityEngine;

namespace Adventurer
{
    public class CoroutineStarter : MonoBehaviour, ICoroutineStarter
    {
        void ICoroutineStarter.StartCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
    }
}