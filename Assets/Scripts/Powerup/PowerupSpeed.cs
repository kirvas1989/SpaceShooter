using UnityEngine;

namespace SpaceShooter
{
    public class PowerupSpeed : Powerup
    {
        /// <summary>
        /// На сколько увеличивать скорость.
        /// </summary>
        [SerializeField] private float m_BonusSpeed;

        public float Speed => m_BonusSpeed;

        /// <summary>
        /// Длительность буста скорости.
        /// </summary>
        [SerializeField] private float m_Time;

        [SerializeField] private ImpactEffect m_Effect;

        public float Time => m_Time;

        private CameraEffect cameraEffect;

        private void Awake()
        {
            cameraEffect = FindObjectOfType<CameraEffect>();
        }

        protected override void OnPickedUp(SpaceShip ship)
        {

            ship.AddSpeed(Speed, Time);

            if (cameraEffect != null)
                cameraEffect.RaiseCameraHeight(Time);

            if (m_Effect != null)
            {
                ImpactEffect effect = Instantiate(m_Effect, ship.transform.position,
                                      Quaternion.Euler(ship.transform.up), ship.transform);

                effect.Lifetime = Time;
            }
        }
    }
}
