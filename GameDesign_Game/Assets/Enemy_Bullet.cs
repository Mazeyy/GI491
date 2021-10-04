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

    /*void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,target,speed * Time.deltaTime);
        Vector2 currenPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currenPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currenPosition, newPosition);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other.CompareTag("Wall") || other.CompareTag("Player"))
            {
                if (other.GetComponent<Player_Stats>() != null)
                {
                    other.GetComponent<Player_Stats>().DealDMG(damage);
                }

                //if (other.GetComponent<Boss>() != null)
                //{
                //    other.GetComponent<Boss>().DealDMG(damage);
                //}
                Destroy(gameObject);
            }
        }
        transform.position = newPosition;
    }*/
}
