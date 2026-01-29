using Assets.Project.CodeBase.Extentions;
using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService;
using Assets.Project.CodeBase.Infostructure.Services.ProgressService.MapService;
using Assets.Project.CodeBase.Infostructure.Services.SaveService;
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
        private IMapInfoService _mapInfoService;
        private ISaveService _saveLoadService;
        public override UniTask Initialize()
        {
            _sceneService = AllServices.Container.Single<ISceneService>();
            _mapInfoService = AllServices.Container.Single<IMapInfoService>();
            _saveLoadService = AllServices.Container.Single<ISaveService>();
            restartButton.onClick.AddListener(() => Restart());
            nextLevelButton.onClick.AddListener(() => GoToNextLevel());
            return base.Initialize();
        }

        private void GoToNextLevel()
        {
            _mapInfoService.ChangeLevelToNext();
            //_saveLoadService.Save();
            _sceneService.LoadScene(SceneNames.Game);
        }


        private void Restart()
        {
            _mapInfoService.ClearSaveLevel();
            _sceneService.LoadScene(SceneNames.Game);
        }
    }
}
