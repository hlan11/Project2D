using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Golem : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    Rigidbody2D rb;
    private int maxHealth = 30;
    private int currentHealth;
    [SerializeField] private Slider EnemyHealthBar;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = currentHealth;
    }
    private void Update()
    {
        if (FacingRight())
        {
            rb.velocity = new Vector2(moveSpeed, 0f);
        }
        else
            rb.velocity = new Vector2(-moveSpeed, 0f);
    }
    bool FacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale=new Vector2(-(Mathf.Sign(rb.velocity.x)), transform.localScale.y);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EnemyHealthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Debug.Log("golem died");
            Destroy(gameObject);
        }
    }
}
