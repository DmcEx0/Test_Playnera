﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Source.Scripts.Models;
using UnityEngine;
using VContainer.Unity;

namespace Source.Scripts.Controllers
{
    public class CameraMovementController : IInitializable, IDisposable
    {
        private readonly IPlayerInputModel _playerInputModel;
        private readonly GameObject _background;
        private readonly Camera _camera;
        private readonly CancellationTokenSource _tokenSource;

        private float _minX;
        private float _maxX;
        
        private Vector3 _previousPointerPosition;
        
        private bool _firstClick = true;

        public CameraMovementController(IPlayerInputModel playerInputModel, GameObject background, Camera camera)
        {
            _playerInputModel = playerInputModel;
            _background = background;
            _camera = camera;

            _tokenSource = new CancellationTokenSource();
        }

        //Скрипт управляет движением камеры по сцене, основываясь на размере спрайта бэкграунда
        //для отслеживания зажатия поинтера использутеся реактивное свойство и подписка на него
        public void Initialize()
        {
            CalculateMinMax();

            _playerInputModel.PointerWorldPosition.Subscribe(MoveCamera).AddTo(_tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource.Dispose();
        }

        private void CalculateMinMax()
        {
            var bgSpriteRenderer = _background.GetComponent<SpriteRenderer>();
            var bgWidth = bgSpriteRenderer.bounds.size.x;
            var halfCamWidth = _camera.orthographicSize * _camera.aspect;
            
            _minX = _camera.transform.position.x - (bgWidth / 2 - halfCamWidth);
            _maxX = _camera.transform.position.x + (bgWidth / 2 - halfCamWidth);
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
            
            if (Mathf.Abs(posX) < 0.01f) // исключение минимального перемещения
            {
                return;
            }
            
            float targetX = Mathf.Clamp(_camera.transform.position.x - posX, _minX, _maxX);
            
            _camera.transform.position = new Vector3(targetX, _camera.transform.position.y, _camera.transform.position.z);
        }
    }
}