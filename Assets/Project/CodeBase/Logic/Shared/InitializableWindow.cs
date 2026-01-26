using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
