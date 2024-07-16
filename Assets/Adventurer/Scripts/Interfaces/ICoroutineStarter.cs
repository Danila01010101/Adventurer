using System.Collections;
using UnityEngine;

namespace Adventurer
{
	public interface ICoroutineStarter
	{
		void StartCoroutine(IEnumerator coroutine);
	}
}