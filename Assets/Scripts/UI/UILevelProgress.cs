using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{

    public class UILevelProgress : MonoBehaviour
    {
        //[SerializeField] private GameProgress gameProgress;
        //[SerializeField] private StoneSpawner stoneSpawner;
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private TextMeshProUGUI nextLevelText;
        [SerializeField] private Image progressBar;

        private float fillAmountStep;


        private void Start()
        {
            //currentLevelText.text = gameProgress.CurrentLevel.ToString();

            //nextLevelText.text = (gameProgress.CurrentLevel + 1).ToString();

            progressBar.fillAmount = 0;

            //fillAmountStep = (float)1 / stoneSpawner.SpawnAmount;
        }

        public void ProgressBarUpdate()
        {
            progressBar.fillAmount += fillAmountStep;
        }
    }
}
