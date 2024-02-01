using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ѕоднимает камеру при увеличении скорости
    /// </summary>
    public class CameraEffect : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;

        [SerializeField] private float m_CameraSpeed;

        [SerializeField] private float m_SizebyEffect;

        public float Size => m_SizebyEffect;

        private float defaultSize;

        private bool isActive = false;

        #region Unity Events

        private void Start()
        {
            defaultSize = m_Camera.orthographicSize;
        }

        private void FixedUpdate()
        {
            if (isActive == false)
            {
                if (m_Camera.orthographicSize > defaultSize)
                    m_Camera.orthographicSize -= m_CameraSpeed * Time.fixedDeltaTime;
            }

            if (isActive == true)
            {
                if (m_Camera.orthographicSize < m_SizebyEffect)
                    m_Camera.orthographicSize += m_CameraSpeed * Time.fixedDeltaTime;
            }
        }

        #endregion

        public void RaiseCameraHeight(float time)
        {
            isActive = true;

            StartCoroutine(WaitForCameraIsUp(time));
        }

        IEnumerator WaitForCameraIsUp(float time)
        {
            yield return new WaitForSeconds(time);

            isActive = false;
        }
    }
}

