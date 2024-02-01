using UnityEngine;

namespace SpaceShooter
{
    public class PowerupShield : Powerup
    {
        [SerializeField] private float m_Time;
        public float Time => m_Time;

        [SerializeField] private ImpactEffect m_Effect;

        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AddShield(Time);

            if (m_Effect != null)
            {
                ImpactEffect effect = Instantiate(m_Effect, ship.transform.position,
                                      Quaternion.Euler(ship.transform.up), ship.transform);

                effect.Lifetime = Time;
            }
        }
    }
}
