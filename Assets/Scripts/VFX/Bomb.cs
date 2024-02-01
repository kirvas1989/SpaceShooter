using UnityEngine;

namespace SpaceShooter
{
    public class Bomb : Explosion
    {
        [Header("Damage Options")]

        [SerializeField] private float m_Radius;

        [SerializeField] private int m_Damage;

        private bool isFromPlayer;

        public override void Explode()
        {
            m_AudioSource.PlayOneShot(m_Clip, volume);

            StartAnimation(true);

            MassAttack(m_Damage);

            Destroy(gameObject, m_ExplosionPrefab.AnimationTime);
        }

        private void MassAttack(int damage)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, m_Radius);

            foreach (Collider2D hit in hits)
            {
                Destructible destructible = hit.transform.root.GetComponent<Destructible>();
                isFromPlayer = true;

                if (destructible != null && destructible != Player.Instance.ActiveShip)
                {
                    destructible.ApplyDamage(damage);

                    if (Player.Instance.ActiveShip != null || isFromPlayer == true)
                    {
                        Player.Instance.AddScore(destructible.ScoreValue);

                        if (m_Damage >= destructible.CurrentHitPoints && destructible.GetComponentInParent<SpaceShip>() != null)
                        {
                            Player.Instance.AddKill();
                        }
                    }
                }
            }
        }
    }
}
