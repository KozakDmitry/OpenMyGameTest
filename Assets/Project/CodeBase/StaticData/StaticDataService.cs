using Assets.Project.CodeBase.StaticData.Cubes;
using Assets.Project.CodeBase.StaticData.Field;
using System.Linq;
using UnityEngine;

namespace Assets.Project.CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string FIELD_CONFIG_PATH = "StaticData/Config/FieldConfig";
        private const string LEVEL_DATA_PATH = "StaticData/LevelData/LevelData";
        private const string CUBES_DATA_PATH = "StaticData/LevelData/CubeData";

        private FieldConfigData _fieldConfigData;
        private LevelData _levelData;
        private CubesData _cubeData;
        public void Load()
        {
            LoadConfigData();
            LoadLevelData();
            LoadCubeData();
        }

        public FieldConfigData ForFieldConfig() =>
            _fieldConfigData;
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



        private void LoadCubeData() =>
            _cubeData = Resources.Load<CubesData>(CUBES_DATA_PATH);
        private void LoadLevelData() =>
            _levelData = Resources.Load<LevelData>(LEVEL_DATA_PATH);
        private void LoadConfigData() =>
            _fieldConfigData = Resources.Load<FieldConfigData>(FIELD_CONFIG_PATH);

    }
}