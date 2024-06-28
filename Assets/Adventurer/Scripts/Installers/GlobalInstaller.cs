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
            Container.Bind<ZenjectSceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.Bind<SceneLoaderMediator>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<DesctopInput>().AsSingle();
        }
    }
}