using Assets.Project.CodeBase.Infostructure.States;
using Assets.Project.CodeBase.Infostructure.Services;

namespace Assets.Project.CodeBase.Infostructure
{
    public class Game 
    {
        public GameStateMachine _stateMachine;

        public Game()
        {
            _stateMachine = new GameStateMachine(AllServices.Container);
        }
    }
}