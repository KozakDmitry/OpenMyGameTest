using Assets.Project.CodeBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
