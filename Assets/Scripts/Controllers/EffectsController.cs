using UnityEngine;
using DG.Tweening;
using Hexagon.Tile;

namespace Hexagon.Controllers
{
    public class EffectsController : MonoBehaviour
    {
        private Transform _camera;

        //DoTween Effect Variables
        private bool _isShaking;
        private float _shakeDuration = 0.1f;
        private float _shakeStrength = 0.1f;
        private float _shakeCooldown = 0.3f;
        private int _shakeVibrato = 1;
        private int _shakeRandomness = 10;

        private void OnEnable() => AbstractTile.OnTileExplode += CameraShakeEffect;
        private void OnDisable() => AbstractTile.OnTileExplode -= CameraShakeEffect;

        private void Start()
        {
            _camera = Camera.main.transform;
        }

        private void CameraShakeEffect(Vector2 coordinates)
        {
            if (_isShaking) return;
            _isShaking = true;
            _camera.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness);
            DOVirtual.DelayedCall(_shakeCooldown, () => _isShaking = false);
        }
    }
}
