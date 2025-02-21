using UnityEngine;
using System.Collections;

public class tama : MonoBehaviour
{
    public GameObject ballPrefab; // 発射する球のプレハブ
    public float spawnInterval = 3f; // 発射間隔
    public float launchForce = 100f; // 発射力
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
        if (ballPrefab == null) return;

        launchForce = Randommethod(100f,1000f);
        newX = Randommethod(-30f,10f);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // 球を生成
        GameObject ball = Instantiate(ballPrefab, transform.position, transform.rotation);
        
        // Rigidbodyを取得して力を加える
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
        }
    }
    
    private float Randommethod(float min, float max)
    {
        float random = Random.Range(min,max);
        return random;
    }
}
