using UnityEngine;
using Zenject;

namespace Adventurer
{
	public class BootstrapLoader : MonoBehaviour
	{
		private SceneLoadMediator sceneLoader;

		[Inject]
		private void Construct(SceneLoadMediator loader)
		{
            sceneLoader = loader;
        }

        private void Start()
        {
            sceneLoader.GoToLevelMainMenu();
        }
    }
}