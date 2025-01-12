using System;
using Cysharp.Threading.Tasks.Linq;
using Source.Scripts.Models;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Controllers
{
    public class CameraMovementController : IInitializable, IDisposable
    {
        private readonly PlayerInputModel _playerInputModel;
        private readonly GameObject _background;
        private readonly Camera _camera;

        private float _minX;
        private float _maxX;
        
        private Vector3 _previousPointerPosition;
        
        private bool _firstClick = true;

        public CameraMovementController(PlayerInputModel playerInputModel, GameObject background, Camera camera)
        {
            _playerInputModel = playerInputModel;
            _background = background;
            _camera = camera;
        }

        public void Initialize()
        {
            CalculateMinMax();

            _playerInputModel.PointerWorldPosition.Subscribe(MoveCamera);
        }

        public void Dispose()
        {
        }

        private void CalculateMinMax()
        {
            var bgSpriteRenderer = _background.GetComponent<SpriteRenderer>();
            var bgWidth = bgSpriteRenderer.bounds.size.x;
            var halfCamWidth = _camera.orthographicSize * _camera.aspect;
            
            _minX = _camera.transform.position.x - (bgWidth / 2 - halfCamWidth);
            _maxX = _camera.transform.position.x + (bgWidth / 2 - halfCamWidth);
            
            // Debug.Log(_camera.transform.position.x);
            // Debug.Log(bgWidth);
            // Debug.Log(halfCamWidth);
            // Debug.Log(_minX);
            // Debug.Log(_maxX);
        }
        
        private void MoveCamera(Vector3 pointerWorldPosition)
        {
            if (pointerWorldPosition == Vector3.zero || _playerInputModel.IsInteractionWithItem)
            {
                _firstClick = true;
                return;
            }
            
            if(_firstClick)
            {
                _previousPointerPosition = pointerWorldPosition;
                _firstClick = false;
            }

            var posX = pointerWorldPosition.x - _previousPointerPosition.x;
            
            if (Mathf.Abs(posX) < 0.01f)
            {
                return;
            }
            
            float targetX = Mathf.Clamp(_camera.transform.position.x - posX, _minX, _maxX);
            
            _camera.transform.position = new Vector3(targetX, _camera.transform.position.y, _camera.transform.position.z);
        }
    }
}