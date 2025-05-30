using FirebaseModule;
using Zenject;

namespace Installers
{
    public class FirebaseInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirebaseInitializer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AuthManager>().AsSingle();
        }
    }
}