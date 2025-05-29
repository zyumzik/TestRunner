using RoadSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class RoadSystemInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startChunkPoint;
        [SerializeField] private Transform _chunksParent;
        [SerializeField] private Transform _clearChunksOnRevivePoint;
        [SerializeField] private Transform[] _roadLanes;
        [SerializeField] private int _startLaneIndex;
        
        public override void InstallBindings()
        {
            Container.Bind<IFactory<Transform, RoadChunk>>().To<RoadChunk.RandomChunkFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<RoadChunkManager>().AsSingle()
                .WithArguments(_startChunkPoint, _chunksParent, _clearChunksOnRevivePoint);
            Container.BindInterfacesAndSelfTo<RoadMovementController>().AsSingle();
            Container.Bind<RoadLaneManager>().AsSingle().WithArguments(_roadLanes, _startLaneIndex);
            Container.Bind<RoadScoreCounter>().AsSingle();
            
            Container.Bind<RoadSystemManager>().AsSingle().NonLazy();
        }
    }
}