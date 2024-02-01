using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� �������� �������.
    /// </summary>
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;

        [SerializeField] private float m_Lifetime;

        [SerializeField] private int m_Damage;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab; // ��: ���������� ������ ImpactEffect � ��� �����������, ������� ����� Projectile.

        [SerializeField] private Bomb m_bombPrefab;

        [SerializeField] private AudioSource m_AudioSource;
        public AudioSource AudioSource => m_AudioSource;
     
        private float m_Timer;
           
        #region Unity Events

        /// <summary>
        /// ��� �� ��������� ��������� �� Destructible.
        /// </summary>

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            /// <summary>
            /// ��� ��� ��������� ��������� �� Destructible.
            /// </summary>
            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent && dest.TeamID != m_Parent.TeamID)
                {
                    dest.ApplyDamage(m_Damage);       
                    
                    if (m_Parent == Player.Instance.ActiveShip || isFromPlayer == true)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);

                        if (m_Damage >= dest.CurrentHitPoints && dest.GetComponentInParent<SpaceShip>() != null) 
                        {
                            Player.Instance.AddKill();
                        }
                    }

                    OnProjectileLifeEnd(hit.collider, hit.point);
                }
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime)
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }
        #endregion

        /// <summary>
        /// ��� ��� ������� ���������.
        /// </summary>
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            // ���� ������������� ����� �������� ������ � �.�.
                     
            if (m_bombPrefab != null)
            {
                Bomb bomb = Instantiate(m_bombPrefab, transform.position, 
                            Quaternion.Euler(0, 0, Random.Range(0, 360)));
                
                bomb.Explode();
            }  
            
            if (m_ImpactEffectPrefab != null)
            {
                ImpactEffect impact = Instantiate(m_ImpactEffectPrefab, transform.position, 
                                      Quaternion.Euler(0f, 0f, Random.Range(0, 360)));

                impact.GetComponent<AnimateSpriteFrames>().StartAnimation(true);
                impact.GetComponent<AudioSource>().Play();
            }
            
            Destroy(gameObject);
        }

        #region ������ �� ��������� � ����

        private Destructible m_Parent;
        private bool isFromPlayer;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;

            if (m_Parent == Player.Instance.ActiveShip)
                isFromPlayer = true;
        }

        #endregion
    }
}
