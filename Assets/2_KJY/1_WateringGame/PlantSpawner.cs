using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _plantPrefabs;
    public int plantCount { get;  private set; }
    [SerializeField] private Transform _spawnPos; //   불필요. 자기자신거 쓰면 됨
    [SerializeField] private float _interval;
    [SerializeField] private bool _isOn;
    [SerializeField] private CheckScore _checkScore;
    
    // 화분 이동 관련 로직
    [SerializeField] private float _defaultSpeed = 5f; // 기본 이동 속도
    [SerializeField] private float _minSpeed = 0.5f; // 최저 속도
    [SerializeField] private float _breakRate = 2f; // 감속 계수 (클수록 감속이 더 빨라짐)
    [SerializeField] private float _accelerationRate = 2f; // 가속 계수 (클수록 빠르게 원속도로 복귀)
    
    // UI 관련
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private float _dropCountOffsetX; // 현재 물방울 수에 대한 X방향 오프셋
    [SerializeField] private float _dropCountOffsetY; // 현재 물방울 수에 대한 Y방향 오프셋
    
    void Awake()
    {
        plantCount = _plantPrefabs.Count;
        // Debug.Log(_plantCount);

        for (int i = 0; i < plantCount; i++)
        {
            var plant = _plantPrefabs[i].GetComponent<Plant>();
            plant.canvas  = _mainCanvas; 
            plant.checkScore  = _checkScore; 
            plant.defaultSpeed = _defaultSpeed;
            plant.minSpeed = _minSpeed;
            plant.breakRate = _breakRate;
            plant.accelerationRate = _accelerationRate;
            plant.dropCountOffsetX = _dropCountOffsetX;
            plant.dropCountOffsetY = _dropCountOffsetY;
        }
    }

    public void SpawnPlant()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        for (int i = 0; i < plantCount; i++)
        {
            Instantiate(_plantPrefabs[i], _spawnPos.position, Quaternion.identity).GetComponent<Plant>().canvas  = _mainCanvas; ;
            yield return new WaitForSeconds(_interval);
        }
    }
}
