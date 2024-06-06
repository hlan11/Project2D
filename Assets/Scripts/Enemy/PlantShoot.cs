using UnityEngine;

public class PlantShoot : MonoBehaviour
{
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float playerCheckRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Collider2D player;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int damage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float rateOfFire;
    private Rigidbody2D rb;
    private Animator anim;
    private float _rate = 0;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>(); 
        anim=GetComponent<Animator>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCheck.position, playerCheckRadius);
    }
    private Collider2D CheckPlayer()
    {
        return Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, playerLayer);
    }
    private void Update()
    {
        if (CheckPlayer())
        {
            anim.SetTrigger("Attack");
            player = CheckPlayer();
            Shoot(player);
        }
    }
    private void Shoot(Collider2D player)
    {
        _rate -= Time.deltaTime;
        if (_rate > 0)
        {
            return;
        }
        var playerPosition = player.transform.position;
        var diffX = playerPosition.x - transform.position.x;
        var bulletForceX = (diffX > 0f ? 1f : -1f) * bulletSpeed;
        var bulletForce = new Vector2(bulletForceX, 0f);
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.Shoot(bulletForce, damage);
        _rate = rateOfFire;
    }
}
