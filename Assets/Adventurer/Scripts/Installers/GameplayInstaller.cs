using Zenject;

namespace Adventurer
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallMovement();
            InstallCheckpoints();
        }

        private void InstallMovement()
        {
            Container.Bind<MovementHandler>().AsSingle().NonLazy();
        }

        private void InstallCheckpoints()
        {
            Container.Bind<CheckpointListener>().AsSingle().NonLazy();
        }
    }
}