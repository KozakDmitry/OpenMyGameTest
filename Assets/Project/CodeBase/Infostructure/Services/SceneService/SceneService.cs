using Assets.Project.CodeBase.Extentions;
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

        public delegate void OnLoad();
        public event OnLoad OnSceneLoaded;


        public SceneService(IGameStateMachine stateMachine)
        {
            _sceneLoader = new();
            _stateMachine = stateMachine;
        }

        public async UniTask LoadFirstScene(string scene)
        {
            await _sceneLoader.Load(scene);
        }

        public async UniTask LoadScene(string nextScene, Action<string> callback = null)
        {
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

