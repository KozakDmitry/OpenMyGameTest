using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;

        public LoadLevelState(IGameStateMachine stateMachine) => 
            _stateMachine = stateMachine;
        public async UniTask Enter(string sceneName) => 
            await LoadDependencies();

        private async UniTask LoadDependencies()
        {
            _stateMachine.Enter<GameLoopState>();
        }



        public void Exit()
        {

        }
    }
}