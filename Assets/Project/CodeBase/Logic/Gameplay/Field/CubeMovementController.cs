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
        private IStaticDataService _staticDataService;
        private CameraSizeProvider _cameraSizeProvider;

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
            _cameraSizeProvider = await AllServices.Container.SingleAwait<CameraSizeProvider>();
            _inputService = AllServices.Container.Single<IInputService>();
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            ConnectSwipeDetect();
        }

        private void ConnectSwipeDetect()
        {
            StaticData.Input.InputConfigData item = _staticDataService.ForInputConfig();
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
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
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
