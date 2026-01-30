using Assets.Project.CodeBase.Data.Progress;
using Assets.Project.CodeBase.Data.Progress.SaveData;
using Assets.Project.CodeBase.Infostructure.Factory.CubeFactory;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService.MapService;
using Assets.Project.CodeBase.Infostructure.Services.SaveService;
using Assets.Project.CodeBase.Logic.Shared;
using Assets.Project.CodeBase.StaticData;
using Assets.Project.CodeBase.StaticData.Field;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay.Field
{
    public class FieldBuilder : InitializableWindow, ISavedProgress
    {
        private IField _field;
        private IMapInfoService _mapInfoService;
        private ICubeFactory _cubeFactory;
        private ISaveService _saveService;
        public override async UniTask Initialize()
        {
            _cubeFactory = AllServices.Container.Single<ICubeFactory>();
            _mapInfoService = AllServices.Container.Single<IMapInfoService>();
            _saveService = AllServices.Container.Single<ISaveService>();
            _field = GetComponent<IField>();
            _saveService.RegisterWriter(this);
            await GenerateField(_mapInfoService.GetFieldConfig());
        }
        private void OnDestroy()
        {
            if (_saveService != null)
            {
                _saveService.RemoveWriter(this);
            }
        }
        public void UpdateProgress(PlayerProgress progress)
        {
            if (_field.GetFieldCells.Count == 0)
            {
                return;
            }
            List<LevelConditions> conditions = new();
            foreach (var item in _field.GetFieldCells)
            {
                conditions.Add(new LevelConditions(item.MatrixPosition, item.Id));
            }
            progress.mapInfo.levelCondition = conditions;
        }
        private async UniTask GenerateField(FieldConfigData _config)
        {
            await SetFieldSize(_config);
            BuildField();
        }
        private async UniTask SetFieldSize(FieldConfigData _config)
        {
            ICameraSizeProvider camProvider = await AllServices.Container.SingleAwait<ICameraSizeProvider>();
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
            BuildMatrix(_mapInfoService.GetCurrentLevelData());
        }
        private void BuildMatrix(LevelInfo levelInfo)
        {
            _field.InitializeGrid(levelInfo.GetSize());
            CheckSavesAndFillGrid(levelInfo);
        }

        private void CheckSavesAndFillGrid(LevelInfo levelInfo)
        {
            if (_mapInfoService.TryGetSavedPositions(out List<LevelConditions> save))
            {
                LoadSave(save);
            }
            else
            {
                LoadBase(levelInfo);
            }
        }

        private void LoadSave(List<LevelConditions> save)
        {
            for (int i = 0; i < save.Count; i++)
            {
                var item = _cubeFactory.CreateFieldCell(save[i].id);
                _field.AddCell(item, save[i].pos);
            }
        }

        private void LoadBase(LevelInfo levelInfo)
        {
            for (int i = 0; i < levelInfo.FlatMatrix.Length; i++)
            {
                if (levelInfo.FlatMatrix[i] != 0)
                {
                    _field.AddCell(_cubeFactory.CreateFieldCell(levelInfo.FlatMatrix[i]), levelInfo.GetMatrixPosition(i));
                }
            }
        }

    }


}
