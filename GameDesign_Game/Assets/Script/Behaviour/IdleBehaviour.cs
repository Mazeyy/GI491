using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{


    public float timer;
    public float minTime;
    public float maxTime;
    public float State_trigger;
    public float State_trigger_rate = 0.2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 5)
        {
           if(State_trigger < Time.time)
           {
                Boss.thisboss.Attack();
                Boss.thisboss.Idle = false; //start attacking
                Debug.Log(timer);
                State_trigger = Time.time + State_trigger_rate;
           }                               
                      
        }
        else
        {            
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {       
    }

}
