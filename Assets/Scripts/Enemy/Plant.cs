using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [Header("Plant")]
    private Animator anim;
    public LayerMask PlayerLayer;
    [SerializeField] public Transform PlayerAttackRange;
    [SerializeField] public float PlayerAttackRadius = 0.5f;
    private int maxHealth = 50;
    private int currentHealth;
    [SerializeField] private Slider EnemyHealthBar;
     void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EnemyHealthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PlayerAttackRange.position, PlayerAttackRadius);
    }
    private Collider2D CheckPlayer()
    {
        return Physics2D.OverlapCircle(PlayerAttackRange.position, PlayerAttackRadius, PlayerLayer);
    }
}
