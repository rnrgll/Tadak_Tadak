using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MinigameManager minigameManager;
    public UIManager uiManager;
    public SoundManager soundManager;
    
    public int totalMiniGames; // 총 게임수
    private int curGameIndex = 0; // 현재게임 인덱스

    private int _score; // 백킹필드
    public int Score
    {
        get => _score; // 읽을 때는 백킹필드의 값(_score)을 가져오고
        set // 값이 바뀔때마다 UI에 자동 반영
        {
            _score = value;
            if (uiManager != null)
            {
                uiManager.UpdateScore(_score);
            }
        }
    }
    
    private int _life; 
    public int Life
    {
        get => _life;
        set
        {
            _life = value;
            if (uiManager != null)
            {
                uiManager.UpdateLife(_life);
            }
        }
    }
    
    void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Init()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    /// <summary>
    ///  게임세션 시작 로직
    /// </summary>
    public void StartGameSession()
    {
        curGameIndex = 0;
        Score = 0;
        Life = 5;
        NextMinigame();
    }

    public void NextMinigame()
    {
        if (curGameIndex < totalMiniGames)
        {
            minigameManager.LoadMinigame(curGameIndex); //해당 인덱스의 게임을 로드하고
            curGameIndex++; // 인덱스 증가
        }
        else
        {
            EndGameSession(); // 인덱스가 게임 수보다 크거나 같다면, 게임 종료
        }
    }

    /// <summary>
    /// Score를 직접 변경해도 UI에 자동 반영되지만, 이 매서드를 호출해서 사용하셔도 됩니다.
    /// </summary>
    /// <param name="points">추가할 점수</param>
    public void AddScore(int points) 
    {
        Score += points;
        // uiManager.UpdateScore(Score);
    }

    /// <summary>
    /// 게임세션의 모든 스테이지를 마치고 정상종료
    /// </summary>
    public void EndGameSession()
    {
        if (uiManager != null)
        {
            uiManager.ShowGameOverScreen(Score);
        }
    }

    /// <summary>
    /// 연속 실패 등의 사유로 게임 중도 종료
    /// </summary>
    public void GameOver()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1) // 게임씬 인덱스
        {
            StartGameSession(); // 씬 전환 후 해당 씬의 오브젝트들의 Awake, OnEnable 완료 후 호출
        }
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
