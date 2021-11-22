using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviour : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    BossTwo bossTwo;

    public float speed;
    public float attackRange;

    private int randAnim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        bossTwo = animator.GetComponent<BossTwo>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossTwo.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("StateBlunt");
        }
        else if (Vector2.Distance(player.position, rb.position) > attackRange)
        {
            randAnim = Random.Range (1,3);
            Debug.Log ("rand = " + randAnim);

            if(randAnim == 1)
            {
                animator.SetTrigger("Jump");
            }
            else
            {
                animator.SetTrigger("Charge");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("StateBlunt");

        animator.ResetTrigger("Jump");

        animator.ResetTrigger("Charge");
    }

}
