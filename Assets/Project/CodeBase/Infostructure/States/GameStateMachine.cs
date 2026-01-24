using Assets.Project.CodeBase.Infostructure.Services;
using Cysharp.Threading.Tasks;
using System;
using Assets.Project.CodeBase.StaticData;
using System.Collections.Generic;
using Assets.Project.CodeBase.Infostructure.Services.SceneService;
using Assets.Project.CodeBase.Infostructure.Services.SaveService;

namespace Assets.Project.CodeBase.Infostructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        private readonly AllServices _services;


        public GameStateMachine(AllServices services)
        {
            _services = services;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, _services.Single<ISceneService>(),_services.Single<ISaveService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this),

                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }
        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            await state.Enter(payload);
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        private TState GetState<TState>() where TState : class, IExitableState =>
             _states[typeof(TState)] as TState;


        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

    }
}