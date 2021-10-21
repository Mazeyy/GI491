using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour
{    
    public float Range = 5F;
    public bool isMoving;
    private float movementSpeed;

    [Space]
    [Header("Statistics :")]
    private Vector2 movementDirection;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator Animator;
    public Transform Player;
    private Transform PlayerPos;
    public GameObject BulletPf;

    [Space]
    [Header("Shoot :")]
    public float fireRate = 0.5f;
    private float nextFire;
    public float attackRate;
    public bool canShoot = true;

    private float dist;
    public float howClose;

    public static Enemy_Shoot thisenemy;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;

        Animator = GetComponent<Animator>();

        thisenemy = this;
    }

    void Update()
    {
        Animate();
        FollowPlayer();
        Slime_Movement_type();
    }

    void Slime_Movement_type()
    {

    }

    public void Attack()
    {
        if (dist < howClose && canShoot == true)
        {
            StartCoroutine(Shoot());

        }
    }


    void FollowPlayer()
    {
        if (Player_Stats.PlayerStats.Health > 0)
        {
            if (Vector2.Distance(transform.position, PlayerPos.position) <= Range)
            {                
                movementDirection = (PlayerPos.transform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, movementSpeed * Time.deltaTime);
            }
        }        
    }

    void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            Animator.SetFloat("Horizontal", movementDirection.x);
            Animator.SetFloat("Vertical", movementDirection.y);
        }
    }

    public IEnumerator Shoot()
    {

        Animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.2f);
        if (nextFire < Time.time)
        {
            Instantiate(BulletPf, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
        canShoot = true;

    }
}
