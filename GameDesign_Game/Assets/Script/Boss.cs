using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Space]
    [Header("Stat :")]
    public float MaxHealt = 1.0f;
    public float Health = 1.0f;
    public float AttackDMG = 10f;
    public GameObject TeleportPrefab;
    public GameObject ThronPrefab;
    public GameObject BossBulletPf;
    public GameObject Player;
    private Transform PlayerPos;

    public Rigidbody2D rb;
    //public float health;

    public float knockbackPower = 0.001f;
    public float knockbackDuration = 0.7f;
    public float knockbackResistance = 0f;
    private float knockCooldown;
    public float StartknockCooldown = 1;

    private Animator anim;

    public float TimeToSpawn;
    public float currentTimeToSpawn;
    public float spawnRadius;
    
    public bool Idle;
    public bool StartSummon;
    public bool currenAttacking;
    public bool currenShooting;

    [Space]
    [Header("Shoot :")]
    public float fireRate = 1f;
    private float nextFire;

    private float dist;
    public float howClose;

    private int countRoot;

    public static Boss thisboss;

    /*public Vector2 velocity = new Vector2(0, 0);
    public GameObject player;

    public float damage;*/
    void Awake()
    {
        thisboss = this;
    }
    private void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        Health = MaxHealt;

        anim = GetComponent<Animator>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;       
    }
     void Update()
    
    {
        /*if (dist <= 1f)
        {
            anim.SetTrigger("Melee");
            if (currentTimeToSpawn > 0)
            {
                currentTimeToSpawn -= Time.deltaTime;
            }
            else
            {
                currentTimeToSpawn = TimeToSpawn;
            }
        }*/

        /*else
        {
            //anim.SetTrigger("Melee");
            currentTimeToSpawn = TimeToSpawn;
        }*/

        //if (Health <= 350)
        //{
        /*anim.SetTrigger("StateTwo");
        //BulletSpawner();            
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
            if (nextFire < Time.time)
            {
                Instantiate(BossBulletPf, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }            
        else
        {                
            currentTimeToSpawn = TimeToSpawn;

        }*/
        //}
        if (Health <= 100)
        {
            anim.SetTrigger("StateThree");
            Idle = true;

            if (StartSummon == true)
            {
                SpawnObjectAtRandom();
                
            }
        }
    }
    public void Attack()
    {
        if (Idle == false)
        {
            dist = Vector2.Distance(PlayerPos.position, transform.position);

            if (dist >= howClose && currenAttacking == false)
            {
                
                    if (nextFire < Time.time)
                    {
                        anim.SetTrigger("Shoot");
                        StartCoroutine(Shoot());
                    }
                    else
                    {
                        nextFire -= Time.deltaTime;
                    }
                
            }
            if (dist <= howClose && currenShooting == false)
            {
                anim.SetTrigger("Melee");                
            }
        }                  
    }

    public IEnumerator Shoot()
    {        
        yield return new WaitForSeconds(0.7f);
        Instantiate(BossBulletPf, transform.position, Quaternion.identity);
        nextFire = Time.time + fireRate;
    }

    void SpawnObjectAtRandom()
    {
        if (currentTimeToSpawn <= 0)
        {
            Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
            Instantiate(ThronPrefab, randomPos, Quaternion.identity);
            currentTimeToSpawn = TimeToSpawn;
        }
        else
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, spawnRadius);
    }*/

    public void DealDMG(float damage)
    {
        Health -= damage;
        print("Damage Taken");
        CheckDeath();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Player_Controller.instance.Knockback(knockbackDuration, knockbackPower, this.transform));
        }        
    }   

    void CheckDeath()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            TeleportDrop();
        }
    }

    void TeleportDrop()
    {
            Vector2 position = transform.position;
            GameObject Teleport = Instantiate(TeleportPrefab, position, Quaternion.identity);

    }

}
