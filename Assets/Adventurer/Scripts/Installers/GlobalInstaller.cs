using Zenject;

namespace Adventurer
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindLoader();
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
    }
}