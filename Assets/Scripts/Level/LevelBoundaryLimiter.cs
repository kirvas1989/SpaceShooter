using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Ограничитель позиции. Работает в связке со скриптом LevelBoundary, если таковой имеется на сцене.
    /// Кидается на объект, который надо уничтожить.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var lb = LevelBoundary.Instance;

            if (lb.ShapeMode == LevelBoundary.Shape.Circle)
            {
                var r = lb.Radius;

                if (transform.position.magnitude > r)
                {
                    if (lb.LimitMode == LevelBoundary.Mode.Limit)
                    {
                        transform.position = transform.position.normalized * r;
                    }

                    if (lb.LimitMode == LevelBoundary.Mode.Teleport)
                    {
                        transform.position = -transform.position.normalized * r;
                    }
                }
            }

            if (lb.ShapeMode == LevelBoundary.Shape.Rectangle)
            {
                var rect = lb.Bounds;

                if (lb.LimitMode == LevelBoundary.Mode.Limit)
                {
                    if (transform.position.x < rect.x)
                        transform.position = new Vector3(rect.x, transform.position.y);

                    if (transform.position.x > rect.x + rect.width)
                        transform.position = new Vector3(rect.x + rect.width, transform.position.y);

                    if (transform.position.y < rect.y)
                        transform.position = new Vector3(transform.position.x, rect.y);

                    if (transform.position.y > rect.y + rect.height)
                        transform.position = new Vector3(transform.position.x, rect.y + rect.height);
                }

                if (lb.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    if (transform.position.x < rect.x)
                        transform.position = new Vector3(rect.x + rect.width, transform.position.y);

                    if (transform.position.x > rect.x + rect.width)
                        transform.position = new Vector3(rect.x, transform.position.y);

                    if (transform.position.y < rect.y)
                        transform.position = new Vector3 (transform.position.x, rect.y + rect.height);

                    if (transform.position.y > rect.y + rect.height)
                        transform.position = new Vector3(transform.position.x, rect.y);
                }

            }
        }
    }
}
