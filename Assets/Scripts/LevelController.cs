using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Main
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private int _levelToLoadAfterCompletingAllLevels = 1;

        [SerializeField] private PlayerData _defaultPlayerData;

        private void Start()
        {
            PlayerResources.ResetMoneyOnLevel();
        }

        private void ResetSaves()
        {
            PlayerResources.SetupData(_defaultPlayerData);
            PlayerResources.SaveData();
        }

        public void Restart()
        {
            PlayerResources.SaveData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNext()
        {
            PlayerData playerData = PlayerResources.GetPlayerData;
            playerData.Level++;
            playerData.MoneyAmount = PlayerResources.GetPlayerData.MoneyAmount;


            int levelIndexToLoad = SceneManager.GetActiveScene().buildIndex + 1;

            if (levelIndexToLoad >= SceneManager.sceneCountInBuildSettings)
            {
                levelIndexToLoad = _levelToLoadAfterCompletingAllLevels;
            }

            PlayerResources.SetupData(playerData);
            PlayerResources.SaveData();
            SceneManager.LoadSceneAsync(levelIndexToLoad);
        }
    }
}


