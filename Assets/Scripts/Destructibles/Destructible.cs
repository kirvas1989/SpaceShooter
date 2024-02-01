using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене. То, что может иметь хитпоинты.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] protected bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Стартовое кол-во хитпоинтов.
        /// </summary>
        [SerializeField] protected int m_HitPoints;
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// Текущие хитпоинты.
        /// </summary>
        protected int m_CurrentHitPoints;
        public int CurrentHitPoints => m_CurrentHitPoints;

        /// <summary>
        /// Визуальный эффект взрыва сущности Destructaible.
        /// </summary>
        [SerializeField] private Explosion m_ExplosionPrefab;
           
        #endregion

        #region Public API
      
        /// <summary>
        /// Применение дамага к объекту.
        /// </summary>
        /// <param name="damage"> Урон, наносимый объекту. </param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            ChangeHitpointsEvent?.Invoke();

            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        [HideInInspector] public UnityEvent ChangeHitpointsEvent;

        /// <summary>
        /// Напрввление движения
        /// </summary>
        /// <returns></returns>
        public Vector3 MovementDirection()
        {
            Vector3 velocity = m_Rigidbody.velocity;
            Vector3 direction = transform.position + velocity * m_PredictionRate;

            return direction;
        }

        /// <summary>
        /// Коэффициент упреждения
        /// </summary>
        private const float m_PredictionRate = 5f;
        public float PredictionRate => m_PredictionRate;

        protected Rigidbody2D m_Rigidbody;

        #endregion

        protected virtual void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();

            m_CurrentHitPoints = m_HitPoints;

            ChangeHitpointsEvent?.Invoke();
        }

        /// <summary>
        /// Переопределяемое событие уничтожения объекта, когда хитпоинты ниже нуля.
        /// </summary> 
        protected virtual void OnDeath()
        {          
            if (m_ExplosionPrefab != null)
            {
                Explosion explosion = Instantiate(m_ExplosionPrefab, transform.position, 
                                      Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));                           
                
                explosion.Explode();
            }

            Destroy(gameObject);

            m_EventOnDeath?.Invoke();
        }

        #region Teams

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIDNeutral = 0;

        [SerializeField] private int m_TeamID;
        public int TeamID => m_TeamID;


        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #endregion

        #region Score

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;  

        #endregion
    }
}

