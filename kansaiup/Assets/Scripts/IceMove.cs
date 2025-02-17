using UnityEngine;

public class IceMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float slipperyFactor = 1.0f; // 滑る係数（1に近いほど長く滑る）

    private Vector3 velocity = Vector3.zero; // 現在の移動速度

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 入力があるときのみ、新しい移動方向を設定
        if (horizontal != 0 || vertical != 0)
        {
            Quaternion horizontalRotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
            Vector3 targetVelocity = horizontalRotation * inputDirection * moveSpeed;

            // 慣性を加味して速度を更新
            velocity = Vector3.Lerp(velocity, targetVelocity, 0.01f); // 0.1f の部分を増やすと滑りが弱くなる
        }

        // 滑る処理（徐々に減速）
        velocity *= slipperyFactor;

        // 実際に移動
        transform.position += velocity * Time.deltaTime;
    }
}
