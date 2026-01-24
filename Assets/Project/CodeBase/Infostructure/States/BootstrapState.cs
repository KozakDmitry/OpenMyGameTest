using Assets.Project.CodeBase.Extentions;
using Assets.Project.CodeBase.Infostructure.AssetManagement;
using Assets.Project.CodeBase.Infostructure.Factory.BackgroundFactory;
using Assets.Project.CodeBase.Infostructure.Factory.CubeFactory;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService;
using Assets.Project.CodeBase.Infostructure.Services.SaveService;
using Assets.Project.CodeBase.Infostructure.Services.SceneService;
using Assets.Project.CodeBase.StaticData;
using Cysharp.Threading.Tasks;

namespace Assets.Project.CodeBase.Infostructure.States
{
    public class BootstrapState : IPayloadedState<string>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly AllServices _services;
        private const string Initial = SceneNames.Start;
        public BootstrapState(IGameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;
            RegisterServices();
        }


        public void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle<IProgressService>(new ProgressService());
            _services.RegisterSingle<IAssets>(new AssetManager());
            _services.RegisterSingle<ISaveService>(new SaveService(_services.Single<IProgressService>()));
            _services.RegisterSingle<IBackgroundFactory>(new BackgroundFactory(_services.Single<IStaticDataService>(),
                                                                               _services.Single<IAssets>()));
            _services.RegisterSingle<ICubeFactory>(new CubeFactory(_services.Single<IStaticDataService>(),
                                                                               _services.Single<IAssets>()));
            _services.RegisterSingle<ISceneService>(new SceneService(_stateMachine));
        }


        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle(staticData);
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