using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private Transform _rootUI;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().WithArguments(_rootUI);
        }
    }
}