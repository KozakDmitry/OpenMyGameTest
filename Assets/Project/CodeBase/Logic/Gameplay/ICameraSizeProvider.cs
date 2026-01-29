using Assets.Project.CodeBase.Infostructure.Services;
using UnityEngine;

namespace Assets.Project.CodeBase.Logic.Gameplay
{
    public interface ICameraSizeProvider : IObject
    {
        Bounds GetCameraBounds();
        Vector3 ScreenToWorldPoint2D(Vector2 screenPosition);
    }
}