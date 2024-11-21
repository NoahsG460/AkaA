using UnityEngine;

public class PlayerLoop : MonoBehaviour
{
    public float leftBoundary = -10f;  // 左端の位置
    public float rightBoundary = 10f; // 右端の位置

    void Update()
    {
        // キャラクターの現在の位置
        Vector3 position = transform.position;

        // 右端を超えた場合、左端にワープ
        if (position.x > rightBoundary)
        {
            position.x = leftBoundary;
        }
        // 左端を超えた場合、右端にワープ
        else if (position.x < leftBoundary)
        {
            position.x = rightBoundary;
        }

        // ワープした位置を適用
        transform.position = position;
    }
}
