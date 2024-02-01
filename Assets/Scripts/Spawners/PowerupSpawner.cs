using UnityEngine;

namespace SpaceShooter 
{ 
    public class PowerupSpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Powerup[] m_PowerupPrefabs;

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

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                int index = Random.Range(0, m_PowerupPrefabs.Length);

                GameObject entity = Instantiate(m_PowerupPrefabs[index].gameObject);

                entity.transform.position = m_Area.GetRandomInsideZone();
                entity.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));

                if (m_AutoDestroy == true && m_DestroyTime > 0)
                    Destroy(entity, m_DestroyTime);
            }
        }
    }
}
