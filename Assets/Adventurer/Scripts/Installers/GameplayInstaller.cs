using UnityEngine;
using Zenject;

namespace Adventurer
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallMovement();
        }

        private void InstallMovement()
        {
            Container.Bind<MovementHandler>().AsSingle().NonLazy();
        }
    }
}