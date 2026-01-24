using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;

namespace Assets.Project.CodeBase.Data.Cubes
{
    public abstract class BaseCube : MonoBehaviour
    {
        protected int id;
        protected SpriteRenderer spriteRenderer;
        protected AnimatorController controller;



        public virtual void Construct(int id, SpriteRenderer renderer, AnimatorController controller)
        {
            this.id = id;
            this.spriteRenderer = renderer;
            this.controller = controller;
        }
        public abstract void SetPosition();

    }





}
