using UnityEngine;

public class LoopingMovement : MonoBehaviour
{
    // ワープする範囲
    public float minX = -10f; // 左端のx座標
    public float maxX = 10f;  // 右端のx座標

    void Update()
    {
        // 現在の位置を取得
        Vector3 position = transform.position;

        // 右端を超えた場合、左端に移動
        if (position.x > maxX)
        {
            position.x = minX;
        }
        // 左端を超えた場合、右端に移動
        else if (position.x < minX)
        {
            position.x = maxX;
        }

        // 新しい位置を設定
        transform.position = position;
    }
}
