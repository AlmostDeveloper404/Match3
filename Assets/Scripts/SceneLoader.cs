using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

namespace Main
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private PlayerData _defaultData;
        [SerializeField] private int _firstLevelToRepeat = 1;

        private IEnumerator Start()
        {
            yield return Helpers.Helper.GetWait(1f);
            PlayerData playerData = SaveLoadProgress.LoadData<PlayerData>();
            if (playerData.Equals(default(PlayerData)))
            {
                playerData = _defaultData;
            }
            PlayerResources.SetupData(playerData);

            int allScenesCount = SceneManager.sceneCountInBuildSettings - 1;
            int startLevel = _firstLevelToRepeat - 1;
            int levelCount = allScenesCount - startLevel;
            int remainder;

            if (playerData.Level > allScenesCount)
            {
                remainder = (playerData.Level - startLevel - 1) % levelCount + startLevel + 1;
            }
            else
            {
                remainder = (playerData.Level - 1) % allScenesCount + 1;
            }

#if !UNITY_EDITOR && UNITY_WEBGL

            if (Yandex.IsMobile())
            {
                SetQuality(0);
            }
            else
            {
                SetQuality(1);
            }

#endif
            SceneManager.LoadSceneAsync(remainder);
        }

        public void SetQuality(int value)
        {
            QualitySettings.SetQualityLevel(value);
        }

        [ContextMenu("Delete Data")]
        private void DeleteData()
        {
            Debug.Log("Saves Deleted!");
            SaveLoadProgress.DeleteData();
        }
    }



}

