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
            Container.Bind<DataParser>().AsSingle();
            Container.Bind<SavesContainer>().AsSingle();
        }
    }
}