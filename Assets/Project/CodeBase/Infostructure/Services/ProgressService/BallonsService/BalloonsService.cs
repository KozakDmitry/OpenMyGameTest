using Assets.Project.CodeBase.Data.Balloons;
using Assets.Project.CodeBase.StaticData;
using Assets.Project.CodeBase.StaticData.Balloons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project.CodeBase.Infostructure.Services.ProgressService.BallonsService
{
    public class BalloonsService : IBalloonsService
    {
        private IProgressService _progressService;
        private IStaticDataService _staticDataService;

        private BalloonsData _ballonsData;
        public BalloonsService(IProgressService progressService, IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _ballonsData = _staticDataService.ForBallonsData();
            
        }

        public MovableBalloon GetRandomBallon() =>
        _ballonsData.ballons[UnityEngine.Random.Range(0, _ballonsData.ballons.Count)];
        public BallonsSpawnSettings GetCurrentSpawnSettings() => 
            _ballonsData.spawnSettings;
        public int GetRandomSpawnTime() =>
         UnityEngine.Random.Range(_ballonsData.spawnSettings.randomTimeRange[0], _ballonsData.spawnSettings.randomTimeRange[1]);

    }
}
