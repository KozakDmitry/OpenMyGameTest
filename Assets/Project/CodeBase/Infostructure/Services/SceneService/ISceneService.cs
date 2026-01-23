using Cysharp.Threading.Tasks;
using System;
using static Assets.Project.CodeBase.Infostructure.Services.SceneService.SceneService;

namespace Assets.Project.CodeBase.Infostructure.Services.SceneService
{
    public interface ISceneService : IService
    {
        event OnLoad OnSceneLoaded;
        UniTask LoadScene(string nextScene, Action<string> callback = null);
    }
}