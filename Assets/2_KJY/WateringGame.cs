using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringGame : MinigameBase
{
    [SerializeField] private PlantSpawner _plantSpawner;
    
    public override void StartGame()
    {
        _plantSpawner.SpawnPlant();
    }

    // 종료조건 : 마지막 화분의 판정이 끝났을 때
    public override void EndGame()
    {
        CompleteGame();
    }
}
