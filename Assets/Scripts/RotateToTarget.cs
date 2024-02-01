using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// –азворачивает выстрел в сторону врага.
    /// </summary>
    public class RotateToTarget : MonoBehaviour
    {
        [SerializeField] private float m_RotationSpeed;
        [SerializeField] private float m_Radius;

        private Collider2D m_ClosestTarget;
        private Vector3 targetDirection;

        private const int Environment = 3;         

        private void Start()
        {
            FindClosestTarget();
        }

        private void Update()
        {
            if (m_ClosestTarget == null) return;

            targetDirection = m_ClosestTarget.transform.position - transform.position;

            transform.up = Vector3.Slerp(transform.up, targetDirection, m_RotationSpeed * Time.deltaTime);
        }

        private Collider2D FindClosestTarget()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, m_Radius);

            foreach (Collider2D hit in hits)
            {
                if (hit.transform.root == Player.Instance.ActiveShip.transform) continue;

                if (hit.transform.root.gameObject.layer == Environment || 
                    hit.gameObject.layer == Environment || 
                    hit.transform.parent.gameObject.layer == Environment) 
                    continue;
             
                SpaceShip ship = hit.transform.root.GetComponent<SpaceShip>();
                
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                float closestDistance = Mathf.Infinity;

                if (ship != null)
                {                    
                    if (ship.TeamID == Player.Instance.ActiveShip.TeamID)
                    {
                        continue;
                    }
                    else if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        m_ClosestTarget = hit;
                    }
                }
                else
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        m_ClosestTarget = hit;
                    }
                }                             
            }

            return m_ClosestTarget;
        }
    }
}
