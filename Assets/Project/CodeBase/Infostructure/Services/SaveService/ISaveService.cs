namespace Assets.Project.CodeBase.Infostructure.Services.SaveService
{
    public interface ISaveService : IService
    {
        void Load();
        void RegisterWriter(ISavedProgress writer);
        void RemoveWriter(ISavedProgress writer);
        void Save();
    }
}