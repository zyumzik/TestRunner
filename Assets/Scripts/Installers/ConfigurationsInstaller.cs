using UnityEngine;
using Zenject;

namespace Installers
{
    public class ConfigurationsInstaller : MonoInstaller
    {
        [SerializeField] private ScriptableObject[] _configurations;
        
        public override void InstallBindings()
        {
            foreach (var configuration in _configurations)
            {
                Container.Bind(configuration.GetType()).FromInstance(configuration).AsSingle();
            }
        }
    }
}