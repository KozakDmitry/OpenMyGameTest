using Assets.Project.CodeBase.Data.Balloons;
using Assets.Project.CodeBase.Infostructure.Factory.BackgroundFactory;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService.BallonsService;
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
    public class BalloonsController : InitializableWindow, IBalloonsController
    {

        private IBalloonsFactory _backgroundFactory;
        private IBalloonsService _balloonService;
        private ICameraSizeProvider _cameraSizeProvider;
        private CancellationTokenSource cts;


        private List<BaseBalloon> _movingBalloons;
        private Bounds cameraBounds;
        public async override UniTask Initialize()
        {
            _backgroundFactory = AllServices.Container.Single<IBalloonsFactory>();
            _balloonService = AllServices.Container.Single<IBalloonsService>();
            _cameraSizeProvider = await AllServices.Container.SingleAwait<ICameraSizeProvider>();
            _movingBalloons = new();
            cameraBounds = _cameraSizeProvider.GetCameraBounds();
        }
        public async override UniTask AfterInitialize()
        {
            await base.AfterInitialize();
            StartToSpawnBallons();
        }

        private async void StartToSpawnBallons()
        {
            cts = new CancellationTokenSource();
            try
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    await UniTask.Delay(_balloonService.GetRandomSpawnTime());
                    if (_balloonService.GetCurrentSpawnSettings().maxBallonsAvailable > _movingBalloons.Count)
                    {
                        _movingBalloons.Add(_backgroundFactory.CreateBallon(_balloonService.GetRandomBallon(), transform));
                        _movingBalloons[^1].Initialize(this, cameraBounds);

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Operation was cancelled " + ex.Message);
            }
        }


        public void Remove(BaseBalloon baseBalloon)
        {
            _movingBalloons.Remove(baseBalloon);
            Destroy(baseBalloon.gameObject);
        }


        private void OnDestroy()
        {
            cts?.Cancel();
            cts?.Dispose();
            cts = null;
        }

     
    }
}
