using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ���̃I�u�W�F�N�g�������̃L�����N�^�[�Ȃ瑀��\�ɂ���
        if (photonView.IsMine)
        {
            Movement();
        }
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }
}
