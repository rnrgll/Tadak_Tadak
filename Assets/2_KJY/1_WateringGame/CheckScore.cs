using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckScore : MonoBehaviour
{
    public int minRange;
    public int maxRange;
    private int _plantCount;
    private int _checkedPlantCount;
    
    [SerializeField] private MinigameBase _game;
    [SerializeField] private PlantSpawner _plantSpawner;
    [SerializeField] private UICanvas  _canvas;

    void Start()
    {
        _plantCount = _plantSpawner.plantCount;
    }

    void Update()
    {
        if (_checkedPlantCount == _plantCount)
        {
            _game.EndGame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if  (other.CompareTag("Plant"))
        {
            Plant plant = other.GetComponent<Plant>();
            int drops = plant.waterAmount;
            plant.Stop();
            if (drops >= minRange && drops <= maxRange)
            {
                _canvas.ActivateSuccessText();
                GameManager.Instance.AddScore(drops); // 스코어 직접 변경해도 됨
            }
            else if (drops < minRange)
            {
                _canvas.ActivateWiltText();
                GameManager.Instance.Life --;
                // 시드는 효과 여기서 주기
            }
            else if (drops > maxRange)
            {
                _canvas.ActivateOverwaterText();
                GameManager.Instance.Life --;
            }
            
            plant.isChecked = true;
            _checkedPlantCount++;
            Destroy(plant.gameObject, 3f);
            
        }
    }
}
