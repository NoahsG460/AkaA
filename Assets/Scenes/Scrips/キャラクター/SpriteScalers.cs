using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    [SerializeField] private Vector3 fixedScale = new Vector3(0.1f, 0.1f, 1f); // 固定するスケール

    void LateUpdate()
    {
        // スケールを常に固定（アニメーションの影響を受けた後に調整）
        if (transform.localScale != fixedScale)
        {
            transform.localScale = fixedScale;
        }
    }
}

