using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UILivesImage : MonoBehaviour
    {
        [SerializeField] private Image[] m_Images;

        private void Start()
        {
            InitLivesImages();

            Player.Instance.ChangeLivesEvent.AddListener(OnChangeNumLives);
        }

        private void OnDestroy()
        {
            Player.Instance.ChangeLivesEvent.RemoveListener(OnChangeNumLives);
        }

        private void OnChangeNumLives()
        {
            InitLivesImages();
        }

        private void InitLivesImages()
        {
            if (m_Images != null || m_Images.Length > 0)
            {
                for (int i = 0; i < m_Images.Length; i++)
                {
                    m_Images[i].enabled = true;

                    if (i > Player.Instance.NumLives - 1)
                        m_Images[i].enabled = false;
                }
            }
        }
    }
}

