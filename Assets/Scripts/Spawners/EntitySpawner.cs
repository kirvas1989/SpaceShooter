using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Entity[] m_EntityPrefabs;

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private SpawnMode m_SpawnMode;

        [SerializeField] private int m_NumSpawns;

        [SerializeField] private float m_RespawnTime;

        [SerializeField] private bool m_AutoDestroy;

        [SerializeField] private float m_DestroyTime;
        
        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_RespawnTime;
        }

        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawnEntities();

                m_Timer = m_RespawnTime;
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

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                if (AreaIsClean())
                {
                    int index = Random.Range(0, m_EntityPrefabs.Length);

                    GameObject entity = Instantiate(m_EntityPrefabs[index].gameObject);

                    entity.transform.position = m_Area.GetRandomInsideZone();
                    entity.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));

                    if (m_AutoDestroy == true && m_DestroyTime > 0)
                        Destroy(entity, m_DestroyTime);               
                }
            }
        }
    }
}
