using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snail : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity=new Vector2(moveSpeed,rb.velocity.y);
    }
    private void OnCollisionEnetr2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
