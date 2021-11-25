using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Shoot : MonoBehaviour
{
    public static Enemy_Shoot EnemyShoot;

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
    public GameObject FloatingTexttPrefab;

    public float MaxHealth;
    public float Health;
    public GameObject HeartPrefab;
    public GameObject PotionPrefab;
    private int randDrop;

    public GameObject Tutorial;

    private void Start()
    {
        Health = MaxHealth;
    }

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

        if (Player_Stats.PlayerStats.Health > 0)
        {
            dist = Vector2.Distance(PlayerPos.position, transform.position);
        }

        if (dist < howClose && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
        Tutorial.SetActive(true);

    }

    void Slime_Movement_type()
    {

    }

    public void Attack()
    {
       /* if (dist < howClose && canShoot == true)
        {
            Debug.Log("sss");
            StartCoroutine(Shoot());

        }*/
        if (dist < howClose)
        {
            Debug.Log("sss");
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

    public void DealDMG(float damage)
    {
        Health -= damage;
        print("Damage Taken Shoot");
        if (FloatingTexttPrefab && Health > 0)
        {
            ShowFloatingTextt();
        }
        CheckDeath();
    }

    void CheckDeath()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            ItemDrop();
        }
    }

    void ShowFloatingTextt()
    {
        var go = Instantiate(FloatingTexttPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = Health.ToString();
    }

    void ItemDrop()
    {
        randDrop = Random.Range(1, 10);
        if (randDrop == 5)
        {
            Vector2 position = transform.position;
            GameObject Heart = Instantiate(HeartPrefab, position, Quaternion.identity);
            //GameObject Heart = Instantiate(HeartPrefab);
            //Heart.SetActive(true);
            Destroy(Heart, 5.0f);
        }

        if (randDrop == 2)
        {
            Vector2 position = transform.position;
            GameObject Potion = Instantiate(PotionPrefab, position, Quaternion.identity);
            Destroy(Potion, 5.0f);
        }

    }
    
}
