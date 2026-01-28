using Assets.Project.CodeBase.Logic.Gameplay.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.CodeBase.Data.Balloons
{
    public abstract class BaseBalloon : BaseObject
    {

        private float amplitude = 0.5f;
        private float frequency = 1.0f;
        private float speed = 1.0f;

        private Vector2 baseVelocity;
        private float startTime;



        public virtual void Initialize()
        {
            //startTime = Time.time;
            //baseVelocity = Unity.Mathematics.Random.insideUnitCircle.normalized * speed;
        }



        private void Update()
        {
            CustomUpdate();
        }


        protected virtual void CustomUpdate()
        {
            float t = Time.time - startTime;
            float offset = amplitude * Mathf.Sin(frequency * t);

            //transform.position += new Vector3(baseVelocity.x, baseVelocity.y + offset, 0) * Time.deltaTime;
            //if (transform.position.y < -screenHeight / 2f)
            //    Destroy(gameObject);
        }

    }
}
