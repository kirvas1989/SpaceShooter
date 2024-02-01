using UnityEngine;

namespace SpaceShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera m_Camera;

        /// <summary>
        /// —сылка объекта, за кем будет следить камера.
        /// </summary>
        [SerializeField] private Transform m_Target;

        /// <summary>
        /// —корость интерполл€ции.
        /// </summary>
        [SerializeField] private float m_InterpolationLinear;

        /// <summary>
        /// —короость угловой интерполл€ции (насколько плавно будет поворачиватьс€ камера.
        /// </summary>
        [SerializeField] private float m_InterpolationAngular;

        /// <summary>
        /// —мещение камеры по оси Z.
        /// </summary>
        [SerializeField] private float m_CameraZOffset;

        /// <summary>
        /// —мещение камеры по направлению движени€.
        /// </summary>
        [SerializeField] private float m_ForwardOffset;

        private void FixedUpdate()
        {
            if (m_Target == null || m_Camera == null) return;

            Vector2 camPos = m_Camera.transform.position;
            Vector2 targetPos = m_Target.position + m_Target.transform.up * m_ForwardOffset;
            Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, m_InterpolationLinear * Time.deltaTime);

            m_Camera.transform.position = new Vector3(newCamPos.x, newCamPos.y, m_CameraZOffset);

            if (m_InterpolationAngular > 0)
            {
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, 
                                                               m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
            }
        }

        public void SetTarget(Transform newTarget)
        {
            m_Target = newTarget;
        }
    }
}
