using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{   
    /// <summary>
    /// ������, ���������� �� ������� ������� � ����.
    /// </summary>
    public abstract class Quest : MonoBehaviour
    {
        #region Class Properties
        /// <summary>
        /// �������� ������. 
        /// </summary>
        [Header("Mission description")]
        [SerializeField] protected string m_Nickname;
        public string Nickname => m_Nickname;


        /// <summary>
        /// ����� ������ / ������.
        /// </summary>
        [SerializeField] protected int m_NumLevel;
        public int NumLevel => m_NumLevel;


        /// <summary>
        /// ����� �������� ��������� �� ����, ������� ���� ������ � ����������� �������.
        /// </summary>
        [Header("Items found")] 
        [SerializeField] protected int m_NumPortals; // �������� �������.
        public int NumPortals => m_NumPortals;
    
        [SerializeField] protected int m_AustranautsSaved; // ��������� ����������.
        public int AustranautSaved => m_AustranautsSaved;


        /// <summary>
        /// ����� ����� �������� ��������� ������ �� ���������� �������.
        /// </summary>
        [Header("Timer to next scene aftet mission complete")]
        [SerializeField] protected float m_timer;
        public float Timer => m_timer;  


        /// <summary>
        /// ������ ���������� ������ / ������.
        /// </summary>
        protected bool isComplete;
        public bool IsComplete => isComplete;

        #endregion

        #region UnityEvents        

        /// <summary>
        /// �������, ������� ����������� ��� �������� ���������� ������ / ������.
        /// </summary>
        [Header("Event on mission complete")]
        [SerializeField] protected UnityEvent m_EventOnComplete;
        public UnityEvent EventOnComplete => m_EventOnComplete;

        /// <summary>
        /// �������, ���������� � �������� ������ �� �����������.
        /// </summary>
        [HideInInspector] protected UnityEvent m_AustranautEvent;
        public UnityEvent AustranautEvent => m_AustranautEvent;

        private List<Austranaut> austranauts = new();
        private Austranaut[] austranautArray;

        /// <summary>
        /// �������, ���������� �� �������� ������ �� ��������.
        /// </summary>
        [HideInInspector] protected UnityEvent m_PortalEvent;
        public UnityEvent PortalEvent => m_PortalEvent;

        /// <summary>
        /// ������� ��� ��������, ������� �������� �� ����� �������� ����������� � �������� ��������, 
        /// � ����� �� ����������� �� ���������� � ����������.
        /// </summary>                          
        [HideInInspector]
        public static UnityEvent OnQuestEvent = new();
        [HideInInspector]
        public static UnityEvent OnAstranautSoundEvent = new();
        [HideInInspector]
        public static UnityEvent OnPortalSoundEvent = new();
        [HideInInspector]
        public static UnityEvent OnQuestCompleteEvent = new();
        
        #endregion

        #region Actions on Events
        protected virtual void OnOpenPortal()
        {           
            m_NumPortals++;

            m_PortalEvent?.Invoke();
            OnQuestEvent?.Invoke();
            OnPortalSoundEvent?.Invoke();
        }

        protected virtual void OnAustranautSaved()
        {
            m_AustranautsSaved++;

            m_AustranautEvent?.Invoke();
            OnQuestEvent?.Invoke();
            OnAstranautSoundEvent?.Invoke();
        }

        protected virtual void OnComplete()
        {
            isComplete = true;

            m_EventOnComplete?.Invoke();
            OnQuestEvent?.Invoke();
            OnQuestCompleteEvent?.Invoke();
        }

        #endregion

        /// <summary>
        /// ����� ����������� �� �����.
        /// </summary>
        protected virtual void FindAustranauts()
        {
            austranautArray = FindObjectsOfType<Austranaut>();
            foreach (Austranaut austranaut in austranautArray)
            {
                austranauts.Add(austranaut);
                austranaut.EventOnPickUp.AddListener(OnAustranautSaved);
            }
        }
    }
}
