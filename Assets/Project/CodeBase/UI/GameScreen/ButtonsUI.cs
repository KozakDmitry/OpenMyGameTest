using Assets.Project.CodeBase.Extentions;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService;
using Assets.Project.CodeBase.Infostructure.Services.SceneService;
using Assets.Project.CodeBase.Logic.Shared;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Project.CodeBase.UI.GameScreen
{
    public class ButtonsUI : InitializableWindow
    {
        [SerializeField]
        private Button restartButton,
                       nextLevelButton;

        private ISceneService _sceneService;
        private IProgressService _progressService;
        public override UniTask Initialize()
        {
            _sceneService = AllServices.Container.Single<ISceneService>();
            restartButton.onClick.AddListener(() => Restart());
            nextLevelButton.onClick.AddListener(() => GoToNextLevel());
            return base.Initialize();
        }

        private void GoToNextLevel()
        {
            _sceneService.LoadScene(SceneNames.Game);
        }


        private void Restart()
        {
            _sceneService.LoadScene(SceneNames.Game);
        }
    }
}
