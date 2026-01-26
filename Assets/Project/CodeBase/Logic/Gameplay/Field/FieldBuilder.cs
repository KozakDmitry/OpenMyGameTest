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
    public class FieldBuilder : InitializableWindow
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
            CameraSizeProvider camProvider = await AllServices.Container.SingleAwait<CameraSizeProvider>();
            Bounds cameraBounds = camProvider.GetCameraBounds();
            Vector2 fieldSize = new Vector2(
                cameraBounds.size.x * _config.relativeFieldSize.x,
                cameraBounds.size.y * _config.relativeFieldSize.y
            );

            Vector2 fieldCenter = cameraBounds.center + new Vector3(
                cameraBounds.size.x * _config.relativePosition.x,
                cameraBounds.size.y * _config.relativePosition.y,
                0
            );
            var fieldBounds = new Bounds(fieldCenter, new Vector3(fieldSize.x, fieldSize.y, 0));

            _field.SetupFieldSize(fieldBounds);
        }

        private void BuildField()
        {
            BuildMatrix(GetCurrentLevel());
        }
        private void BuildMatrix(LevelInfo levelInfo)
        {
            _field.InitializeGrid(levelInfo.GetSize());
            for (int i = 0; i < levelInfo.FlatMatrix.Length; i++)
            {
                if (levelInfo.FlatMatrix[i] != 0)
                {
                    _field.AddCell(_cubeFactory.CreateFieldCell(levelInfo.FlatMatrix[i]), levelInfo.GetMatrixPosition(i));
                }
            }
        }
        private LevelInfo GetCurrentLevel()
        {
            return _staticDataService.ForFieldData().levelInfo
                 .SingleOrDefault(x => x.LevelId == _progressService.Progress.levelInfo.currentLevelId)
                 ?? _staticDataService.ForFieldData().levelInfo[0];
        }



    }


}
