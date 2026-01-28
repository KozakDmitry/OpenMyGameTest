using Assets.Project.CodeBase.Data.Balloons;
using Assets.Project.CodeBase.Infostructure.AssetManagement;
using Assets.Project.CodeBase.StaticData;
using UnityEngine;

namespace Assets.Project.CodeBase.Infostructure.Factory.BackgroundFactory
{
    public class BalloonsFactory : IBalloonsFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IAssets _assets;



        public BalloonsFactory(IStaticDataService staticData, IAssets assets)
        {
            _staticData = staticData;
            _assets = assets;
        }


        public BaseBalloon CreateBallon(BaseBalloon movableBalloon)
        {
            throw new System.NotImplementedException();
        }
    }
}
