using UnityEngine;

namespace SpaceShooter
{
    public class PlanetSpawner : MonoBehaviour
    {
        [SerializeField] private GravityWell[] m_PlanetPrefabs;

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private int m_NumPlanets;

        private void Start()
        {
            for (int i = 0; i < m_NumPlanets; i++)
            {
                SpawnPlanets();
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

        private void SpawnPlanets()
        {
            if (AreaIsClean())
            {
                int index = Random.Range(0, m_PlanetPrefabs.Length);

                GameObject debris = Instantiate(m_PlanetPrefabs[index].gameObject);

                debris.transform.position = m_Area.GetRandomInsideZone();
                debris.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
            }
        }
    }
}
