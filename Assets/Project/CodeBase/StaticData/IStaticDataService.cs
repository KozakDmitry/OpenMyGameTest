using Assets.Project.CodeBase.Infostructure.Services;

using System.Collections.Generic;

namespace Assets.Project.CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
    }
}