using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using System;

namespace Main
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timerText;


        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _loadNextButton;

        [SerializeField] private Animator _endPanalAnimator;

        private LevelController _levelController;
        private SoundManager _soundManager;
        [SerializeField] private AudioClip _winSound;
        [SerializeField] private AudioClip _lostSound;

        private float _timer;
        [SerializeField] private int _seconds;
        [SerializeField] private int _minutes;

        private TimeSpan _time;

        [Inject]
        private void Construct(LevelController levelController, SoundManager soundManager)
        {
            _levelController = levelController;
            _soundManager = soundManager;
        }

        private void OnEnable()
        {
            GameManager.OnLevelCompleted += LevelCompleted;
            GameManager.OnLevelFailed += GameOver;

            PlayerResources.OnPlayerDataChanged += UpdateScoreAmount;

            _restartButton.onClick.AddListener(_levelController.Restart);
            _loadNextButton.onClick.AddListener(_levelController.LoadNext);
        }

        private void OnDisable()
        {
            GameManager.OnLevelCompleted -= LevelCompleted;
            GameManager.OnLevelFailed -= GameOver;

            PlayerResources.OnPlayerDataChanged -= UpdateScoreAmount;

            _restartButton.onClick.RemoveAllListeners();
            _loadNextButton.onClick.RemoveAllListeners();
        }

        private void Start()
        {
            _time = new TimeSpan(0, _minutes, _seconds);
            _levelText.text = $"Level {PlayerResources.GetPlayerData.Level}";
            UpdateScoreAmount();
        }

        private void Update()
        {
            LaunchCountdown();
        }

        private void LaunchCountdown()
        {
            _timer += Time.deltaTime;
            if (_timer > 1f)
            {
                _timer = 0;
                _time -= new TimeSpan(0, 0, 1);
                if (_time <= TimeSpan.Zero)
                {
                    GameManager.ChangeGameState(GameState.LevelCompleted);
                }
                _timerText.text = $" {_time.Minutes.ToString("00")}:{_time.Seconds.ToString("00")}";
            }
        }

        private void UpdateScoreAmount()
        {
            _scoreText.text = $"{PlayerResources.GetLevelMoney}";
        }

        private void LevelCompleted()
        {
            _scoreText.text = $"+{PlayerResources.GetLevelMoney}";
            _soundManager.PlaySound(_winSound);
            _resultText.text = "WIN!";
            _endPanalAnimator.SetTrigger("Finish");
        }

        private void GameOver()
        {
            _scoreText.text = $"+{PlayerResources.GetLevelMoney}";
            _soundManager.PlaySound(_lostSound);
            _loadNextButton.gameObject.SetActive(false);
            _resultText.text = "LOST!";
            _endPanalAnimator.SetTrigger("Finish");
        }
    }
}


