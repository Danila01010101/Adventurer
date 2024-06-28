using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Adventurer
{
    public class ZenjectSceneLoaderWrapper : MonoBehaviour
    {
        private readonly ZenjectSceneLoader zenjectSceneLoader;

        public ZenjectSceneLoaderWrapper(ZenjectSceneLoader zenjectSceneLoader) 
            => this.zenjectSceneLoader = zenjectSceneLoader;

        public void Load(Action<DiContainer> action, int sceneID) 
            => zenjectSceneLoader.LoadScene(sceneID, LoadSceneMode.Single, container => action?.Invoke(container));
    }
}