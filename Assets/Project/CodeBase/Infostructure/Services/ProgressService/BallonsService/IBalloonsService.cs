using Assets.Project.CodeBase.Data.Balloons;
using Assets.Project.CodeBase.StaticData.Balloons;

namespace Assets.Project.CodeBase.Infostructure.Services.ProgressService.BallonsService
{
    public interface IBalloonsService : IService
    {
        BallonsSpawnSettings GetCurrentSpawnSettings();
        MovableBalloon GetRandomBallon();
        int GetRandomSpawnTime();
    }
}