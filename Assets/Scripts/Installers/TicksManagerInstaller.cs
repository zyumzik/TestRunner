using Core.Managers;
using Zenject;

namespace Installers
{
    public class TicksManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TicksManager>().AsSingle();
        }
    }
}