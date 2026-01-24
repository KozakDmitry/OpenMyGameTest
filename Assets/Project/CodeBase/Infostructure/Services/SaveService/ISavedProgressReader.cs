using Assets.Project.CodeBase.Data;
using Cysharp.Threading.Tasks;

namespace Assets.Project.CodeBase.Infostructure.Services.SaveService
{
    public interface ISavedProgressReader
    {
        UniTask LoadProgress(PlayerProgress progress);
    }
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}
