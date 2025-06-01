using FirebaseModule;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FirebaseInstaller : MonoInstaller
    {
        [SerializeField] private string _databaseUri;
        [SerializeField] private string _leaderboardPathString;
        
        public override void InstallBindings()
        {
            Container.Bind<FirebaseInitializer>().AsSingle().WithArguments(_databaseUri).NonLazy();
            
            Container.BindInterfacesAndSelfTo<AuthManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<LeaderboardManager>().AsSingle().WithArguments(_leaderboardPathString);
        }
    }
}