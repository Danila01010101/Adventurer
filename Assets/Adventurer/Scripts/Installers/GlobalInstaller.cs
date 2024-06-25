using System;
using Zenject;

namespace Adventurer
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<DesctopInput>().AsSingle();
        }
    }
}