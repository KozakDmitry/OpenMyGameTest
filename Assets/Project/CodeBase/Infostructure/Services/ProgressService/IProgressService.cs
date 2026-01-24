using Assets.Project.CodeBase.Data;

namespace Assets.Project.CodeBase.Infostructure.Services.ProgressService
{
    public interface IProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}