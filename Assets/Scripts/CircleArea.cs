using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace SpaceShooter
{
    /// <summary>
    /// Рисует в редакторе круг, где будет спавн игровых сущностей.
    /// </summary>
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float m_Radius;

        [SerializeField] private Color m_Color;
        public float Radius => m_Radius;

        public Vector2 GetRandomInsideZone()
        {
            return (Vector2) transform.position + (Vector2) UnityEngine.Random.insideUnitSphere * m_Radius;
        }

#if UNITY_EDITOR

        //private static Color GizmoColor = new Color(0, 1, 0, 0.15f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = m_Color;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
    }
#endif
}
