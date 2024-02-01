using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ќзвучка игровых событий.
    /// </summary>
    public class FXSound : MonoBehaviour
    {
        [SerializeField] private AudioSource m_AudioSource;

        [SerializeField] private AudioClip m_ClipPortal;
        [SerializeField] private AudioClip m_ClipAustranaut;
        [SerializeField] private AudioClip m_ClipPowerup;

        [SerializeField] private float m_volume;

        [HideInInspector] public UnityEvent OnAstranautSoundEvent;
        [HideInInspector] public UnityEvent OnPortalSoundEvent;
        [HideInInspector] public UnityEvent OnPowerupSoundEvent;


        private void Start()
        {
            Quest.OnAstranautSoundEvent.AddListener(PlayAustranautSound);
            Quest.OnPortalSoundEvent.AddListener(PlayPortalSound);
            Powerup.OnPowerupSoundEvent.AddListener(PlayPowerupSound);

        }

        public void PlayAustranautSound()
        {
            m_AudioSource.PlayOneShot(m_ClipAustranaut, m_volume);
        }

        public void PlayPortalSound()
        {
            m_AudioSource.PlayOneShot(m_ClipPortal, m_volume);
        }

        public void PlayPowerupSound()
        {
            m_AudioSource.PlayOneShot(m_ClipAustranaut, m_volume);
        }
    }
}
