using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.StaticData.Cubes;
using Assets.Project.CodeBase.StaticData.Field;
using System.Collections.Generic;

namespace Assets.Project.CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        CubeData ForCubeData(int id);
        FieldConfigData ForFieldConfig();
        LevelData ForFieldData();
        void Load();
    }
}