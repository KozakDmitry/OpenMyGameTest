using Assets.Project.CodeBase.Infostructure.Services;

namespace Assets.Project.CodeBase.Infostructure.Input
{
    public interface IInputService : IService
    {
        event InputService.StartT OnStartEvent;
        event InputService.EndT OnEndEvent;

        void Disable();
        void Enable();
    }
}