using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Witch : MonoBehaviour
{
    private Animator anim;
    public LayerMask PlayerLayer;
    [SerializeField] public Transform PlayerAttackRange;
    [SerializeField] public float PlayerAttackRadius = 0.5f;
    [SerializeField] private Slider WitchHealthBar;
    private Rigidbody2D rb;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
    }
    Collider2D CheckPlayer()
    {
        return Physics2D.OverlapCircle(PlayerAttackRange.position, PlayerAttackRadius, PlayerLayer);
    }
    public void Update()
    {
        if (CheckPlayer())
        {
            anim.SetTrigger("Attack");
        }   
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(PlayerAttackRange.position, PlayerAttackRadius);
    }
}
