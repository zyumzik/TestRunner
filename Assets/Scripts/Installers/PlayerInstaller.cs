using PlayerModule;
using PlayerModule.PlayerInputModule;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _playerParent;
        [SerializeField] private Transform _spawnPoint;
        
        public override void InstallBindings()
        {
            #if UNITY_EDITOR
            Container.Bind<IPlayerInputStrategy>().To<EditorInputStrategy>().AsSingle();
            #elif UNITY_ANDROID || UNITY_IOS
            Container.Bind<IPlayerInputStrategy>().To<MobileSwapInputStrategy>().AsSingle();
            #endif

            Container.Bind<PlayerInput>().AsSingle();
            Container.Bind<PlayerInputHandler>().AsSingle();
            
            Container.Bind<PlayerSpawner>().AsSingle().WithArguments(_playerPrefab, _playerParent, _spawnPoint);

            Container.Bind<Player>().FromMethod(context =>
            {
                var playerSpawner = context.Container.Resolve<PlayerSpawner>();
                return playerSpawner.Spawn();
            }).AsSingle().NonLazy();
        }
    }
}