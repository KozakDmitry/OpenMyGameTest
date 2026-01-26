using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.StaticData.Cubes;
using Assets.Project.CodeBase.StaticData.Field;
using Assets.Project.CodeBase.StaticData.Input;
using System.Collections.Generic;

namespace Assets.Project.CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        FieldConfigData ForFieldConfig();
        InputConfigData ForInputConfig();
        CubeData ForCubeData(int id);
        LevelData ForFieldData();
        void Load();
    }
}