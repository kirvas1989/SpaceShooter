using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SpaceShooter
{   
    /// <summary>
    /// Интерфейс игры
    /// </summary>
    public class UITextEvents : MonoBehaviour
    {
        [SerializeField] private Player m_Player;
        [SerializeField] private Mission_01 m_Mission_01;
        [SerializeField] private Mission_02 m_Mission_02;
        [SerializeField] private GameObject missionPassedPanel;

        #region Class Properties
        
        [Header("Text")]
        [SerializeField] private string markTextLevel;
        [SerializeField] private Text m_TextLevel;
        [Header("")]
        [SerializeField] private string markTextPortals;
        [SerializeField] private Text m_TextPortals;
        [Header("")]
        [SerializeField] private string markTextAustranauts;
        [SerializeField] private Text m_TextAustranauts;
        [Header("")]
        [SerializeField] private string markTextLives;
        [SerializeField] private Text m_TextLives;
        [Header("")]
        [SerializeField] private string markTextMission;
        [SerializeField] private Text m_TextMission;

        #endregion

        [Header("Events")]
        [HideInInspector] public UnityEvent OnPlayerEvent;
        [HideInInspector] public UnityEvent OnQuestEvent;
        [HideInInspector] public UnityEvent OnQuestCompleteEvent;

        private void Start()
        {           
            Player.OnPlayerEvent.AddListener(DrawInterface);
            Quest.OnQuestEvent.AddListener(DrawInterface);
            Quest.OnQuestCompleteEvent.AddListener(MissionPassedPannelSwitch);

            missionPassedPanel.SetActive(false);

            DrawInterface();
        }

        private void DrawInterface() 
        {
            m_TextPortals.text = markTextPortals + " " + m_Mission_01.NumPortals;
            m_TextAustranauts.text = markTextAustranauts + " " + m_Mission_01.AustranautSaved;
            m_TextLives.text = markTextLives + " " + m_Player.NumLives;
           
            if (m_Mission_01.IsComplete == false) // Временное решение. Система интерфейса будет переделана, когда будут готовы все миссии / уровни.
            {
                m_TextLevel.text = markTextLevel + " " + m_Mission_01.NumLevel;
                m_TextMission.text = markTextMission + " " + m_Mission_01.Nickname;
            }
            else
            {
                m_TextLevel.text = markTextLevel + " " + m_Mission_02.NumLevel;
                m_TextMission.text = markTextMission + " " + m_Mission_02.Nickname;
            }
        }

        private void MissionPassedPannelSwitch()
        {
            missionPassedPanel.SetActive(true);

            StartCoroutine(WaitForPannelTurnOff());
        }

        IEnumerator WaitForPannelTurnOff()
        {
            yield return new WaitForSeconds(m_Mission_02.Timer);

            missionPassedPanel.SetActive(false);
        }
    }
}
