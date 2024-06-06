using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    [Header("Attack Damage")]
    private int MaxHealth = 100;
    private int currentHealth;
    [SerializeField] private int CrouchKickDamage = 10;
    [SerializeField] private int NormalKickDamage = 20;
    [SerializeField] private int FlyingKickDamage = 35;
    [SerializeField] private int PunchDamage = 5;
    [Header("Attack Mana")]
    private int MaxMana = 20;
    private int currentMana;
    [SerializeField] private int CrouchKickMana = 4;
    [SerializeField] private int NormalKickMana = 3;
    [SerializeField] private int FlyingKickMana = 5;
    [SerializeField] private int PunchMana = 2;
    [Header("Attack Range")]
    public LayerMask EnemyLayer;
    [SerializeField] public Transform EnemyAttackRange;
    [SerializeField] public float EnemyAttackRadius = 0.5f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider ManaBar;
    public Collider2D myEnemy;
    [Header("Sound")]
    [SerializeField] private AudioSource PickUpSound;
    [SerializeField] private AudioSource DeathSound;
    [Header("Attack Sound")]
    [SerializeField] private AudioSource CrouchKickSound;
    [SerializeField] private AudioSource NormalKickSound;
    [SerializeField] private AudioSource FlyingKickSound;
    [SerializeField] private AudioSource PunchSound;
    private void Awake()
    {
        anim=GetComponent<Animator>();
        currentHealth = MaxHealth;
        currentMana = MaxMana;
    }
    public Collider2D CheckEnemy()
    {
        return Physics2D.OverlapCircle(EnemyAttackRange.position, EnemyAttackRadius, EnemyLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(EnemyAttackRange.position, EnemyAttackRadius);
    }
    public void Attack()
    {
        var enemy = CheckEnemy();
        //myEnemy = enemy;
        if ((bool)enemy)
        {
            anim.SetTrigger("Attack");
            if(Input.GetKeyDown(KeyCode.N))
            {
            DealDamageToEnemy(enemy, NormalKickDamage);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
            DealDamageToEnemy(enemy, FlyingKickDamage);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
            DealDamageToEnemy(enemy, CrouchKickDamage);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
            DealDamageToEnemy(enemy, PunchDamage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruits"))
        {
            PickUpSound.Play();
            currentMana += 5;
        }
        if (collision.CompareTag("AddPlayerHealth"))
        {
            PickUpSound.Play();
            currentHealth += 60;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            currentHealth -= 10;
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            currentHealth -= 20;
        }
    }
    private void DealDamageToEnemy(Collider2D enemy, int damage)
    {
        enemy.GetComponent<EnemyController>().TakeDamage(damage);
    }
    private void CheckMana()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NormalKickSound.Play();
            currentMana -= NormalKickMana;
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            FlyingKickSound.Play();
            currentMana -= FlyingKickMana;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            CrouchKickSound.Play();
            currentMana -= CrouchKickMana;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PunchSound.Play();
            currentMana -= PunchMana;
        }
        if (currentMana <= 0)
        {
            anim.SetTrigger("Idle");
        }
    }
    private void UpdateMana()
    {
        ManaBar.value = currentMana;
    }
    private void UpdateHealth()
    {
        healthBar.value = currentHealth;
    }
    void Died()
    {
        if (currentHealth <= 0)
        {
            bool Hurt = currentHealth <= 0;
            anim.SetBool("Hurt", Hurt);
            Destroy(gameObject);
            LoadLoseScence();
        }
    }
    void LoadLoseScence()
    {
        SceneManager.LoadScene(3);
    }
    private void Update()
    {
        CheckMana();
        UpdateMana();
        UpdateHealth();
        Attack();
        Died();
    }
}
