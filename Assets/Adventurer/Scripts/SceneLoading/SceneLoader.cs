using System;

namespace Adventurer
{
    public class SceneLoader : ISlotLoader, ISimpleSceneLoader
    {
        private ZenjectSceneLoaderWrapper zenjectSceneLoader;

        public SceneLoader(ZenjectSceneLoaderWrapper zenjectSceneLoader) 
        {
            this.zenjectSceneLoader = zenjectSceneLoader;
        }

        public void Load(SceneID sceneID)
        {
            if (sceneID != SceneID.Bootstrap || sceneID != SceneID.MainMenu)
                throw new ArgumentException($"Scene with {sceneID} index cannot be started without data, use ILevelLoader");

            zenjectSceneLoader.Load(null, (int)sceneID);
        }

        public void Load(SaveSlotData data)
        {
            var sceneID = data.LastSceneIndex;

            if (sceneID == SceneID.Bootstrap || sceneID == SceneID.MainMenu)
                throw new ArgumentException($"Save slot has {sceneID} index as start scene");

            zenjectSceneLoader.Load(container =>
            {
                container.BindInstance(data);
            }, (int)sceneID);
        }
    }
}