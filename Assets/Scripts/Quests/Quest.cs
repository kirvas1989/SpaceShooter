using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{   
    /// <summary>
    /// Скрипт, отвечающий за систему квестов в игре.
    /// </summary>
    public abstract class Quest : MonoBehaviour
    {
        #region Class Properties
        /// <summary>
        /// Описание миссии. 
        /// </summary>
        [Header("Mission description")]
        [SerializeField] protected string m_Nickname;
        public string Nickname => m_Nickname;


        /// <summary>
        /// Номер миссии / уровня.
        /// </summary>
        [SerializeField] protected int m_NumLevel;
        public int NumLevel => m_NumLevel;


        /// <summary>
        /// Число открытых предметов за игру, которые дают доступ к последующим уровням.
        /// </summary>
        [Header("Items found")] 
        [SerializeField] protected int m_NumPortals; // Открытые порталы.
        public int NumPortals => m_NumPortals;
    
        [SerializeField] protected int m_AustranautsSaved; // Спасенные астранавты.
        public int AustranautSaved => m_AustranautsSaved;


        /// <summary>
        /// Время перед запуском следующей миссии по завершении текущей.
        /// </summary>
        [Header("Timer to next scene aftet mission complete")]
        [SerializeField] protected float m_timer;
        public float Timer => m_timer;  


        /// <summary>
        /// Маркер завершения уровня / миссии.
        /// </summary>
        protected bool isComplete;
        public bool IsComplete => isComplete;

        #endregion

        #region UnityEvents        

        /// <summary>
        /// События, которые запускаются при успешном завершении миссии / уровня.
        /// </summary>
        [Header("Event on mission complete")]
        [SerializeField] protected UnityEvent m_EventOnComplete;
        public UnityEvent EventOnComplete => m_EventOnComplete;

        /// <summary>
        /// Событие, сообщающее о спасении одного из астранавтов.
        /// </summary>
        [HideInInspector] protected UnityEvent m_AustranautEvent;
        public UnityEvent AustranautEvent => m_AustranautEvent;

        private List<Austranaut> austranauts = new();
        private Austranaut[] austranautArray;

        /// <summary>
        /// Событие, сообщающее об открытии одного из порталов.
        /// </summary>
        [HideInInspector] protected UnityEvent m_PortalEvent;
        public UnityEvent PortalEvent => m_PortalEvent;

        /// <summary>
        /// События для скриптов, которые отвечают за звуки спасения астранавтов и открытия порталов, 
        /// а также за отображения их количества в интерфейсе.
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
        /// Поиск астранавтов на сцене.
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
