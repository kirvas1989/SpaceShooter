using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip m_TargetShip;
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;  // То же самое, как описание метода ниже.

        /// <summary>
        /// То же самое, как описание скрипта выше.
        /// </summary>
        /// <param name="ship"></param>

        //public void SetTargetShip(SpaceShip ship)
        //{
        //    m_TargetShip = ship;
        //}

        [SerializeField] private VirtualJoystick m_MobileJoystick;
        public VirtualJoystick MobileJoystick => m_MobileJoystick;

        [SerializeField] private ControlMode m_ControlMode;
        public ControlMode ControlsMode => m_ControlMode;

        /// <summary>
        /// Кнопка выстрела из главного оружия. 
        /// </summary>
        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        public PointerClickHold MobileFirePrimary => m_MobileFirePrimary;
     
        /// <summary>
        /// Кнопка выстрела из дополнительного оружия. 
        /// </summary>
        [SerializeField] private PointerClickHold m_MobileFireSecondary;
        public PointerClickHold MobileFireSecondary => m_MobileFireSecondary;

        #region Unity Events

        private void Start()
        {
            m_TargetShip = Player.Instance.ActiveShip; // / / / // Дополнил сам.
            
            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);

                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                m_MobileJoystick.gameObject.SetActive(true);

                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (m_TargetShip == null) return;

            if (m_ControlMode == ControlMode.Keyboard)
                ControlKeyboard();

            if (m_ControlMode == ControlMode.Mobile)
                ControlMobile();
        }
        #endregion

        public void ControlMobile()
        {
            m_ControlMode = ControlMode.Mobile; 

            Vector3 dir = m_MobileJoystick.Value;

            m_TargetShip.ThrustControl = dir.y;
            m_TargetShip.TorqueControl = -dir.x;

            if (m_MobileFirePrimary.IsHold == true)
                m_TargetShip.Fire(TurretMode.Primary);

            if (m_MobileFireSecondary.IsHold == true)
                m_TargetShip.Fire(TurretMode.Secondary);
        }

        public void ControlKeyboard()
        {
            m_ControlMode = ControlMode.Keyboard;
            
            float thrust = 0;
            float torque = 0;

            #region Кнопки движения корабля
            if (Input.GetKey(KeyCode.UpArrow))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                torque = -1.0f;
            #endregion

            #region Кнопки стрельбы
            if (Input.GetKey(KeyCode.Space))
                m_TargetShip.Fire(TurretMode.Primary);

            if (Input.GetKey(KeyCode.LeftShift))
                m_TargetShip.Fire(TurretMode.Secondary);

            #endregion

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}
