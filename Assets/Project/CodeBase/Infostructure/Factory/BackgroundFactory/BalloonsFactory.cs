using Assets.Project.CodeBase.Data.Balloons;
using Assets.Project.CodeBase.StaticData;
using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure.Factory.BackgroundFactory
{
    public class BalloonsFactory : IBalloonsFactory
    {
        private readonly IStaticDataService _staticData;



        public BalloonsFactory(IStaticDataService staticData)
        {
            _staticData = staticData;
        }


        public BaseBalloon CreateBallon(BaseBalloon movableBalloon, Transform transform) => 
            Object.Instantiate<BaseBalloon>(movableBalloon, transform);
    }
}
