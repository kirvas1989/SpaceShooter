using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        /// <summary>
        /// Жизни игрока.
        /// </summary>
        [SerializeField] private int m_NumLives;
        public int NumLives => m_NumLives;
        
        /// <summary>
        /// Ссылка на корабль со сцены.
        /// </summary>
        [SerializeField] private SpaceShip m_Ship;
        public SpaceShip ActiveShip => m_Ship;

        /// <summary>
        /// Ссылка на префаб корабля.
        /// </summary>
        [SerializeField] private GameObject m_PlayerShipPrefab;

        [SerializeField] private CameraController m_CameraController;
        
        [SerializeField] private MovementController m_MovementController;

        [SerializeField] private Explosion m_ExplosionPrefab;
    
        [HideInInspector] private UnityEvent m_Event;
        public UnityEvent Event => m_Event;

        public UnityEvent ChangeLivesEvent;

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }

        /// <summary>
        /// События для отображения в интерфейсе.
        /// </summary>
        [HideInInspector] public static UnityEvent OnPlayerEvent = new ();

        private void Start()
        {
            Respawn();
        }

        /// <summary>
        /// Метод, который будет выполняться всякий раз при смерти корабля.
        /// </summary>
        private void OnShipDeath() 
        {
            m_NumLives--;
            
            ChangeLivesEvent?.Invoke();
            
            m_Ship.EventOnDeath.RemoveListener(OnShipDeath); 
            
            if (m_NumLives > 0)
            {
                StartCoroutine(WaitForAnimationDeath());
            }
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);

            m_Event?.Invoke();
            OnPlayerEvent?.Invoke();
        }

        private void Respawn() 
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);
                m_Ship = newPlayerShip.GetComponent<SpaceShip>();

                m_CameraController.SetTarget(m_Ship.transform);
                m_MovementController.SetTargetShip(m_Ship);

                m_Ship.EventOnDeath.AddListener(OnShipDeath);

                ChangeLivesEvent?.Invoke();
            }
        }
        
        /// <summary>
        /// Ждем, пока закончится анимация взрыва корабля перед респауном нового.
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitForAnimationDeath()
        {
            yield return new WaitForSeconds(m_ExplosionPrefab.AnimationTime);

            Respawn();
        }

        #region Score

        public int Score { get; internal set; }
        public int NumKills { get; private set; }
        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion
    }
}
