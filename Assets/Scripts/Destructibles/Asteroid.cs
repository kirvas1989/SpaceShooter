using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : Destructible
    {
        [SerializeField] private GameObject m_asteroidFragmentPrefab;
        [SerializeField] private ImpactEffect m_Impact;

        [Header("Количество обломков, на которое раскалывается астероид")]       
        [SerializeField] private int minFragments;
        [SerializeField] private int maxFragments;

        [Header("Cила, скоторой обломки будут разлетаться")]
        [SerializeField] private float m_Thrust;
        [SerializeField] private float m_Drag;

        protected override void Start()
        {
            base.Start();

            EventOnDeath.AddListener(SpawnFragments);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GravityWell gw = collision.transform.GetComponentInParent<GravityWell>();

            if (gw != null)
                ApplyDamage(m_CurrentHitPoints);
        }

        protected override void OnDestroy()      
        {                     
            base.OnDestroy();
            
            EventOnDeath.RemoveListener(SpawnFragments);          
        }

        private void SpawnFragments()
        {
            if (m_Impact != null)
            {
                m_Impact = Instantiate(m_Impact, transform.position,
                           Quaternion.Euler(0f, 0f, Random.Range(0, 360)));
            }

            int fragmentsCount = Random.Range(minFragments, maxFragments + 1);

            int hp = m_HitPoints / fragmentsCount;

            float angle = 360 / fragmentsCount;

            for (int i = 0; i < fragmentsCount; i++)
            {
                GameObject asteroid = Instantiate(m_asteroidFragmentPrefab, transform.position,
                                            Quaternion.Euler(0f, 0f, i * angle));

                AsteroidFragment fragment = asteroid.GetComponent<AsteroidFragment>();
                fragment.SetHitPoints(hp);

                Rigidbody2D rb = asteroid.GetComponentInParent<Rigidbody2D>();
                rb.AddForce(fragment.transform.up * m_Thrust * Time.deltaTime, ForceMode2D.Impulse);
                rb.drag = m_Drag;
                rb.angularDrag = m_Drag;
            }
        }
    }
}
