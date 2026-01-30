using Assets.Project.CodeBase.Logic.Gameplay.BackGround;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Project.CodeBase.Data.Balloons
{
    public class MovableBalloon : BaseBalloon
    {

        private float frequency = 1.0f,
                      speed = 1.0f,
                      screenOffset = 2f,
                      startTime;

        private Vector2 baseVelocity,
                        pos,
                        dir;
        private Vector3 movement;

        public override void Initialize(IBalloonsController balloonsController, Bounds cameraBounds)
        {
            base.Initialize(balloonsController, cameraBounds);
            float hw = bounds.size.x * 0.5f,
                  hh = bounds.size.y * 0.5f;
            GetPositionToSide(hw, hh, Random.Range(0, 2), out pos, out dir);
            float angle = Random.Range(-10, 10f) * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle),
                  sin = Mathf.Sin(angle);
            baseVelocity = new Vector2(
                dir.x * cos - dir.y * sin,
                dir.x * sin + dir.y * cos
            ).normalized * speed;

            transform.position = pos;
            startTime = Time.time;
        }

        private void GetPositionToSide(float hw, float hh, int side, out Vector2 pos, out Vector2 dir)
        {
            switch (side)
            {
                case 0:
                    pos = new Vector2(
                        bounds.center.x - hw - 1f,
                        Random.Range(bounds.center.y - hh, bounds.center.y + hh)
                    );
                    dir = Vector2.right;
                    break;

                case 1:
                default:
                    pos = new Vector2(
                        bounds.center.x + hw + 1f,
                        Random.Range(bounds.center.y - hh, bounds.center.y + hh)
                    );
                    dir = Vector2.left;
                    break;
            }
        }


        protected override void CustomUpdate()
        {
            if (baseVelocity == Vector2.zero) return;
            movement = (Vector2)(baseVelocity * Time.deltaTime) +
                                new Vector2(-baseVelocity.y, baseVelocity.x).normalized *
                                Mathf.Sin(frequency * (Time.time - startTime)) *
                                Time.deltaTime;
            transform.position += movement;

            if (!IsInsideExtendedBounds(transform.position))
            {
                OnExitScreen();
            }
        }

        private bool IsInsideExtendedBounds(Vector3 pos)
        {
            return pos.x >= bounds.min.x - screenOffset &&
                   pos.x <= bounds.max.x + screenOffset &&
                   pos.y >= bounds.min.y - screenOffset &&
                   pos.y <= bounds.max.y + screenOffset;
        }
    }
}
