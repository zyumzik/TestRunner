using DG.Tweening;
using UnityEngine;

namespace PlayerModule
{
    public class PlayerVisuals : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Transform _playerModel;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Ease _jumpUpEase;
        [SerializeField] private Ease _jumpDownEase;

        private Sequence _jumpTween;
        
        private void OnEnable()
        {
            _player.OnJump += OnJump;
            _player.OnSlide += OnSlide;
        }

        private void OnDisable()
        {
            _player.OnJump -= OnJump;
            _player.OnSlide -= OnSlide;
        }

        private void OnJump(float duration)
        {
            _jumpTween?.Complete();

            _jumpTween = DOTween.Sequence();
            _jumpTween
                .Join(_playerModel.DOLocalMoveY(_jumpHeight, duration / 2)).SetEase(_jumpUpEase)
                .Append(_playerModel.DOLocalMoveY(0, duration / 2).SetEase(_jumpDownEase));
        }

        private void OnSlide(float obj)
        {
            _jumpTween?.Complete();
        }
    }
}