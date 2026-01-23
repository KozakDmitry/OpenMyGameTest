
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.CodeBase.UI.LoadingScreen
{
    public class LoadingUI : MonoBehaviour
    {
        public Slider slider;

        private void Awake()
        {
            DIContainer.Get().Register<LoadingUI>(this);
        }

    }
}
