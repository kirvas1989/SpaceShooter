using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class TrailSwitcher : MonoBehaviour
    {
        [SerializeField] private float trailDelayTimer;

        private TrailRenderer trail;

        private void Start()
        {
            trail = GetComponentInChildren<TrailRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Teleporter teleporter = collision.GetComponent<Teleporter>();

            if (teleporter != null)
            {
                if (trail != null)
                trail.enabled = false;

                StartCoroutine(WaitForTrailRendererTurnOn());
            }
        }

        IEnumerator WaitForTrailRendererTurnOn()
        {
            yield return new WaitForSeconds(trailDelayTimer);

            if (trail != null)
                trail.enabled = true;
        }
    }
}
