// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class WaterDropSpawner : MonoBehaviour
// {
//         private CustomPool<WaterDrop> _waterDropPool;
//         [SerializeField] private WaterDrop _waterDropPrefab;
//         [SerializeField] private int _poolSize;
//         
//         [SerializeField] private Transform[] _spawnPoints;
//         
//         public float baseRate = 0.5f; // 최소 생성 속도
//         public float maxRate = 5.0f; // 최대 생성 속도
//         public float growthSpeed = 2.0f; // 생성 속도 증가율
//         public float decaySpeed = 3.0f; // 감소 속도
//
//         private float holdTime = 0f;
//         private float spawnRate = 0f;
//         private float lastSpawnTime = 0f;
//
//         void Start()
//         {
//             _waterDropPool = new CustomPool<WaterDrop>(_poolSize, _waterDropPrefab);
//         }
//
//         void Update()
//         {
//             if (Input.GetKey(KeyCode.LeftShift)) // 버튼을 누르고 있을 때
//             {
//                 holdTime += Time.deltaTime;
//                 spawnRate = baseRate + maxRate * (1 - Mathf.Exp(-holdTime * growthSpeed));
//             }
//             else if (spawnRate > baseRate) // 버튼을 떼었을 때 서서히 감소
//             {
//                 spawnRate *= Mathf.Exp(-Time.deltaTime * decaySpeed);
//             }
//
//             // 일정한 간격으로 물방울 스폰
//             if (Time.time - lastSpawnTime >= 1f / spawnRate)
//             {
//                 SpawnWaterDrop();
//                 lastSpawnTime = Time.time;
//             }
//         }
//
//         void SpawnWaterDrop()
//         {
//             for (int i = 0; i < _spawnPoints.Length; i++)
//             {
//                 var target = _waterDropPool.Get();
//                 if (target != null)
//                 {
//                     target.transform.position = _spawnPoints[i].transform.position;
//                     target.SetPool(_waterDropPool);
//                 }
//             }
//         }
// }
