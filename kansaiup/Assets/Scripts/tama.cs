using UnityEngine;
using System.Collections;

public class tama : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    public Transform spawnPoint; // 発射位置
    public float spawnInterval = 3f; // 発射間隔
    public float launchForce = 10f; // 発射力
    private float newX;

    private void Start()
    {
        StartCoroutine(SpawnBallRoutine());
    }

    private IEnumerator SpawnBallRoutine()
    {
        while (true)
        {
            SpawnBall();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBall()
    {
        if (ballPrefab == null || spawnPoint == null) return;

        launchForce = Randommethod(20,30);
        newX = Randommethod(-10,10);
        spawnPoint.position = new Vector3(newX, spawnPoint.position.y, spawnPoint.position.z);
        Debug.Log(spawnPoint);

        // 球を生成
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        
        // Rigidbodyを取得して力を加える
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(spawnPoint.forward * launchForce, ForceMode.Impulse);
            Debug.Log("ボールを投げる");
        }
    }
    
    private int Randommethod(int min, int max)
    {
        int random = Random.Range(min,max);
        return random;
    }
}
