using Zenject;

namespace Adventurer
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindLoader();
            BindData();
        }

        private void BindLoader()
        {
            Container.Bind<ZenjectSceneLoaderWrapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.Bind<SceneLoadMediator>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<DesctopInput>().AsSingle();
        }

        private void BindData()
        {
            Container.Bind<DataParser>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavesData>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavesContainer>().AsSingle().NonLazy();
        }
    }
}