using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Austranaut : Entity
    {
        [SerializeField] private UnityEvent m_EventOnPickUp;
        public UnityEvent EventOnPickUp => m_EventOnPickUp;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.GetComponent<SpaceShip>();

            if (ship != null && ship == Player.Instance.ActiveShip)
            {
                OnPickUp();
            }
        }

        private void OnPickUp()
        {
            Destroy(gameObject);

            m_EventOnPickUp?.Invoke();
        }
    }
}

