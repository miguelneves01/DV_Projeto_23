using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{ 
    private Transform target;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float health;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private Transform attackPos;
    [SerializeField] private Transform groundPos;
    private int gold;
    private int xp;
    private float lastAttack = 3f;
    private float direction;
    private Rigidbody2D rb;
    private Animator anim;
    private bool dead= false;
    private float deathTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Gate").transform;

        gold = 10 * ExperienceSystem.Instance.CurrentLevel;
        xp = 2 * ExperienceSystem.Instance.CurrentLevel;

        GameObject player = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(groundPos.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
}

    // Update is called once per frame
    void Update()
    {
        if (target.position.x > transform.position.x){
            direction = 1f;
        }else{
            direction = -1f;
        }
        if (target.position.x + (attackRange *8.5)< transform.position.x && !dead){
            rb.velocity = new Vector2(direction * movementSpeed,rb.velocity.y);
            anim.SetInteger("AnimState", 2); // 2 = run
        }else{
            if(lastAttack >= attackCooldown && !dead){
                anim.SetInteger("AnimState", 0);
                anim.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemy);
                for(int i = 0; i < enemiesToDamage.Length;i++){
                    enemiesToDamage[i].GetComponent<GateScript>().TakeDamage(damage);
                }
                lastAttack = 0;
            }
            lastAttack += Time.deltaTime;
        }
        if (dead){
            deathTime += Time.deltaTime;
            if(deathTime > 2){
                Destroy(gameObject);
            }
        }
        
    }

    public void TakeDamage(int damage){
        anim.SetTrigger("Hurt");
        health -= damage;
        Debug.Log("Damage Taken");
        if (health <= 0){
            anim.SetTrigger("Death");
            CurrencySystem.Instance.AddCurrency(gold);
            ExperienceSystem.Instance.AddEXP(xp);
            dead = true;
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}
