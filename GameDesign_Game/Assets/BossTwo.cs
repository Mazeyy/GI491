using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwo : MonoBehaviour
{
    public bool isFlipped = false;

    public Transform PlayerPos;
    private Vector2 movementDirection;
    public bool isMoving;

    public float attackDamage;
    public float enragedAttackDamage;

    public Vector3 attackOffset;
    public float attackRange;
    public LayerMask attackMask;

    public float health = 800;
    public GameObject BossCyclop;
    public bool IsInvulnerable = false;
    public float speed;

    Rigidbody2D rbBoss;
    Transform player;
    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > PlayerPos.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < PlayerPos.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }

    }

    /*public void LookAtPlayerTop()
    {
        Vector3 flipped = transform.localScale;
        flipped.y *= -1f;

        if (transform.position.x > PlayerPos.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(180f, 0f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < PlayerPos.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(180f, 0f, 0f);
            isFlipped = true;
        }
    }*/



    //Event Attack
    /*public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player_Stats>().Health(attackDamage);
        }
    }*/

    //IsCharge
    //public void TakeDamage(float damage)
    //{
    // if (Vector2.Distance(player.position, rb.position) >= attackRange)
    //{
    //  FollowPlayer();
    //}

    /*if (IsInvulnerable)
    {
        return;

        health -= damage;

        if(health <= 200)
        {
            GetComponent<Animator>().SetBool("IsCharge", true);
        }
        if (health <= 0)
        {
            Die();
        }
    }*/
    //}

    void FollowPlayer()
    {
        if (Player_Stats.PlayerStats.Health > 0)
        {
            if (Vector2.Distance(player.position, rb.position) >= attackRange)
            {
                anim.SetBool("IsCharge", true);
                //target = new Vector2(PlayerPos.position.x, PlayerPos.position.y);
                rbBoss = GetComponent<Rigidbody2D>();
                Vector2 moveDirection = (PlayerPos.transform.position - transform.position).normalized * speed;
                //this.transform.Rotate(0, 0, Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg);
                rbBoss.velocity = new Vector2(moveDirection.x, moveDirection.y);
            }
         }


            /*if (Player_Stats.PlayerStats.Health > 0)
            {
                if (Vector2.Distance(transform.position, PlayerPos.position) <= Range)
                {
                    isMoving = true;
                    movementDirection = (PlayerPos.transform.position - transform.position).normalized;
                    transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, movementSpeed * Time.deltaTime);
                }
            }
            else
            {
                isMoving = false;
            }*/
     }
}
