using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGame : MinigameBase
{
    public override void StartGame()
    {
        Debug.Log("샘플게임 시작했당");
    }

    public override void EndGame()
    {
        CompleteGame();
    }
}
