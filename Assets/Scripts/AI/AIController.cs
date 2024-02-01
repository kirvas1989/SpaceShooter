using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }
        #region Class Properties
        /// <summary>
        /// Выбор поведения ИИ.
        /// </summary>
        [SerializeField] private AIBehaviour m_AIBehaviour;

        #region Movement

        [Header("Movement")]
        /// <summary>
        /// Скорость движения корабля.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NaviagationLinear;

        /// <summary>
        /// Скорость поворота корабля.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        /// <summary>
        /// Время, через которое корабль будет лететь к новой случайной точке в радиусе m_PatrolPoint[].
        /// </summary>
        [SerializeField] private float m_RandomSelectMovePointTime;

        /// <summary>
        /// Указание зоны патрулирования. Если зон несколько - будет организовано круговое движения по маршруту из списка зон.
        /// </summary>
        [SerializeField] private AIPointPatrol[] m_PatrolPoints;

        #endregion

        #region Collisions

        [Header("Evade Collisions")]

        /// <summary>
        /// Длина луча рейкаста для определения коллизии столкновения.
        /// </summary>
        [SerializeField] private float m_EvadeRayLength;

        /// <summary>
        /// Коэффициент поворота. Чем выше значение,тем больше будет поворачивать корабль от препятствия. 
        /// </summary>
        [SerializeField] private float m_EvadeCollisionRate;

        /// <summary>
        /// Время, через которое будет меняться сторона уворота от столкновения (левая / правая).
        /// </summary>
        [SerializeField] private float m_EvadeCollisionTime;

        #endregion

        #region Attack

        [Header("Attack")]
        /// <summary>
        /// Радиус определения ближайшего противника для атаки.
        /// </summary>
        [SerializeField] private float m_AggroRadius;

        /// <summary>
        /// Промежутки времени между бросками рейкаста на поиск новой цели для атаки.
        /// </summary>
        [SerializeField] private float m_FindNewTargetTime;

        /// <summary>
        /// Промежутки времени между выстрелами.
        /// </summary>
        [SerializeField] private float m_ShootDelay;

        /// <summary>
        /// Устанавливает на каком расстоянии от игрока начинает по нему стрелять.
        /// </summary>
        [SerializeField] private float m_FireRange;
        
        /// <summary>
        /// Время запрета на стрельбу во время избегания коллизии.
        /// </summary>
        [SerializeField] private float m_FireBanTime;

        /// <summary>
        /// Префаб оружия, из которого будут производиться выстрелы.
        /// </summary>
        [SerializeField] Projectile m_ProjectilePrefab;
        #endregion

        private SpaceShip m_SpaceShip;
        private Destructible m_SelectedTarget;
        private AIPointPatrol m_TargetPatrolPoint;
        private Vector3 m_MovePosition;
        private int m_PointIndex;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;
        private Timer m_EvadeCollisionTimer;
        private Timer m_FirePermitTimer;

        #endregion

        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            InitTimers();

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_PatrolPoints != null && m_PatrolPoints.Length > 0)
                {
                    m_PointIndex = 0;
                    m_TargetPatrolPoint = m_PatrolPoints[m_PointIndex];
                }
                else
                {
                    m_TargetPatrolPoint = FindNearestPatrolPoint();
                }
            }
        }

        #region Update

        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
            {
                // Заготовка
            }

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviorPatrol();
            }
        }

        private void UpdateBehaviorPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionEvadeCollision();
            ActionFindNewAttackTarget();
            ActionFire();
        }

        #endregion

        #region Actions

        #region PatrolBehaviour

        /// <summary>
        /// Движение с упреждением к цели
        /// </summary>
        private void MakeLead()
        {
            float distance = Vector2.Distance(m_SelectedTarget.transform.position, transform.position);

            if (distance > m_SelectedTarget.PredictionRate)
                m_MovePosition = m_SelectedTarget.MovementDirection();
        }

        /// <summary>
        /// Движение по заданному в инспекторе маршруту 
        /// </summary>         
        private void FollowThePatrolRoute()
        {
            bool reachedPatrolPoint = (m_PatrolPoints[m_PointIndex].transform.position - transform.position).sqrMagnitude
                                       < m_PatrolPoints[m_PointIndex].Radius * m_PatrolPoints[m_PointIndex].Radius;

            if (reachedPatrolPoint == true)
            {
                if (m_PointIndex < m_PatrolPoints.Length - 1)
                {
                    m_PointIndex++;
                }
                else
                    m_PointIndex = 0;
            }
            else
            {
                m_MovePosition = m_PatrolPoints[m_PointIndex].transform.position;
            }
        }

        private void PatrolTheArea()
        {
            bool isInsidePatrolZone = (m_TargetPatrolPoint.transform.position - transform.position).sqrMagnitude
                                       < m_TargetPatrolPoint.Radius * m_TargetPatrolPoint.Radius;

            if (isInsidePatrolZone == true)
            {
                if (m_RandomizeDirectionTimer.IsFinished == true)
                {
                    Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_TargetPatrolPoint.Radius
                                       + m_TargetPatrolPoint.transform.position;
                    m_MovePosition = newPoint;
                    m_RandomizeDirectionTimer.Restart();
                }
            }
            else
            {
                m_MovePosition = m_TargetPatrolPoint.transform.position;
            }
        }

        private AIPointPatrol FindNearestPatrolPoint()
        {
            float maxDist = float.MaxValue;

            AIPointPatrol potentialTarget = null;

            foreach (var v in AIPointPatrol.AllPatrolPoints)
            {
                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = m_AggroRadius;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;

                if (v.TeamID == Destructible.TeamIDNeutral) continue;

                if (v.TeamID == m_SpaceShip.TeamID) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);


                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }

        #endregion 

        private void ActionFindNewMovePosition()
        {
            if (isEvade == true) return;

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    MakeLead();
                }
                else
                {
                    if (m_TargetPatrolPoint != null)
                    {
                        if (m_PatrolPoints.Length > 1)
                        {
                            FollowThePatrolRoute();
                        }
                        else
                        {
                            PatrolTheArea();
                        }
                    }
                    else
                    {
                        m_MovePosition = FindNearestPatrolPoint().transform.position;
                    }
                }
            }
        }

        private void ActionFindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestDestructibleTarget();

                m_FindNewTargetTimer.Restart();
            }
        }

        #region Collision

        [SerializeField] private bool isEvade;
        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                isEvade = true;
                m_EvadeCollisionTimer.Start(m_EvadeCollisionTime);

                m_MovePosition = transform.position + transform.right * m_EvadeCollisionRate;
            }
            else
            {
                if (m_EvadeCollisionTimer.IsFinished == true)
                {
                    isEvade = false;
                }
            }
        }

        #endregion

        #region Control
        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NaviagationLinear;

            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular; ///           
        }

        private const float MAX_ANGLE = 45.0f;


        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }
        #endregion

        #region Fire
        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                CheckFirePermission();

                if (isFireAllowed == true)
                {
                    if (m_FireTimer.IsFinished == true)
                    {
                        m_SpaceShip.Fire(TurretMode.Primary);

                        m_FireTimer.Restart();
                    }
                }
            }
        }

        [HideInInspector] private bool isFireAllowed;
        private void CheckFirePermission()
        {
            float distance = (m_SelectedTarget.transform.position - transform.position).magnitude;

            if (distance > m_FireRange)
            {
                isFireAllowed = false;
            }
            else
            {
                if (isEvade == false && m_FirePermitTimer.IsFinished == true)
                    isFireAllowed = true;

                if (isEvade == true)
                {
                    isFireAllowed = false;
                    m_FirePermitTimer.Start(m_FireBanTime);

                    if (m_FirePermitTimer.IsFinished == true)
                        isFireAllowed = true;
                }
            }              
        }
        #endregion

        #endregion

        #region Timers

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
            m_EvadeCollisionTimer = new Timer(m_EvadeCollisionTime);
            m_FirePermitTimer = new Timer(m_FireBanTime);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
            m_EvadeCollisionTimer.RemoveTime(Time.deltaTime);
            m_FirePermitTimer.RemoveTime(Time.deltaTime);
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoints[0] = point;
        }

        #endregion
    }
}