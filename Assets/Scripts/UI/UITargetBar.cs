using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UITargetBar : MonoBehaviour
    {
        public enum UITargetType
        {
            Distance,
            Count,
            Timer
        }

        [SerializeField] private GameObject m_TargetPanel;

        [SerializeField] private Image m_FillImage;

        [SerializeField] private UITargetType m_UITargetType;

        [SerializeField] private LevelConditionNumCollected m_Level;

        private Transform m_TargetPoint;
        private Transform m_RespawnPoint;

        private Austranaut[] austranautArray;
        private List<Austranaut> austranauts = new();

        private bool isEnabled = false;
        private bool timerIsRunning;

        private float m_Time;

        private void Start()
        {
            if (m_UITargetType == UITargetType.Distance)
            {
                Player.Instance.ChangeLivesEvent.AddListener(OnChangeLivesEvent);

                m_TargetPoint = GameObject.FindGameObjectWithTag("Quest").transform;
                m_RespawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;

                isEnabled = true;
            }

            if (m_UITargetType == UITargetType.Count)
            {
                austranautArray = FindObjectsOfType<Austranaut>();
                foreach (Austranaut austranaut in austranautArray)
                {
                    austranauts.Add(austranaut);
                    austranaut.EventOnPickUp.AddListener(OnPickUpEvent);
                }

                m_FillImage.fillAmount = 0;
            }

            if (m_UITargetType == UITargetType.Timer)
            {
                timerIsRunning = true;
            }
        }

        private void OnDestroy()
        {
            if (m_UITargetType == UITargetType.Distance)           
                Player.Instance.ChangeLivesEvent.RemoveListener(OnChangeLivesEvent);
            

            if (m_UITargetType == UITargetType.Count)
                foreach (Austranaut austranaut in austranautArray)
                {
                    austranauts.Add(austranaut);
                    austranaut.EventOnPickUp.RemoveListener(OnPickUpEvent);
                }
        }

        private void FixedUpdate()
        {
            if (m_UITargetType == UITargetType.Distance)
            {
                if (Player.Instance.ActiveShip != null && m_RespawnPoint != null && m_TargetPoint != null)
                {
                    if (isEnabled == false)
                        m_TargetPanel.SetActive(false);
                    else
                    {
                        m_TargetPanel.SetActive(true);

                        m_FillImage.fillAmount = Mathf.InverseLerp(m_RespawnPoint.position.magnitude,
                                                               m_TargetPoint.position.magnitude,
                                                               Player.Instance.ActiveShip.transform.position.magnitude);
                    }
                }
            }

            if (m_UITargetType == UITargetType.Timer)
            {
                if(Player.Instance.ActiveShip != null)
                {
                    if (timerIsRunning == true)
                    {
                        m_Time += Time.deltaTime;
                    }

                    m_FillImage.fillAmount = m_Time / (float)LevelController.Instance.ReferenceTime;
                }
            }
        }

        private void OnChangeLivesEvent()
        {
            if (m_UITargetType == UITargetType.Distance)
            {
                isEnabled = true;
            }

            if (m_UITargetType == UITargetType.Timer)
            {
                timerIsRunning = false;
            }
        }

        private void OnPickUpEvent()
        {
            if (m_UITargetType == UITargetType.Count)
            {
                if (Player.Instance.ActiveShip != null && m_Level != null)
                {
                    m_FillImage.fillAmount = (float)m_Level.NumCollected / (float)m_Level.NumToCollect;
                }
            }
        }
    }
}
