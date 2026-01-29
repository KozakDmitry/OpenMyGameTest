using Assets.Project.CodeBase.Logic.Gameplay;
using Assets.Project.CodeBase.Logic.Gameplay.BackGround;
using UnityEngine;

namespace Assets.Project.CodeBase.Data.Balloons
{
    public abstract class BaseBalloon : BaseObject
    {

      

        protected IBalloonsController _balloonController;
        protected  Bounds bounds;

        public virtual void Initialize(IBalloonsController balloonsController, Bounds cameraBounds)
        {
            _balloonController = balloonsController;
            bounds = cameraBounds;
        }



        private void Update()
        {
            CustomUpdate();
        }


        protected virtual void CustomUpdate()
        {
           
        }

       

        protected virtual void OnExitScreen()
        {
            _balloonController?.Remove(this);
        }

    }
}
