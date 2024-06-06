using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private Animator anim;
    [Header("Player Move")]
    bool FacingRight = true;
    [SerializeField] private float MoveMent = 8f;
    [SerializeField] private float JumpForce = 11f;
    [SerializeField] private float multiplier = 5f;
    [Header("KnockBack")]
    [SerializeField] private Vector2 KnockBackDirection;
    private bool isKnock;
    [SerializeField] private float KnockBackTime;
    [Header("Move Sound")]
    [SerializeField] private AudioSource JumpSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Move()
    {
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * MoveMent, rb.velocity.y, 0);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce * multiplier);
            JumpSound.Play();
        }
    }
    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !FacingRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && FacingRight)
        {
            Flip();
        }
    }
    private void AnimationController()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        bool isJumping = rb.velocity.y > 0;
        anim.SetBool("isJumping", isJumping);
        bool CrouchKick = Input.GetKeyDown(KeyCode.C);
        anim.SetBool("CrouchKick", CrouchKick);
        anim.SetTrigger("Attack");
        bool NormalKick = Input.GetKeyDown(KeyCode.N);
        anim.SetBool("NormalKick", NormalKick);
        anim.SetTrigger("Attack");
        bool FlyingKick = Input.GetKeyDown(KeyCode.F);
        anim.SetBool("FlyingKick", FlyingKick);
        anim.SetTrigger("Attack");
        bool Punch = Input.GetKeyDown(KeyCode.P);
        anim.SetBool("Punch", Punch);
        anim.SetTrigger("Attack");
        bool Hurt = isKnock;
        anim.SetBool("Hurt", Hurt);
    }
    public void KnockBack()
    {
        isKnock = true;
        rb.velocity = new Vector2(KnockBackDirection.x, KnockBackDirection.y);
        Invoke("CancelKnockBack", KnockBackTime);
    }
    private void CancelKnockBack()
    {
        isKnock = false;
    }
    private void Update()
    {
        Move();
        Jump();
        AnimationController();
        FlipController();
    }
}
