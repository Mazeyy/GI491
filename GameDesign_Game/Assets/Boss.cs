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
    private float currentTimeToSpawn;
    public float spawnRadius = 1;

    [Space]
    [Header("Shoot :")]
    public float fireRate = 1f;
    private float nextFire;


    /*public Vector2 velocity = new Vector2(0, 0);
    public GameObject player;

    public float damage;*/

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = MaxHealt;

        anim = GetComponent<Animator>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;

        //StartCoroutine(SpawnThron());
    }

     void Update()
    //ถ้า Boss เหลือเลือดเท่าที่กำหนด ก็เปลี่ยน state
    {
        if (Health <= 350)
        {
            anim.SetTrigger("StateTwo");
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
                
            }


        }
        if (Health <= 100)
        {
            anim.SetTrigger("StateThree");

            if(currentTimeToSpawn > 0)
            {
                currentTimeToSpawn -= Time.deltaTime;
            }
            else
            {
                SpawnObjectAtRandom();
                
                //Instantiate(ThronPrefab, transform.position, Quaternion.identity);

                currentTimeToSpawn = TimeToSpawn;
                //Destroy(ThronPrefab, 0.5f);
            }
        }
    }

    void SpawnObjectAtRandom()
    {
        Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
        Instantiate(ThronPrefab, randomPos, Quaternion.identity);
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

        if (other.tag == ("Weapon"))
        {
            if (knockCooldown <= 0)
            {
                StartCoroutine(Knock());
                Vector2 direction = (this.transform.position - other.transform.position);
                transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y * knockbackPower);
            }
        }
    }

    IEnumerator Knock()
    {
        knockCooldown = 0.4f;
        yield return new WaitForSeconds(knockCooldown);
        knockCooldown = 0;
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
