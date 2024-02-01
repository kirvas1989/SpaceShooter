using UnityEngine;

namespace SpaceShooter
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float m_Lifetime;

        [SerializeField] protected bool m_DirectionControl;

        private float m_Timer;

        private void Update()
        {
            if (m_Timer < m_Lifetime)
                m_Timer += Time.deltaTime;
            else
                Destroy(gameObject);

            if (m_DirectionControl == true)
                transform.up = transform.root.up;
        }

        public float Lifetime
        {
            get { return m_Lifetime; }  
            set { m_Lifetime = value; } 
        }
    }
}
