using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    [SerializeField] private Vector3 fixedScale = new Vector3(0.1f, 0.1f, 1f); // 固定するスケール

    void Start()
    {
        // 初期スケールを設定
        transform.localScale = fixedScale;
    }

    void Update()
    {
        // スケールを常に固定
        if (transform.localScale != fixedScale)
        {
            transform.localScale = fixedScale;
        }
    }
}
