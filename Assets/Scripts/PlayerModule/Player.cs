using System;
using Configurations;
using Core.FiniteStateMachine;
using Core.Managers;
using Core.TimerModule;
using GameStateManagerModule;
using PlayerModule.PlayerInputModule;
using PlayerModule.PlayerStates;
using RoadSystem;
using RoadSystem.Obstacle;
using UnityEngine;
using Zenject;

namespace PlayerModule
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController _movementController;
        [SerializeField] private PlayerAnimatorController _animatorController;
        [SerializeField] private ObstacleDetector _obstacleDetector;
        [SerializeField] private Collider _runningCollider;
        [SerializeField] private Collider _jumpCollider;
        [SerializeField] private Collider _slideCollider;

        [Inject] private PlayerConfiguration _playerConfiguration;
        [Inject] private PlayerInputHandler _playerInputHandler;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private TicksManager _ticksManager;
        [Inject] private RoadLaneManager _roadLaneManager;
        [Inject] private RoadMovementController _roadMovementController;
        
        private int _health;
        private Timer _jumpTimer;
        private Timer _slideTimer;

        #region StateMachine Variables

        private StateMachine _stateMachine;
        private readonly Trigger _runTrigger = new();

        #endregion

        public event Action<float> OnSlide;
        public event Action<float> OnJump;
        public event Action OnDeath;
        
        #region Monobehaviour Methods
        
        private void Awake()
        {
            _health = _playerConfiguration.MaxHealth;
            _animatorController.Initialize(_roadMovementController);
            InitializeStateMachine();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnEnable()
        {
            _obstacleDetector.OnObstacleEnter += ObstacleEntered;
            _gameStateManager.OnGamePrepared += OnGamePrepared;
            _gameStateManager.OnGameStart += OnGameStart;
            _gameStateManager.OnGameEnd += OnGameEnd;
            _gameStateManager.OnGameRestarted += OnGameRestarted;
        }

        private void OnDisable()
        {
            _obstacleDetector.OnObstacleEnter -= ObstacleEntered;
            _gameStateManager.OnGamePrepared -= OnGamePrepared;
            _gameStateManager.OnGameStart -= OnGameStart;
            _gameStateManager.OnGameEnd -= OnGameEnd;
            _gameStateManager.OnGameRestarted -= OnGameRestarted;
        }
        
        #endregion

        public void DecreaseHealth()
        {
            if (_health == 0) return;
            
            _health = Mathf.Clamp(_health - 1, 0, _playerConfiguration.MaxHealth);
            if (_health == 0) Die();
        }

        public void Die()
        {
            _gameStateManager.EndGame();
            OnDeath?.Invoke();
        }
        
        public void Respawn()
        {
            _health = _playerConfiguration.MaxHealth;
            _stateMachine.Reset();
            transform.position = _roadLaneManager.Reset().position;
        }

        public void Jump()
        {
            if (_jumpTimer.IsRunning) return;
            
            _jumpTimer.Start(_animatorController.GetJumpDuration());
            _slideTimer.Stop();
            
            OnJump?.Invoke(_animatorController.GetJumpDuration());
        }
        
        public void Slide()
        {
            if (_slideTimer.IsRunning) return;
            
            _slideTimer.Start(_animatorController.GetSlideDuration());
            _jumpTimer.Stop();
            
            OnSlide?.Invoke(_animatorController.GetSlideDuration());
        }
        
        private void ObstacleEntered(RoadObstacle obstacle)
        {
            DecreaseHealth();
            obstacle.DestroyObstacle();
        }

        private void OnGamePrepared()
        {
            Respawn();
        }
        
        private void OnGameStart()
        {
            _movementController.enabled = true;
            _runTrigger.Activate();
        }

        private void OnGameEnd()
        {
            _movementController.enabled = false;
        }

        private void OnGameRestarted()
        {
            _health = _playerConfiguration.MaxHealth;
            _movementController.enabled = true;
            _runTrigger.Activate();
        }
        
        private void InitializeStateMachine()
        {
            _stateMachine = new StateMachine();
            
            //_stateMachine.OnStateChanged += (state) => Debug.Log($"Player state: {state}");
            
            // declaring states
            var idleState = new IdleState(_animatorController);
            var runningState = new RunningState(_animatorController, _runningCollider);
            var jumpState = new JumpState(_animatorController, _jumpCollider);
            var slideState = new SlideState(_animatorController, _slideCollider);
            var deathState = new DeathState(_animatorController);
            
            // timers
            _jumpTimer = new Timer(_ticksManager);
            _slideTimer = new Timer(_ticksManager);
            
            // defining transitions
            _stateMachine.AddTransition(idleState, runningState, _runTrigger);
            
            _stateMachine.AddTransition(runningState, jumpState,  
                new Condition(() => _jumpTimer.IsRunning));
            _stateMachine.AddTransition(jumpState, runningState, 
                new Condition(() => !_jumpTimer.IsRunning));
            
            _stateMachine.AddTransition(runningState, slideState, 
                new Condition(() => _slideTimer.IsRunning));
            _stateMachine.AddTransition(slideState, runningState, 
                new Condition(() => !_slideTimer.IsRunning));
            
            _stateMachine.AddTransition(jumpState, slideState, 
                new Condition(() => _slideTimer.IsRunning));
            _stateMachine.AddTransition(slideState, jumpState, 
                new Condition(() => _jumpTimer.IsRunning));
            
            _stateMachine.AddAnyTransition(deathState, new Condition(() => _health == 0));
            _stateMachine.AddTransition(deathState, idleState, new Condition(() => _health > 0));
            
            // setting initial state
            _stateMachine.SetInitialState(idleState);
        }
    }
}