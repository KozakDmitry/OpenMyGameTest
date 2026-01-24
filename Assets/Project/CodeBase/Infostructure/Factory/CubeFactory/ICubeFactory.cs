using Assets.Project.CodeBase.Infostructure.Services;
using Assets.Project.CodeBase.Logic.Gameplay.Field;

namespace Assets.Project.CodeBase.Infostructure.Factory.CubeFactory
{
    public interface ICubeFactory : IService
    {
        FieldCell CreateFieldCell(int id);
    }
}