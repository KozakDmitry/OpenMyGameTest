using Assets.Project.CodeBase.Data.Balloons;
using Assets.Project.CodeBase.Infostructure.Services;

namespace Assets.Project.CodeBase.Infostructure.Factory.BackgroundFactory
{
    public interface IBalloonsFactory : IService
    {
        BaseBalloon CreateBallon(BaseBalloon movableBalloon, UnityEngine.Transform transform);
    }
}