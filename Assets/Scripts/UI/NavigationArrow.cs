using UnityEngine;

namespace SpaceShooter
{
    public abstract class NavigationArrow : MonoBehaviour
    {
        [SerializeField] protected Transform m_Arrow;
        [SerializeField] protected SpriteRenderer m_Renderer;

        [SerializeField] protected float m_Offset;
        [SerializeField] protected float m_Scale;

        /// <summary>
        /// ћен€ем размер стрелки и ее рассто€ни€ от игрока.
        /// </summary>
        protected virtual void SetArrowPosition()
        {
            m_Arrow.transform.position = new Vector2(m_Arrow.transform.position.x, m_Arrow.transform.position.y + m_Offset);
            m_Arrow.transform.localScale = new Vector3(m_Scale, m_Scale, m_Scale);
        }

        /// <summary>
        /// Ќаходит ближайшую к игроку цель из списка целей и указывает в ее направлении.
        /// </summary>
        /// <param name="targetArray"></param>
        protected virtual void InitClosestTarget(Transform[] targetArray)
        {
            if (targetArray == null || targetArray.Length == 0)
            {
                m_Renderer.enabled = false;
            }
            else
            {
                Transform closestTarget = null;
                float closestDistance = Mathf.Infinity;

                foreach (Transform target in targetArray)
                {
                    if (target == null)
                        m_Renderer.enabled = false;

                    if (target != null)
                    {
                        m_Renderer.enabled = true;

                        float distance = Vector3.Distance(transform.position, target.position);

                        if (distance < closestDistance)
                        {
                            closestTarget = target;
                            closestDistance = distance;
                        }

                        if (closestTarget != null)
                        {
                            Vector3 direction = closestTarget.position - transform.position;
                            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                            m_Arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        }
                    }
                }
            }
        }
    }
}
