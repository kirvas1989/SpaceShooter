using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class CleanSheetRestart : MonoBehaviour
    {
        public void CleanRestart()
        {
            //if (live.CurrentLive == 0)
            //{
            //    PlayerPrefs.DeleteKey("");


            //    //PlayerPrefs.DeleteAll();
            //}

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
