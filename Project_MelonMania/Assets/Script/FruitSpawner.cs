using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // 과일 프리팹 배열
    public Transform spawnPoint; // 과일이 소환될 위치
    private bool isSpawning = false;

    private GameObject SpawnedFruit;
    void Start()
    {
        // 첫 과일 소환
        SpawnRandomFruit();
    }

    void Update()
    {
        // 마우스 버튼을 누르고 있을 때
        if (Input.GetMouseButton(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = new Vector3(worldPoint.x, spawnPoint.position.y, spawnPoint.position.z);

            spawnPoint.position = newPosition;
            // 과일이 소환된 상태라면 과일의 위치 업데이트
            if (SpawnedFruit != null)
            {
                SpawnedFruit.transform.position = newPosition;
            }
            else
            {
                // 처음 마우스를 눌렀을 때만 과일 소환
                if (Input.GetMouseButtonDown(0))
                {
                    spawnPoint.position = newPosition;
                    SpawnRandomFruit();
                }                
            }

            
            
        }

        // 마우스 버튼을 놓았을 때
        if (Input.GetMouseButtonUp(0) && !isSpawning)
        {
            DropFruit();
        }
    }

    void SpawnRandomFruit()
    {
        // 랜덤한 과일 소환
        int randomIndex = Random.Range(0, fruitPrefabs.Length);
        GameObject fruit = Instantiate(fruitPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        SpawnedFruit = fruit;
        // Rigidbody 2D를 비활성화하여 과일이 고정되도록 함
        Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void DropFruit()
    {
        // Coroutine 시작
        StartCoroutine(DropAndSpawn());
    }
    
    IEnumerator DropAndSpawn()
    {
        isSpawning = true;
        // 모든 과일을 떨어뜨림
        foreach (Rigidbody2D rb in FindObjectsOfType<Rigidbody2D>())
        {
            rb.isKinematic = false;
        }

        // 0.5초 동안 기다림
        yield return new WaitForSeconds(0.5f);

        // 새 과일 소환
        SpawnRandomFruit();
        
        isSpawning = false;
    }
}