using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Space]
    [Header("CharacterAttributes:")]
    private float movementBaseSpeed = 1.0f;
    public static Player_Controller instance;
    private bool CanMove = true;

    [Space]
    [Header("CharacterStatistics:")]
    Vector2 CombatDirection;
    Vector2 movementDirection;    
    public float movementSpeed;        

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator Animator;
    public GameObject CrossHair;
    public bool rangeCombat;    
    private bool Range_Attack;
    public GameObject ArrowCircle;

    [Space]
    [Header("Dash")]
    private float DashSpeed = 3f;
    public bool IsDashing;
    public float Dashtime;
    public float timeBTWdash;

    [Space]
    [Header("Melee")]
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform Attackpoint;
    public LayerMask whatIsEnemies;
    public float attackRange = 0.5f;
    public int damage = 10;
    public float knockbackPower = 0.005f;   

    [Space]
    [Header("Bullet")]
    public GameObject BulletPrefab;
    public float BulletSpd;
    public float minDamage;
    public float maxDamage;        

    void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {        
        ProcessInputs();
        Move();
        Animate();
        Shoot();
        //Aim();
        Attack();
    }

    private void ProcessInputs()
    {
        if (CanMove == true)
        {
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
            movementDirection.Normalize();
        }        

        Range_Attack = Input.GetButtonDown("Fire2");

        if (Input.GetButton("Fire1"))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsDashing == false)
        {
            StartCoroutine(Dashing());
        }
    }

    private void Move()
    {
        rb.velocity = movementDirection * movementSpeed * movementBaseSpeed;
    }

    private void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            Animator.SetFloat("Horizontal", movementDirection.x);
            Animator.SetFloat("Vertical", movementDirection.y);
        }       

        Animator.SetFloat("Speed", movementSpeed);       
    } 
    private IEnumerator Dashing()
    {                      
        IsDashing = true;
        movementBaseSpeed = movementBaseSpeed * movementSpeed * DashSpeed;
        yield return new WaitForSeconds(Dashtime);
        movementBaseSpeed = 1;
        yield return new WaitForSeconds(timeBTWdash);
        IsDashing = false;
    }

    void Attack()
    {       
        if (timeBtwAttack <= 0)
        {            
            if (Input.GetButton("Fire1"))
            {
                movementDirection = Vector2.zero;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 playerpos = transform.position;
                CrossHair.transform.localPosition = mousePosition;
                CombatDirection = mousePosition - playerpos;                ;
                CombatDirection.Normalize();                
                Animator.SetFloat("Hori_Slash", CombatDirection.x);
                Animator.SetFloat("Vert_Slash", CombatDirection.y);

                Animator.SetTrigger("Attack");

                //Collider2D[] hitenemies = Physics2D.OverlapCircleAll(Attackpoint.position, attackRange, whatIsEnemies);
                
                //for (int i = 0; i < hitenemies.Length; i++)
                //{
                //    hitenemies[i].GetComponent<Enemy_Stats>().DealDMG(damage);                    
                //}
                timeBtwAttack = startTimeBtwAttack;               
            }            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }   

    public IEnumerator Knockback(float knockbackDuration , float knockbackPower, Transform obj)
    {
        float timer = 0;
        while(knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * knockbackPower);            
        }
        yield return 0;
    }

    

    void OnDrawGizmosSelected()
    {
        if (Attackpoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackpoint.position, attackRange);
    }



    public void Shoot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerpos = transform.position;
        CrossHair.transform.localPosition = mousePosition;
        CombatDirection = mousePosition - playerpos;
        CombatDirection.Normalize();
        
        if (Range_Attack && rangeCombat == true)
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = CombatDirection * BulletSpd;
            bullet.GetComponent<bullet>().damage = Random.Range(minDamage, maxDamage);
            bullet.transform.Rotate(0, 0, Mathf.Atan2(CombatDirection.y, CombatDirection.x) * Mathf.Rad2Deg);
            Destroy(bullet, 3.0f);
        }        
    }
}

