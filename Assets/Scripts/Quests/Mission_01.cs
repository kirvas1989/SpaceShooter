using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class Mission_01 : Quest
    {
        [Header("LevelTarget")]
        [SerializeField] private Teleporter m_Target;
        public Teleporter Target => m_Target;

        #region ����������� ������
        protected override void OnComplete()
        {
            base.OnComplete();
        }

        protected override void OnOpenPortal()
        {
            base.OnOpenPortal();

            Debug.Log("������ 1: ������ ������!");
        }

        protected override void OnAustranautSaved()
        {
            base.OnAustranautSaved();

            Debug.Log("������ 1: ������ ���������!");
        }

        protected override void FindAustranauts()
        {
            base.FindAustranauts();
        }

        #endregion

        private void Start()
        {
            m_Target.EventOnTeleported.AddListener(FinishLevel);

            FindAustranauts();
        }

        #region  �������� ��� ��������� ������
        private void FinishLevel()
        {
            m_Target.EventOnTeleported.RemoveListener(FinishLevel);

            OnOpenPortal();

            OnComplete();

            StartCoroutine(WaitForLevelSummury());                    
        }

        IEnumerator WaitForLevelSummury()
        {
            yield return new WaitForSeconds(m_timer);

            Debug.Log("������� 1 �������!");
        }

        #endregion
    }
}

