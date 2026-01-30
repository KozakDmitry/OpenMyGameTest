using Assets.Project.CodeBase.Infostructure.Input;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Logic.Shared;
using Assets.Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngineInternal;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class CubeMovementController : InitializableWindow
    {
        private IField _field;
        private IInputService _inputService;
        private ICameraSizeProvider _cameraSizeProvider;

        private float _minimumDistanceConfig,
                      _maximumTimeConfig;

        private Vector2 startPosition,
                    endPosition;
        private float startTime,
                      endTime;
        public async override UniTask Initialize()
        {
            await base.Initialize();
            _field = GetComponent<IField>();
            _cameraSizeProvider = await AllServices.Container.SingleAwait<ICameraSizeProvider>();
            _inputService = AllServices.Container.Single<IInputService>();
            ConnectSwipeDetect();
        }

        private void ConnectSwipeDetect()
        {
            StaticData.Input.InputConfigData item = _inputService.GetInputConfig();
            _maximumTimeConfig = item.MaximumSwipeTime;
            _minimumDistanceConfig = item.MinimumSwipeRange;
            _inputService.OnStartEvent += SwipeStart;
            _inputService.OnEndEvent += SwipeEnd;
        }


        private void SwipeStart(Vector2 position, float time)
        {
            startPosition = _cameraSizeProvider.ScreenToWorldPoint2D(position);
            startTime = time;
        }
        private void SwipeEnd(Vector2 position, float time)
        {
            endPosition = _cameraSizeProvider.ScreenToWorldPoint2D(position);
            endTime = time;
            DetectSwipe();
        }

        private void DetectSwipe()
        {
            if ((Vector2.Distance(startPosition, endPosition) >= _minimumDistanceConfig) && (endTime - startTime) <= _maximumTimeConfig)
            {
                TryGetCube(startPosition, endPosition - startPosition);
            }
        }

        private void TryGetCube(Vector2 startPosition, Vector2 secondDirection)
        {
            RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector2.zero);
            if (hit&& hit.collider.TryGetComponent<FieldCell>(out var result) && result.IsCubeAvailable())
            {
                _field.TrySwapTwoCubes(result, secondDirection);
            }
        }

        private void OnDestroy()
        {
            if (_inputService != null)
            {
                _inputService.OnStartEvent -= SwipeStart;
                _inputService.OnEndEvent -= SwipeEnd;
            }
        }

    }
}
