using AdsModule;
using Zenject;

namespace Installers
{
    public class AdsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AdsManager>().AsSingle().NonLazy();
        }
    }
}