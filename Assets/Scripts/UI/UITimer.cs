using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField] private Text m_TimerText;

        private bool timerIsRunning;

        private float m_Time;

        private void Start()
        {
            ResultPanelController.Instance.ResultEvent.AddListener(OnResultEvent);

            timerIsRunning = true;
        }

        private void OnDestroy()
        {
            ResultPanelController.Instance.ResultEvent.RemoveListener(OnResultEvent);
        }

        private void FixedUpdate()
        {
            if (timerIsRunning)
            {
                m_Time += Time.deltaTime;
                DisplayTime(m_Time);
            }
        }

        private void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            m_TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void OnResultEvent()
        {
            if (ResultPanelController.Instance != null)
                timerIsRunning = false;
        }
    }
}
