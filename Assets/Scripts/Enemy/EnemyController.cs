using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Animator anim;
    public int maxHealth=50;
    public int currentHealth;
    [SerializeField] private Slider EnemyHealthBar;
    [SerializeField] private ParticleSystem Death;
    private void Awake()
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
            var effect = Instantiate(Death, transform.position, Quaternion.identity);
            effect.Play();
            Invoke("Die", 0.2f);
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
