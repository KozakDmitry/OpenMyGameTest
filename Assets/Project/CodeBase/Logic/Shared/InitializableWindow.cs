using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Shared
{
    public abstract class InitializableWindow : MonoBehaviour
    {

        public virtual async UniTask Initialize()
        {
           
        }

        public virtual async UniTask AfterInitialize()
        {
            
        }


    }
}
