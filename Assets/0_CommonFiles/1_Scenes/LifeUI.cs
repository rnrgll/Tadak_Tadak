using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _lives; // 목숨을 나타내는 프리팹

    void Start()
    {
        UpdateLifeUI(GameManager.Instance.Life);
    }

    public void UpdateLifeUI(int lifeCount)
    {
        for (int i = 0; i < lifeCount; i++)
        {
            _lives[i].SetActive(true);
        }

        for (int i = lifeCount; i < _lives.Length; i++)
        {
            _lives[i].SetActive(false);
        }
    }
}
