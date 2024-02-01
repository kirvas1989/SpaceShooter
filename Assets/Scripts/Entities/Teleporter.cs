using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(AudioSource))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Teleporter target;

        [SerializeField] private SpriteRenderer m_Renderer;

        [SerializeField] private AudioSource sound;

        [SerializeField] private bool isActivated;

        [SerializeField] private UnityEvent m_EventOnTeleported;
        public UnityEvent EventOnTeleported => m_EventOnTeleported;

        [HideInInspector] public bool IsReceive;


        /// <summary>
        /// Разрешение включения телепортов для класса Quest.
        /// </summary>
        /// <param name="value"></param>
        internal void ActivateTeleport(bool value)
        {
            isActivated = value;
        }

        private void Update()
        {
            if (isActivated == true)
            {
                if (m_Renderer != null)
                {
                    m_Renderer.color = new Color(104 / 255f, 210 / 255f, 149 / 255f); // Светло-зеленый.
                }
                else if (m_Renderer != null)
                {
                    m_Renderer.color = new Color(185 / 255f, 92 / 255f, 104 / 255f); // Светло-красный
                }                
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isActivated == true)
            {
                if (IsReceive == true) return;

                SpaceShip ship = collision.GetComponent<SpaceShip>();

                if (ship != null && ship == Player.Instance.ActiveShip)
                {
                    target.IsReceive = true;

                    ship.transform.position = target.transform.position;

                    if (sound!=null)
                        sound.Play();

                    m_EventOnTeleported?.Invoke();
                }
                else return;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            SpaceShip ship = collision.GetComponent<SpaceShip>();

            if (ship != null && Player.Instance.ActiveShip)
            {
                IsReceive = false;
            }
        }
    }
}
