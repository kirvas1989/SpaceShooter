using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {
        [HideInInspector]
        public static UnityEvent OnPowerupSoundEvent = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            if (ship != null && ship == Player.Instance.ActiveShip) // Ограничение, чтобы бонусы мог подбирать только корабль игрока.
            {
                OnPickedUp(ship);
               
                Destroy(gameObject);

                OnPowerupSoundEvent?.Invoke();
            }           
        }

        protected abstract void OnPickedUp(SpaceShip ship);       
    }
}
