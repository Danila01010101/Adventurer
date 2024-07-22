using GenshinImpactMovementSystem;
using Zenject;

namespace Adventurer
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLoader();
            BindData();
        }

        private void BindLoader()
        {
            Container.Bind<ZenjectSceneLoaderWrapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.Bind<SceneLoadMediator>().AsSingle();
        }

        private void BindData()
        {
            Container.Bind<DataParser>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavesData>().AsSingle();
            Container.BindInterfacesAndSelfTo<SavesContainer>().AsSingle().NonLazy();
        }
    }
}