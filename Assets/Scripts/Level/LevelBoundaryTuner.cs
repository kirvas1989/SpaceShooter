using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{ 
    /// <summary>
    /// Меняет положение и радиус уровня в зависимости от уровня.
    /// </summary>
    /// 
    public class LevelBoundaryTuner : MonoBehaviour
    {
        [Header("Level 01")]
        [SerializeField] GameObject level_001;
        [SerializeField] private float radius_Level_001;

        [Header("Level 02")]
        [SerializeField] GameObject level_002;
        [SerializeField] private float radius_Level_002;

        [Header("Level 02")]
        [SerializeField] GameObject level_003;
        [SerializeField] private float radius_Level_003;

        private LevelBoundary levelBoundary;

        [HideInInspector] public UnityEvent OnQuestCompleteEvent;

        private void Start()
        {
            levelBoundary = LevelBoundary.Instance;

            SwitchLevel_001();
        }

        private void SwitchLevel_001()
        {
            Quest.OnQuestCompleteEvent.AddListener(SwitchLevel_002);

            levelBoundary.transform.position = level_001.transform.position;
            levelBoundary.Radius = radius_Level_001;
        }

        private void SwitchLevel_002()
        {
            Quest.OnQuestCompleteEvent.RemoveListener(SwitchLevel_002);
            Quest.OnQuestCompleteEvent.AddListener(SwitchLevel_003);

            levelBoundary.transform.position = level_002.transform.position;
            levelBoundary.Radius = radius_Level_002;
        }

        private void SwitchLevel_003()
        {
            Quest.OnQuestCompleteEvent.RemoveListener(SwitchLevel_003);

            levelBoundary.transform.position = level_003.transform.position;
            levelBoundary.Radius = radius_Level_003;
        }
    }
}
