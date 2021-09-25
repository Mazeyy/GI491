using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Space]
    [Header("CharacterAttributes:")]
    private float movementBaseSpeed = 1.0f;       

    [Space]
    [Header("CharacterStatistics:")]
    Vector2 CombatDirection;
    Vector2 movementDirection;    
    private float movementSpeed;        

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
    private bool candash = true;
    public float DashSPD;
    public float timeBTWdash;
    public float Dashtime;

    [Space]
    [Header("Melee")]
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform Attackpoint;
    public LayerMask whatIsEnemies;
    public float attackRange = 0.5f;
    public int damage = 10;

    [Space]
    [Header("Bullet")]
    public GameObject BulletPrefab;
    public float BulletSpd;
    public float minDamage;
    public float maxDamage;

    void Awake()
    {
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
        {
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
            movementDirection.Normalize();            
                        
        }
        
        Range_Attack = Input.GetButtonDown("Fire2");        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dashing();
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

    void Dashing()
    {
        if (candash == true)
        {
            StartCoroutine(Dash());  
        }              
    }   
    IEnumerator Dash()
    {
        candash = false;
        movementBaseSpeed = DashSPD;
        yield return new WaitForSeconds(Dashtime);
        movementBaseSpeed = 1;
        yield return new WaitForSeconds(timeBTWdash);
        candash = true;
    }
    //IEnumerator Attacking()
    //{
    //    movementBaseSpeed = 0;
    //    yield return new WaitForSeconds(0.3f);
    //    movementBaseSpeed = 1;
    //}

    void Attack()
    {
       
        if (timeBtwAttack <= 0)
        {            
            if (Input.GetButton("Fire1"))
            {
                //Animator.SetFloat("Hori_Slash", shootingDirection.x);     //MoveTo Shoot 
                //Animator.SetFloat("Vert_Slash", shootingDirection.y);
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = Vector2.MoveTowards(transform.position, mousePosition, 100f * Time.deltaTime);
                Animator.SetTrigger("Attack");
                Collider2D[] hitenemies = Physics2D.OverlapCircleAll(Attackpoint.position, attackRange, whatIsEnemies);

                for (int i = 0; i < hitenemies.Length; i++)
                {
                    hitenemies[i].GetComponent<Enemy_Stats>().DealDMG(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
                //StartCoroutine(Attacking());
            }            
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
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
        Animator.SetFloat("Hori_Slash", CombatDirection.x);
        Animator.SetFloat("Vert_Slash", CombatDirection.y);

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

