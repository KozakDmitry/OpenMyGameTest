using Assets.Project.CodeBase.Data.Progress;

namespace Assets.Project.CodeBase.Infostructure.Services.ProgressService
{
    public class ProgressService : IProgressService
    {
        public PlayerProgress Progress
        {
            get;
            set;
        }


        public ProgressService()
        {
            Progress = new PlayerProgress();
        }
    }
}
