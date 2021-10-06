using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0, 0);
    public GameObject target;
    private Transform PlayerPos;
    
    Rigidbody2D rbBullet;

    public float damage;
    public float speed;

    void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //target = new Vector2(PlayerPos.position.x, PlayerPos.position.y);
        rbBullet = GetComponent<Rigidbody2D>();
        Vector2 moveDirection = (PlayerPos.transform.position - transform.position).normalized * speed;
        this.transform.Rotate(0, 0, Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg);
        rbBullet.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(this.gameObject, 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
