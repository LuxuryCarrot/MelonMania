using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareFruit : MonoBehaviour
{
    public int stage; // 과일의 단계
    public GameObject[] nextStageFruitPrefabs; // 다음 단계의 과일 프리팹 배열

    void OnCollisionEnter2D(Collision2D collision)
    {
        CompareFruit otherFruit = collision.gameObject.GetComponent<CompareFruit>();
    
    
        // 같은 단계의 과일과 충돌한 경우
        if (otherFruit != null && otherFruit.stage == this.stage)
        {
            if (otherFruit != null && this.GetInstanceID() < otherFruit.GetInstanceID())
            {
                // 충돌 처리
                Vector3 spawnPosition = (transform.position + otherFruit.transform.position) / 2;
                int nextStage = stage;
    
                // 다음 단계의 과일 생성
                Instantiate(nextStageFruitPrefabs[nextStage], new Vector3(spawnPosition.x, spawnPosition.y + 0.1f, + spawnPosition.z) , Quaternion.identity);
    
                // 현재 과일과 충돌한 과일 제거
                Destroy(otherFruit.gameObject);
                Destroy(gameObject);
            }
    
        }
    }
}
