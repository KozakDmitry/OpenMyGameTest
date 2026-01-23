using System.Threading;
using Assets.Project.CodeBase.Infostructure.Services.SceneService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure.States
{
    public class LoadProgressState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneService _sceneService;

        public LoadProgressState(IGameStateMachine gameStateMachine, ISceneService sceneService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneService = sceneService;
        }

        public async UniTask Enter(string startScene)
        {
            await _sceneService.LoadScene(startScene);
        }




        public void Exit()
        {

        }
    }
    public class QualityItem
    {
        public Vector2 Memory;
        public int idQuality;

    }
}