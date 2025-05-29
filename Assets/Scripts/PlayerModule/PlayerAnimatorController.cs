using RoadSystem;
using UnityEngine;

namespace PlayerModule
{
    [System.Serializable]
    public class AnimationData
    {
        [field: SerializeField] public string StateName { get; private set; }
        [field: SerializeField] public AnimationClip Clip { get; set; }
        [field: SerializeField, Range(0, 1)] public float TransitionDuration { get; private set; } 
        [field: SerializeField] public float ClipSpeed { get; private set; }
        
        public float StateDuration => Clip.length / ClipSpeed;

        public int Hash
        {
            get
            {
                if (_hash == 0) _hash = Animator.StringToHash(StateName);
                return _hash;
            }
        }

        private int _hash;
    }
    
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _minAnimatorSpeed;
        [SerializeField] private float _maxAnimatorSpeed;
        
        [Header("Animations")] 
        [SerializeField] private AnimationData _idle;
        [SerializeField] private AnimationData _run;
        [SerializeField] private AnimationData _jump;
        [SerializeField] private AnimationData _slide;
        [SerializeField] private AnimationData _death;

        private RoadMovementController _roadMovementController;

        public void Initialize(RoadMovementController roadMovementController)
        {
            _roadMovementController = roadMovementController;
        }

        private void Update()
        {
            _animator.speed = Mathf.Lerp(_minAnimatorSpeed, _maxAnimatorSpeed,
                _roadMovementController.CurrentSpeedRatio);
            //Debug.Log($"Animation Speed: {_animator.speed}");
        }

        public void PlayIdle() => CrossFade(_idle);
        public void PlayRun() => CrossFade(_run);
        public void PlayJump() => CrossFade(_jump);
        public void PlaySlide() => CrossFade(_slide);
        public void PlayDeath() => CrossFade(_death);

        public float GetJumpDuration() => _jump.StateDuration / _animator.speed;
        public float GetSlideDuration() => _slide.StateDuration / _animator.speed;
        
        private void CrossFade(AnimationData animationData)
        {
            _animator.CrossFade(animationData.Hash, animationData.TransitionDuration, 0);
        }
    }
}