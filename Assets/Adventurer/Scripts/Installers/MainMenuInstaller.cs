using Zenject;

namespace Adventurer
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindData();
        }

        private void BindData()
        {
            Container.Bind<MainMenuSlotSelector>().AsSingle().NonLazy();
            Container.Bind<GameStarter>().AsSingle().NonLazy();
        }
    }
}