using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Main
{
    public class ToggleSound : MonoBehaviour
    {

        [SerializeField] private Button _soundButton;
        [SerializeField] private Image _soundImage;
        [SerializeField] private Sprite _enableImage;
        [SerializeField] private Sprite _disableSound;

        [SerializeField] private AudioClip _uiSound;

        private SoundManager _soundManager;

        [Inject]
        private void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        private void OnEnable()
        {
            Setup();
            _soundButton.onClick.AddListener(Toggle);
        }

        private void OnDisable()
        {
            _soundButton.onClick.RemoveAllListeners();
        }

        private void Setup()
        {
            PlayerData playerData = PlayerResources.GetPlayerData;
            _soundImage.sprite = playerData.SoundSwitcher == 0 ? _enableImage : _disableSound;
        }

        public void Toggle()
        {
            PlayerData playerData = PlayerResources.GetPlayerData;
            playerData.SoundSwitcher = playerData.SoundSwitcher == 0 ? 1 : 0;
            AudioListener.volume = playerData.SoundSwitcher == 0 ? 1 : 0;
            _soundManager.PlaySound(_uiSound);
            PlayerResources.SetupData(playerData);
            Setup();
        }
    }
}


