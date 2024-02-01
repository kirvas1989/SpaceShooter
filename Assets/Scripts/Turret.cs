using UnityEngine;

namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        /// <summary>
        /// �� �� �����, ��� �: public bool CanFire => m_RefireTimer <= 0;
        /// </summary>
        /// <returns></returns>
        /// 
            //public bool CanFire()
            //{
            //    if (m_RefireTimer <= 0)
            //        return true;
            //    else return false;
            //}

        public bool CanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        #region Unity Events
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
                m_RefireTimer -= Time.deltaTime;
        }
        #endregion

        #region Public API
        public void Fire()
        {
            if (m_TurretProperties == null) return;

            if (m_RefireTimer > 0) return;
            
            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false) // ���������, ������� �� ������� ��� ���������.
                return;

            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) // ���������, ������� �� ��������� ��� ���������.
                return;

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponentInParent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;
            projectile.SetParentShooter(m_Ship);
            projectile.AudioSource.PlayOneShot(m_TurretProperties.LaunchSFX);
         
            m_RefireTimer = m_TurretProperties.RateOfFire;    
        }

        /// <summary>
        /// �����, ������� ����� �������� Properties.
        /// </summary>
        /// <param name="props"></param>
        public void AssignLoadOut(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = props; 
        }       

        #endregion
    }
}
