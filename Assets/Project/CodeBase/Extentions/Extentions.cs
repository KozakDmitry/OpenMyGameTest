using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Extentions
{
    public static class Extentions
    {
        public static async UniTask AwaitAnimationComplete(this Animator animator, int layerIndex = 0)
        {
            await UniTask.Yield();
            if (animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime >= 1f)
            {
                return;
            }
            while (animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime <= 1f)
            {
                await UniTask.Yield(); 
            }
        }
    }
}
