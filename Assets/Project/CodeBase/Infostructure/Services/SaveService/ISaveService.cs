using Assets.Project.CodeBase.Data.Progress;

namespace Assets.Project.CodeBase.Infostructure.Services.SaveService
{
    public interface ISaveService : IService
    {

        void RegisterWriter(ISavedProgress writer);
        void RemoveWriter(ISavedProgress writer);
        void Load();
        void Save();
    }
}