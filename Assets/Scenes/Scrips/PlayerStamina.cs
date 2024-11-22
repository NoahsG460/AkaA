using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float boostedSpeed = 6f; // シフトキーで速くなる速度
    public float jumpForce = 5f; // ジャンプ力を設定
    public bool IsGrounded { get; set; } // 地面に接地しているかの判定
    private bool isBoosting; // スピードアップ中かを判定
    private float currentSpeed; // 現在の移動速度
    Rigidbody2D rb;
    Animator animator;

    public float JumpForce => jumpForce; // ジャンプ力を公開

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // シフトキーでスピードアップ（地上にいるときのみ）
        if (IsGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            isBoosting = true;
        }
        else if (IsGrounded)
        {
            isBoosting = false;
        }

        // プレイヤーの移動
        Movement();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal"); // 横方向の入力 (A/Dキーや矢印キー)

        // 現在の速度を計算（地上でのみブースト適用）
        currentSpeed = isBoosting ? boostedSpeed : moveSpeed;

        // 向きの変更
        if (x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        animator.SetFloat("Speed", Mathf.Abs(x)); // アニメーションのスピード設定

        // 移動を適用
        rb.velocity = new Vector2(x * currentSpeed, rb.velocity.y);
    }
}
