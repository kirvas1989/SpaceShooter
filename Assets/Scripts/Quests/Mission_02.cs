using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class Mission_02 : Quest
    {
        [Header("LevelTarget")]
        [SerializeField] private Teleporter m_teleportA;
        [SerializeField] private Teleporter m_teleportB;
        [SerializeField] private Teleporter m_teleportC;

        [Header("Previous Mission")]
        [SerializeField] private Mission_01 mission01;

        #region ����������� ������
        protected override void OnComplete()
        {
            base.OnComplete();
        }

        protected override void OnOpenPortal()
        {
            base.OnOpenPortal();

            Debug.Log("������ 2: ������ ������!");
        }

        protected override void OnAustranautSaved()
        {
            base.OnAustranautSaved();

            CheckTarget();

            Debug.Log("������ 2: ������ ���������!");
        }

        protected override void FindAustranauts()
        {
            base.FindAustranauts();
        }
        #endregion

        #region UnityEvents
        private void Start()
        {
            FindAustranauts();

            if (mission01 != null)
                mission01.Target.EventOnTeleported.AddListener(OnOpenPortal);
        }

        #endregion

        /// <summary>
        /// ����������� ������� ������� ��������� ������.
        /// </summary>
        private void CheckTarget()
        {         
            if (m_AustranautsSaved == 3)
            { 
                ActivateTeleport(m_teleportA);

                Debug.Log("�������� A �����������!");
            }
            
            if (m_AustranautsSaved == 6) 
            {
                ActivateTeleport(m_teleportB);

                Debug.Log("�������� B �����������!");
            }
                
            if (m_AustranautsSaved == 9)
            {
                ActivateTeleport(m_teleportC);

                FinishLevel();

                Debug.Log("�������� � �����������!");
            }
        }

        /// <summary>
        /// ��������� ��������� ����������.
        /// </summary>
        /// <param name="teleporter"></param>
        private void ActivateTeleport(Teleporter teleporter)
        {
            teleporter.ActivateTeleport(true);

            OnOpenPortal();
        }

        #region �������� ��� ��������� ������
        private void FinishLevel()
        {
            OnComplete();

            StartCoroutine(WaitForLevelSummury());
        }

        IEnumerator WaitForLevelSummury()
        {
            yield return new WaitForSeconds(m_timer);

            Debug.Log("������ ������� �������!");          
        }
        #endregion
    }
}

