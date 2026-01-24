using Assets.Project.CodeBase.Infostructure.Factory.CubeFactory;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService;
using Assets.Project.CodeBase.Logic.Shared;
using Assets.Project.CodeBase.StaticData;
using Assets.Project.CodeBase.StaticData.Field;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class FieldController : InitializableWindow, IObject
    {
        private IField _field;
        private IStaticDataService _staticDataService;
        private IProgressService _progressService;
        private ICubeFactory _cubeFactory;
        public override async UniTask Initialize()
        {
            _staticDataService = AllServices.Container.Single<IStaticDataService>();
            _progressService = AllServices.Container.Single<IProgressService>();
            _cubeFactory = AllServices.Container.Single<ICubeFactory>();
            _field = GetComponent<IField>();
            await GenerateField(_staticDataService.ForFieldConfig());
        }

        private async UniTask GenerateField(FieldConfigData _config)
        {
            await SetFieldSize(_config);
            BuildField();
        }

        private async UniTask SetFieldSize(FieldConfigData _config)
        {
            CameraSizeProvider cam = await AllServices.Container.SingleAwait<CameraSizeProvider>();
            var rect = cam.GetFieldSize();
            var fieldPos = rect.position + rect.size * _config.relativePosition;
            var fieldRect = new Rect(fieldPos, rect.size * _config.relativeFieldSize);
            _field.SetupFieldSize(fieldRect);
        }

        private void BuildField()
        {
            BuildMatrix(GetCurrentLevel());
        }

        private LevelInfo GetCurrentLevel()
        {
            return _staticDataService.ForFieldData().levelInfo
                 .SingleOrDefault(x => x.LevelId == _progressService.Progress.levelInfo.currentLevelId)
                 ?? _staticDataService.ForFieldData().levelInfo[0];
        }

        private void BuildMatrix(LevelInfo levelInfo)
        {
            _field.InitializeGrid(levelInfo.GetSize());
            for (int i = 0; i <= levelInfo.FlatMatrix.Length; i++)
            {
                _field.AddCell(_cubeFactory.CreateFieldCell(levelInfo.FlatMatrix[i]), levelInfo.GetMatrixPosition(i));
            }
        }

    }


}
