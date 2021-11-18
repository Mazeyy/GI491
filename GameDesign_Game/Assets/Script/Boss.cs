using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [Space]
    [Header("Stat :")]
    public float MaxHealt;
    public float Health;
    public float AttackDMG;
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
    public float fireRate = 0.5f;
    private float nextFire;
    public float attackRate;
    public bool canShoot = true;

    private float dist;
    public float howClose;

    public bool isFlipped = false;

    public static Boss thisboss;

    public Slider HealthBar;

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

        //HealthBar.minValue = 0;
        HealthBar.maxValue = MaxHealt;
        //HealthBar.value = Health;
        //HealthBar.wholeNumbers = true;
    }
     void Update()    
    {
        if(Health >= 0)
        {
            HealthBar.value = 0;
        }
        HealthBar.value = Health;

        if(Player_Stats.PlayerStats.Health > 0)
        {
            dist = Vector2.Distance(PlayerPos.position, transform.position);
        }        
        
        if (Health <= 100)
        {
            anim.SetTrigger("StateThree");
            Idle = true;

            if (StartSummon == true)
            {
                SpawnObjectAtRandom();
                
            }
        }
        HealthBar.value = Health;
    }

    /*public void LookAtPlayer()
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
    }*/

    public void Attack()
    {
        if(attackRate < Time.time)
        {        
            
            if (dist > howClose && currenAttacking == false && canShoot == true)
            {
                StartCoroutine(Shoot());           
                
            }
            if (dist < howClose && currenShooting == false)
            {
                anim.SetTrigger("Melee");                
            }

            attackRate = Time.time + fireRate;
        }

    }

    public IEnumerator Shoot()
    {              
        
        anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.2f);
        if (nextFire < Time.time)
        {
            Instantiate(BossBulletPf, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
        canShoot = true;
        
    }

    /*public IEnumerator MeleeAttack()
    {
        anim.SetTrigger("Melee");
        yield return new WaitForSeconds(nextFire)
        {

        }
    }*/

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
            StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    {
        
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
        TeleportDrop();
        
    }

    void TeleportDrop()
    {
            Vector2 position = transform.position;
            GameObject Teleport = Instantiate(TeleportPrefab, position, Quaternion.identity);

    }

}
