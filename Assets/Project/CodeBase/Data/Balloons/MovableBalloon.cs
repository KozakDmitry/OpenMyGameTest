using Assets.Project.CodeBase.Logic.Gameplay.BackGround;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Project.CodeBase.Data.Balloons
{
    public class MovableBalloon : BaseBalloon
    {

        private float frequency = 1.0f,
                      speed = 1.0f,
                      offset = 2f,
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
            GetPositionToSide(hw, hh, Random.Range(0, 4), out pos, out dir);
            float angle = Random.Range(-30f, 30f) * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle), sin = Mathf.Sin(angle);
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
                    pos = new(bounds.center.x - hw - 1f, Random.Range(bounds.center.y - hh, bounds.center.y + hh));
                    dir = Vector2.right;
                    break;
                case 1:
                    pos = new(bounds.center.x + hw + 1f, Random.Range(bounds.center.y - hh, bounds.center.y + hh));
                    dir = Vector2.left;
                    break;
                case 2:
                    pos = new(Random.Range(bounds.center.x - hw, bounds.center.x + hw), bounds.center.y - hh - 1f);
                    dir = Vector2.up;
                    break;
                default:
                    pos = new(Random.Range(bounds.center.x - hw, bounds.center.x + hw), bounds.center.y + hh + 1f);
                    dir = Vector2.down;
                    break;
            }
        }


        protected override void CustomUpdate()
        {
            if (baseVelocity == Vector2.zero) return;
            movement = (Vector2)(baseVelocity *
                                Time.deltaTime) +
                                new Vector2(-baseVelocity.y, baseVelocity.x).normalized *
                                Mathf.Sin(frequency * Time.time - startTime) *
                                Time.deltaTime;
            transform.position += movement;

            if (!IsInsideExtendedBounds(transform.position))
            {
                OnExitScreen();
            }
        }

        private bool IsInsideExtendedBounds(Vector3 pos)
        {
            return pos.x >= bounds.min.x - offset &&
                   pos.x <= bounds.max.x + offset &&
                   pos.y >= bounds.min.y - offset &&
                   pos.y <= bounds.max.y + offset;
        }
    }
}
