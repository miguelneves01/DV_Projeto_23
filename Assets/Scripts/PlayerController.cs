using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _jumpPower = 2.0f;
    [SerializeField] private float _runSpeed = 0.5f;
    private SpriteRenderer sprite;
    private bool grounded = true;
    private Animator anim;
    private enum MovementState {idle, running, jumping, falling, mAttack, rAttack}
    private float horizontalInput;
    private float attackLock =1f;
    private bool attacking = false;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask whatIsEnemy;

    private int playerDamage;
    [SerializeField] private Transform launchPos;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowPrefabFlipped;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerDamage = Inventory.Instance.GetMelee().Damage;
    }

    // Update is called once per frame
    private void Update()
    {    
        attackLock += Time.deltaTime;
        if (attackLock < 0.5f)
        {
            attacking = true;
        }else{
            attacking = false;
            horizontalInput = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector2(horizontalInput * _runSpeed, _rb.velocity.y);
            if (Input.GetKey(KeyCode.Space) && grounded && !attacking)
                Jump();
            
            UpdateAnimationState();
        }


    }
    
    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void UpdateAnimationState(){
        MovementState state;
        if(horizontalInput > 0f){
            state = MovementState.running;
            sprite.flipX = false;
            attackPos.localPosition = new Vector2(Mathf.Abs(attackPos.localPosition.x), attackPos.localPosition.y);
            launchPos.localPosition = new Vector2(Mathf.Abs(launchPos.localPosition.x), launchPos.localPosition.y);

        }else if(horizontalInput < 0f){
            state = MovementState.running;
            sprite.flipX = true;
            attackPos.localPosition = new Vector2(-Mathf.Abs(attackPos.localPosition.x), attackPos.localPosition.y);
            launchPos.localPosition = new Vector2(-Mathf.Abs(launchPos.localPosition.x), launchPos.localPosition.y);

        }else{
            state = MovementState.idle;
        }
        if(_rb.velocity.y > 0.1f){
            state = MovementState.jumping;
        }else if(_rb.velocity.y < -0.1f){
            state = MovementState.falling;
        }
        if (Input.GetKey(KeyCode.Mouse0) && grounded && attackLock > 0.8f){
            state = MovementState.mAttack;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemy);
            for(int i = 0; i < enemiesToDamage.Length;i++){
                enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(playerDamage);
            }
            attackLock = 0f;
        }
        if (Input.GetKey(KeyCode.Mouse1) && grounded && attackLock > 0.8f){
            state = MovementState.rAttack;
            StartCoroutine("BowAttack");
            attackLock = 0f;
        }
        anim.SetInteger("state", (int)state);
    }

    private IEnumerator BowAttack()
    {
        yield return new WaitForSeconds(0.45f);
        if (sprite.flipX){
                Instantiate(arrowPrefabFlipped, launchPos.position, arrowPrefabFlipped.transform.rotation);
        }else{
                Instantiate(arrowPrefab, launchPos.position, arrowPrefab.transform.rotation);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }
}
