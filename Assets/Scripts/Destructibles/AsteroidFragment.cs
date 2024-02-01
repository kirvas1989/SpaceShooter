using UnityEngine;

namespace SpaceShooter
{
    public class AsteroidFragment : Destructible
    {
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GravityWell gw = collision.transform.GetComponentInParent<GravityWell>();

            if (gw != null)
                ApplyDamage(m_CurrentHitPoints);

            SpaceShip ship = collision.transform.GetComponentInParent<SpaceShip>();

            if (ship != null)
            {
                ApplyDamage((int)ship.transform.GetComponentInParent<CollisionDamageApplicator>().DamageConstant);
            }              
        }    

        public void SetHitPoints(int hp)
        {
            m_HitPoints = hp;           
        }       
    }
}
