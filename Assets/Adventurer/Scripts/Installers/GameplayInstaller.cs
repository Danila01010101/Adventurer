using Zenject;

namespace Adventurer
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallCheckpoints();
        }

        private void InstallCheckpoints()
        {
            Container.Bind<CheckpointListener>().AsSingle().NonLazy();
        }
    }
}