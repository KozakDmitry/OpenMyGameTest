using Assets.Project.CodeBase.Extentions;
using Assets.Project.CodeBase.Infostructure.Input;
using Assets.Project.CodeBase.Infostructure.States;
using Assets.Project.CodeBase.UI.LoadingScreen;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Assets.Project.CodeBase.Infostructure.Services.SceneService
{
    public class SceneService : ISceneService
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;

        public delegate void OnLoad();
        public event OnLoad OnSceneLoaded;

        private bool _isLoading;

        public SceneService(IGameStateMachine stateMachine, IInputService inputService)
        {
            _sceneLoader = new();
            _isLoading = false;
            _stateMachine = stateMachine;
            _inputService = inputService;
        }

        public async UniTask LoadFirstScene(string scene)
        {
            _isLoading = true;
            await _sceneLoader.Load(scene);
            _isLoading = false;
        }

        public async UniTask LoadScene(string nextScene, Action<string> callback = null)
        {
            if (_isLoading)
            {
                return;
            }
            else
            {
                _isLoading = true;
            }
            _inputService.Disable();
            LoadingUI loadUI = await InitLoadingScene();
            await _sceneLoader.LoadBase(nextScene, loadUI.slider, async (loadingScene) =>
            {
                for (float k = 0.8f; k < 0.9f; k += 0.01f)
                {
                    await UniTask.Delay(10);
                    loadUI.slider.value = k;
                }
                await InitializeAndSwapToNewScene(nextScene, loadingScene, loadUI.slider);

            });
        }

        private async UniTask InitializeAndSwapToNewScene(string nextScene, Scene LoadingScene, Slider slider)
        {
            SceneManager.SetActiveScene(LoadingScene);
            await _stateMachine.Enter<LoadLevelState, string>(nextScene);
            for (float k = 0.9f; k < 1f; k += 0.01f)
            {
                await UniTask.Delay(10);
                slider.value = k;
            }
            await SceneManager.UnloadSceneAsync(SceneNames.Loading);
            GC.Collect();
            OnSceneLoaded?.Invoke();
            _inputService.Enable();
            _isLoading = false;
        }


        private async UniTask<LoadingUI> InitLoadingScene()
        {
            string LastScene = SceneManager.GetActiveScene().name;
            Scene LoaderScene = await _sceneLoader.LoadAdditive(SceneNames.Loading);
            SceneManager.SetActiveScene(LoaderScene);
            LoadingUI loadUI = await InitializeLoadingUI();
            await SceneManager.UnloadSceneAsync(LastScene);
            return loadUI;
        }

        private async UniTask<LoadingUI> InitializeLoadingUI() =>
            await AllServices.Container.SingleAwait<LoadingUI>();
    }


}

