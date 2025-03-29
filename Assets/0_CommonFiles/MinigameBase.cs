using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameBase : MonoBehaviour
{
        public abstract void StartGame();
        public abstract void EndGame();

        /// <summary>
        /// 현재 미니게임을 종료하고, 게임매니저에 접근해 다음 미니게임을 로드합니다.
        /// </summary>
        /// <param name="score"></param>
        protected void CompleteGame()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.NextMinigame();
                Debug.Log("다음게임 로드함 ㅇㅇ");
            }
        }
    }
