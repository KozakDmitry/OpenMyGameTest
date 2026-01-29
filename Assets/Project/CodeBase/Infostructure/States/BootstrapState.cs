using Assets.Project.CodeBase.Extentions;
using Assets.Project.CodeBase.Infostructure.Factory.BackgroundFactory;
using Assets.Project.CodeBase.Infostructure.Factory.CubeFactory;
using Assets.Project.CodeBase.Infostructure.Input;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService.BallonsService;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService.MapService;
using Assets.Project.CodeBase.Infostructure.Services.SaveService;
using Assets.Project.CodeBase.Infostructure.Services.SceneService;
using Assets.Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;
using UnityEditor.MPE;

namespace Assets.Project.CodeBase.Infostructure.States
{
    public class BootstrapState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly AllServices _services;
        private const string Initial = SceneNames.Start;
        private IProgressService _progressService;
        private IStaticDataService _staticDataService;
        public BootstrapState(IGameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;
            RegisterServices();
        }

        private void RegisterInnerServices()
        {
            _progressService = new ProgressService();
            RegisterStaticData();
        }

        public void RegisterServices()
        {
            RegisterInnerServices();
            RegisterStaticData();
            RegisterDataServices();
            _services.RegisterSingle<IInputService>(new InputService(_staticDataService));
            _services.RegisterSingle<ISaveService>(new SaveService(_progressService));
            _services.RegisterSingle<IBalloonsFactory>(new BalloonsFactory(_staticDataService));
            _services.RegisterSingle<ICubeFactory>(new CubeFactory(_staticDataService));
            _services.RegisterSingle<ISceneService>(new SceneService(_stateMachine,
                                                                     _services.Single<IInputService>()));
        }

        private void RegisterDataServices()
        {
            _services.RegisterSingle<IBalloonsService>(new BalloonsService(_progressService,
                                                                           _staticDataService));
            _services.RegisterSingle<IMapInfoService>(new MapInfoService(_progressService,
                                                                         _staticDataService));
        }

        private void RegisterStaticData()
        {
            _staticDataService = new StaticDataService();
            _staticDataService.Load();
        }



        public async UniTask Enter(string startScene)
        {
            CheckStartScene(ref startScene);
            await _services.Single<ISceneService>().LoadFirstScene(Initial);
            EnterLoadLevel(startScene);
        }

        private void CheckStartScene(ref string startScene)
        {
            if (startScene == SceneNames.Start)
            {
                startScene = SceneNames.Game;
            }
        }

        private void EnterLoadLevel(string startScene) =>
              _stateMachine.Enter<LoadProgressState, string>(startScene);

        public void Exit()
        {

        }


    }

}