using FirebaseModule;
using GameStateManagerModule;

namespace RoadSystem
{
    public class RoadSystemManager
    {
        private readonly GameStateManager _gameStateManager;
        private readonly RoadChunkManager _roadChunkManager;
        private readonly RoadMovementController _roadMovementController;
        private readonly RoadScoreCounter _roadScoreCounter;
        private readonly LeaderboardManager _leaderboardManager;

        public RoadSystemManager(GameStateManager gameStateManager, RoadChunkManager roadChunkManager,
            RoadMovementController roadMovementController, RoadScoreCounter roadScoreCounter,
            LeaderboardManager leaderboardManager)
        {
            _gameStateManager = gameStateManager;
            _roadChunkManager = roadChunkManager;
            _roadMovementController = roadMovementController;
            _roadScoreCounter = roadScoreCounter;
            _leaderboardManager = leaderboardManager;

            _gameStateManager.OnGamePrepared += OnGamePrepared;
            _gameStateManager.OnGameStart += OnGameStart;
            _gameStateManager.OnGamePaused += OnGamePaused;
            _gameStateManager.OnGameResumed += OnGameResumed;
            _gameStateManager.OnGameEnd += OnGameEnd;
            _gameStateManager.OnGameRestarted += OnGameRestarted;
        }
        
        private void OnGamePrepared()
        {
            _roadChunkManager.ConstructStartRoad();
            _roadMovementController.Reset();
            _roadScoreCounter.Reset();
        }
        
        private void OnGameStart()
        {
            _roadMovementController.StartMoving();
            _roadScoreCounter.Start();
        }

        private void OnGamePaused()
        {
            _roadMovementController.StopMoving();
            _roadScoreCounter.Stop();
        }

        private void OnGameResumed()
        {
            _roadMovementController.StartMoving();
            _roadScoreCounter.Start();
        }

        private void OnGameEnd()
        {
            _roadMovementController.StopMoving();
            _roadScoreCounter.Stop();

            TryUpdateScore();
        }

        private void OnGameRestarted()
        {
            _roadChunkManager.DeactivateObstaclesOnRevive();
            _roadMovementController.StartMoving();
            _roadScoreCounter.Start();
        }

        private async void TryUpdateScore()
        {
            var userData = await _leaderboardManager.GetUserScore();

            if (userData == null)
            {
                await _leaderboardManager.SubmitScore(_roadScoreCounter.TimeScore);
            }
            else if (_roadScoreCounter.TimeScore > userData.Score)
            {
                await _leaderboardManager.SubmitScore(_roadScoreCounter.TimeScore);
            }
        }
    }
}