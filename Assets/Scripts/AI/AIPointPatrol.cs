using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public class AIPointPatrol : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);


        private static HashSet<AIPointPatrol> m_AllPatrolPoints;

        public static IReadOnlyCollection<AIPointPatrol> AllPatrolPoints => m_AllPatrolPoints;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }

        protected virtual void OnEnable()
        {
            if (m_AllPatrolPoints == null)
                m_AllPatrolPoints = new HashSet<AIPointPatrol>();

            m_AllPatrolPoints.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllPatrolPoints.Remove(this);
        }
    }
}
