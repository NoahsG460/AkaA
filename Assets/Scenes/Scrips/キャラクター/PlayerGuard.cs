using System.Collections;
using UnityEngine;

public class PlayerGuard : MonoBehaviour
{
    public int maxGuardStock = 3;        // 最大ストック数
    public float guardRechargeTime = 5f; // ストックが1つ回復するまでの時間
    public float guardCooldown = 0.5f;   // ガード解除後の後隙（クールダウン）
    private int currentGuardStock;      // 現在のガードストック数
    private bool canGuard = true;       // ガード可能かどうか
    private float currentCooldown = 0f; // クールダウンのカウント

    private Animator animator;          // アニメーション制御用

    void Start()
    {
        currentGuardStock = maxGuardStock; // 初期ストック数を設定
        animator = GetComponent<Animator>();
        StartCoroutine(RechargeGuard());   // ストック回復コルーチンを開始

        Debug.Log("ガード機能が初期化されました: 現在のストック数 = " + currentGuardStock);
    }

    void Update()
    {
        // ガード入力と処理
        if (Input.GetKeyDown(KeyCode.L) && canGuard && currentGuardStock > 0)
        {
            Debug.Log("ガード入力を受け取りました");
            StartCoroutine(PerformGuard());
        }
        else if (Input.GetKeyDown(KeyCode.L) && currentGuardStock == 0)
        {
            Debug.Log("ガード入力を受け取りましたが、ストックが足りません！");
        }

        // 後隙のクールダウン処理
        if (!canGuard)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0f)
            {
                canGuard = true;
                Debug.Log("後隙が終了しました。ガードが再び可能です");
            }
        }
    }

    IEnumerator PerformGuard()
    {
        // ガードを実行
        currentGuardStock--;  // ストックを1つ消費
        canGuard = false;     // 後隙が発生するためガード不可
        currentCooldown = guardCooldown;

        if (animator != null)
        {
            animator.SetTrigger("IsGuard"); // ガードアニメーションを再生
        }

        Debug.Log("ガード発動！ 残りストック: " + currentGuardStock);

        // ガード中の処理（必要なら待機時間を設定可能）
        yield return new WaitForSeconds(0.3f);

        Debug.Log("ガードが終了しました。後隙が開始します");
    }

    IEnumerator RechargeGuard()
    {
        while (true)
        {
            if (currentGuardStock < maxGuardStock)
            {
                yield return new WaitForSeconds(guardRechargeTime);
                currentGuardStock++;
                Debug.Log("ガードストックが回復しました: 現在のストック数 = " + currentGuardStock);
            }
            else
            {
                yield return null; // 最大ストックなら何もしない
            }
        }
    }

    public int GetCurrentGuardStock()
    {
        return currentGuardStock;
    }

    public int GetMaxGuardStock()
    {
        return maxGuardStock;
    }
}
