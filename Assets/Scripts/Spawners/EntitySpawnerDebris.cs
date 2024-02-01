using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Спавн мусора.
    /// </summary>
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefabs;

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private int m_NumDebris;

        [SerializeField] private float m_RandomSpeed;


        private void Start()
        {
            for (int i = 0; i < m_NumDebris; i++)
            {
                SpawnDebris();
            }
        }

        private bool AreaIsClean()
        {
            if (Player.Instance.ActiveShip != null)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, m_Area.Radius);

                if (hits.Length > 0)
                {
                    foreach (Collider2D hit in hits)
                    {
                        if (hit.transform.root == Player.Instance.ActiveShip.transform)
                            return false;
                    }
                }
            }

            return true;
        }

        private void SpawnDebris()
        {
            if (AreaIsClean())
            {
                int index = Random.Range(0, m_DebrisPrefabs.Length);

                GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

                debris.transform.position = m_Area.GetRandomInsideZone();
                debris.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));

                debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

                Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

                if (rb != null && m_RandomSpeed > 0)
                {
                    rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
                }
            }
        }

        private void OnDebrisDead()
        {
            SpawnDebris();
        }
    }
}
