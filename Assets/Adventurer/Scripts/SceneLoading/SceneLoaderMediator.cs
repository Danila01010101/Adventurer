using UnityEngine;
using Zenject;

namespace Adventurer
{
	public class SceneLoaderMediator : MonoBehaviour
	{
		private ISlotLoader sceneLoader;
		private ISimpleSceneLoader simpleSceneLoader;

		[Inject]
		private void Construct(ISlotLoader sceneLoader, ISimpleSceneLoader simpleSceneLoader)
		{
			this.sceneLoader = sceneLoader;
			this.simpleSceneLoader = simpleSceneLoader;
		}

		public void GoToGameplayLevel(SaveSlotData data)
			=> sceneLoader.Load(data);

		public void GoToLevelMainMenu()
			=> simpleSceneLoader.Load(SceneID.MainMenu);

		public void LoadBootScene()
			=> simpleSceneLoader.Load(SceneID.Bootstrap);
    }
}