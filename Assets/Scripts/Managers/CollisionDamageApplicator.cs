using UnityEngine;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_VelocityDamageModifier;

        [SerializeField] private float m_DamageConstant;
        public float DamageConstant => m_DamageConstant;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag)
                return;
             
            var destructible = transform.root.GetComponent<Destructible>();

            if (destructible != null )
            {
                destructible.ApplyDamage((int)m_DamageConstant + 
                                         (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
}
