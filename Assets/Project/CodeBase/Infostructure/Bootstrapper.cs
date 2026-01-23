using Assets.Project.CodeBase.Infostructure.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Project.CodeBase.Infostructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private Game _game;
        private async void Awake()
        {
            _game = new Game();
            DontDestroyOnLoad(this);
            await _game._stateMachine.Enter<BootstrapState, string>(SceneManager.GetActiveScene().name);
        }
    }
}