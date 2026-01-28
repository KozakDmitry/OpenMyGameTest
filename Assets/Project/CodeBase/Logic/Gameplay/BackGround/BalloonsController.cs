using Assets.Project.CodeBase.Data.Balloons;
using Assets.Project.CodeBase.Infostructure.Factory.BackgroundFactory;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Logic.Shared;
using Assets.Project.CodeBase.StaticData;
using Assets.Project.CodeBase.StaticData.Balloons;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.BackGround
{
    public class BalloonsController : InitializableWindow
    {
        private IStaticDataService _staticDataService;
        private IBalloonsFactory _backgroundFactory;
        private CameraSizeProvider _cameraSizeProvider;
        private CancellationTokenSource cts;

        private BalloonsData _ballonsData;
        private List<BaseBalloon> _movingBalloons;
        public async override UniTask Initialize()
        {
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            _backgroundFactory = AllServices.Container.Single<IBalloonsFactory>();
            _cameraSizeProvider = await AllServices.Container.SingleAwait<CameraSizeProvider>();
            _ballonsData = _staticDataService.ForBallonsData();
            _movingBalloons = new();
        }
        public async override UniTask AfterInitialize()
        {
            //StartToSpawnBallons();
            await base.AfterInitialize();
        }

        private async void StartToSpawnBallons()
        {
            cts = new CancellationTokenSource();
            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (_ballonsData.spawnSettings.maxBallonsAvailable > _movingBalloons.Count)
                    {
                        _movingBalloons.Add(_backgroundFactory.CreateBallon(GetRandomBallon()));
                    }
                    await UniTask.Delay(GetRandomTime());
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Operation was cancelled " + ex.Message);
            }
        }

        private MovableBalloon GetRandomBallon() =>
            _ballonsData.ballons[UnityEngine.Random.Range(0, _ballonsData.ballons.Count)];

        private int GetRandomTime() =>
            UnityEngine.Random.Range(_ballonsData.spawnSettings.randomTimeRange[0], _ballonsData.spawnSettings.randomTimeRange[1]);

        private void OnDestroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            cts = null;
        }

    }
}
