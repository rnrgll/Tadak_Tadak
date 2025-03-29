using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public List<GameObject> minigamePrefabs; // 미니게임 프리팹 리스트
    public Transform minigameContainer; // 미니게임이 로드될 부모 오브젝트

    private GameObject curMinigame; // 현재 미니게임 프리팹

    void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.minigameManager = this;
        }
        
        GameManager.Instance.totalMiniGames = minigamePrefabs.Count;
    }
    
    public void LoadMinigame(int index)
    {
        Debug.Log("Loading minigame " + index);
        if (curMinigame != null)
        {
            Destroy(curMinigame);
        }

        GameObject minigamePrefab = minigamePrefabs[index];
        curMinigame = Instantiate(minigamePrefab, minigameContainer);
        StartCoroutine(SafeStart(curMinigame));
    }

    /// <summary>
    /// 객체 초기화 완료 후 게임을 시작할 수 있도록 한 프레임 지연시행
    /// </summary>
    /// <returns></returns>
    private IEnumerator SafeStart(GameObject minigame)
    {
        yield return null;
        minigame.GetComponent<MinigameBase>().StartGame();
    }
}
