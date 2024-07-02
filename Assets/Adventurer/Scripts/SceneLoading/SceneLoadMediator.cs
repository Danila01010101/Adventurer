using UnityEngine;
using Zenject;

namespace Adventurer
{
	public class SceneLoadMediator
	{
		private ISceneLoader sceneLoader;
		private ISimpleSceneLoader simpleSceneLoader;

		[Inject]
		private void Construct(ISceneLoader sceneLoader, ISimpleSceneLoader simpleSceneLoader)
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