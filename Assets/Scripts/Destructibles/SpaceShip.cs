using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [Header("Space Ship")]

        /// <summary>
        /// Масса для автоматической установки у ригида.
        /// </summary>
        [SerializeField] private float m_Mass;

        #region Movement

        /// <summary>
        /// Толкающая вперед сила.
        /// </summary>
        [SerializeField] private float m_Thrust;
        public float Thrust => m_Thrust;
        
        private float defaultThrust;
        
        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float m_Mobility;
        public float Mobility => m_Mobility;
        
        private float defaultMobility;

        /// <summary>
        /// Максимальная линейная скорость.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// Максимальная вращательная скорость. В градусах/сек.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        #endregion

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;
  
        [SerializeField] private AudioSource m_SoundHit;
     
        #region Public API
        /// <summary>
        /// Управление линейной тягой. От -1.0 до +1.0.
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой. От -1.0 до +1.0.
        /// </summary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity Event

        protected override void Start()
        {
            base.Start();

            //m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigidbody.mass = m_Mass;
            m_Rigidbody.inertia = 1;

            InitOffensive();
            InitSpeed();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();

            UpdateEnergyRegen();
        }

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

            if (collision.gameObject.GetComponent<Rigidbody2D>() != null || 
                collision.gameObject.GetComponentInParent<GravityWell>() != null)
            {
                if (gameObject != null)
                {
                    if (m_SoundHit != null)
                    {
                        if (m_SoundHit.enabled == true)
                            m_SoundHit.Play();
                    }                       
                }             
            }
        }

        #endregion

        /// <summary>
        /// Метод добавления сил кораблю для движения.
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigidbody.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force); // Движение вперед.

            m_Rigidbody.AddForce(-m_Rigidbody.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force); // Ограничение максимальной скорости

            m_Rigidbody.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force); // Вращение корабля.

            m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force); // Сила, противоположная вращению ( == максимальная скорость поворота).
        }

        #region Стрельба

        [SerializeField] private Turret[] m_Turrets;

        public UnityEvent FireEvent;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();

                    FireEvent?.Invoke();
                }
            }
        }

        #endregion

        #region Ограничение стрельбы

        /// <summary>
        /// Максимальный показатель энергии и максимальный показатель патронов.
        /// </summary>
        [SerializeField] private int m_MaxEnergy;
        public int MaxEnergy => m_MaxEnergy;

        [SerializeField] private int m_MaxAmmo;
        public int MaxAmmmo => m_MaxAmmo;

        /// <summary>
        /// Текущий показатель энергии и текущий показатель патронов.
        /// </summary>
        private float m_PrimaryEnergy;
        public float PrimaryEnergy => m_PrimaryEnergy;

        private int m_SecondaryAmmo;
        public float SecondaryAmmo => m_SecondaryAmmo;

        /// <summary>
        /// Метод для получения бонусной энергии из подбираемых бонусов.
        /// </summary>
        /// <param name="e"></param>
        public void AddEnergy(int e)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);

            FireEvent?.Invoke();
        }

        /// <summary>
        /// Метод для получения бонусных патронов из подбираемых бонусов.
        /// </summary>
        /// <param name="ammo"></param>
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);

            FireEvent?.Invoke();
        }

        /// <summary>
        /// Инициализация переменных при старте.
        /// </summary>
        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;

            FireEvent?.Invoke();
        }
        #endregion

        #region Восстановление энергии      
        /// <summary>
        /// Восстановление ед. энергии в сек.
        /// </summary>
        [SerializeField] private int m_EnergyRegenPerSecond;

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);

            FireEvent?.Invoke();
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0) return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;
        }
        #endregion

        #region Контроль боезапаса

        public bool DrawAmmo(int count)
        {
            if (count == 0) return true;

            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }

            return false;
        }
        #endregion

        #region Возможность подбирать Powerup

        /// <summary>
        /// Можно будет при подборе различных бонусов менять свойства стрельбы.
        /// </summary>
        /// <param name="props"></param>
        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadOut(props);
            }
        }

        #endregion

        #region Неуязвимость

        public void AddShield(float time)
        {
            m_Indestructible = true;

            ShieldEvent?.Invoke();

            StartCoroutine(WaitForShieildIsActive(time));
        }

        public UnityEvent ShieldEvent;

        IEnumerator WaitForShieildIsActive(float time)
        {
            Debug.Log($"Неуязвимость активирована на {time} секунд!");

            yield return new WaitForSeconds(time);

            m_Indestructible = false;

            Debug.Log("Щит отключен!");
        }

        #endregion

        #region Ускорение

        private void InitSpeed()
        {
            defaultThrust = m_Thrust;
            defaultMobility = m_Mobility;
        }

        public void AddSpeed(float speed, float time)
        {
            float speedFactor = m_Mobility / m_Thrust;

            m_Thrust += speed;
            m_Mobility += speed * speedFactor;

            SpeedEvent?.Invoke();

            StartCoroutine(WaitForAccelerationIsActive(time));
        }

        public UnityEvent SpeedEvent;

        IEnumerator WaitForAccelerationIsActive(float time)
        {
            Debug.Log($"Скорость увеличена на {time} секунд!");

            yield return new WaitForSeconds(time);

            m_Thrust = defaultThrust;
            m_Mobility = defaultMobility;

            Debug.Log("Скорость на дефолтном значении!");
        }
        #endregion

        #region Восполнение здоровья

        public void AddHitpoints(int num)
        {
            if (m_CurrentHitPoints == m_HitPoints)
                return;
            else
            {
                m_CurrentHitPoints += num;

                if (m_CurrentHitPoints > m_HitPoints)
                    m_CurrentHitPoints = m_HitPoints;

                ChangeHitpointsEvent?.Invoke();
            }
        }

        #endregion
    }
}
