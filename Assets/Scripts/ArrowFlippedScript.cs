using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFlippedScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private int arrowDamage;
    private float lifetime;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    void Start()
    {
        lifetime = 0;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        arrowDamage = Inventory.Instance.GetRanged().Damage;
    }


    private void Update()
    {
        rb.velocity = new Vector2(-speed,0);
        
        lifetime += Time.fixedDeltaTime;
        if (lifetime > resetTime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Mob1>().TakeDamage(arrowDamage);
        }
        Destroy(gameObject);
    }
}
