using System;
using UnityEngine;

namespace Main
{
    public enum GameState { Reset, LevelCompleted, LevelFailed}


    public static class GameManager
    {
        public static event Action OnReseting;
        public static event Action OnLevelFailed;
        public static event Action OnLevelCompleted;


        public static void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Reset:
                    OnReseting?.Invoke();
                    break;
                case GameState.LevelFailed:
                    OnLevelFailed?.Invoke();
                    break;
                case GameState.LevelCompleted:
                    OnLevelCompleted?.Invoke();
                    break;
                default:
                    break;
            }
        }
    }
}

