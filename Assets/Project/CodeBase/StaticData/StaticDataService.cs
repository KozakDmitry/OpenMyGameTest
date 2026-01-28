using Assets.Project.CodeBase.StaticData.Balloons;
using Assets.Project.CodeBase.StaticData.Cubes;
using Assets.Project.CodeBase.StaticData.Field;
using Assets.Project.CodeBase.StaticData.Input;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Project.CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string FIELD_CONFIG_PATH = "StaticData/Config/FieldConfig";
        private const string INPUT_CONFIG_PATH = "StaticData/Config/InputConfig";


        private const string LEVEL_DATA_PATH = "StaticData/LevelData/LevelData";
        private const string CUBES_DATA_PATH = "StaticData/LevelData/CubeData";
        private const string BALLOONS_DATA_PATH = "StaticData/LevelData/BalloonsData";
        private FieldConfigData _fieldConfigData;
        private InputConfigData _inputConfigData;


        private LevelData _levelData;
        private CubesData _cubeData;
        private BalloonsData _balloonsData;
        public void Load()
        {
            LoadConfigData();
            LoadLevelData();
            LoadCubeData();
            LoadBalloonsData();
        }


        private void LoadConfigData()
        {
            _fieldConfigData = Resources.Load<FieldConfigData>(FIELD_CONFIG_PATH);
            _inputConfigData = Resources.Load<InputConfigData>(INPUT_CONFIG_PATH);
        }
        private void LoadBalloonsData() =>
            _balloonsData = Resources.Load<BalloonsData>(BALLOONS_DATA_PATH);
        private void LoadCubeData() =>
            _cubeData = Resources.Load<CubesData>(CUBES_DATA_PATH);
        private void LoadLevelData() =>
            _levelData = Resources.Load<LevelData>(LEVEL_DATA_PATH);




        public FieldConfigData ForFieldConfig() =>
            _fieldConfigData;
        public InputConfigData ForInputConfig() =>
           _inputConfigData;

        public LevelData ForFieldData() =>
            _levelData;
        public CubeData ForCubeData(int id)
        {
            CubeData item = _cubeData.cubeData.SingleOrDefault(x => x.id == id);
            if (item == default)
            {
                Debug.Log("StaticData->ForCubeData->No such id: " + id);
                return _cubeData.cubeData[0];
            }
            return item;
        }

        public BalloonsData ForBallonsData() => 
            _balloonsData;



    }
}