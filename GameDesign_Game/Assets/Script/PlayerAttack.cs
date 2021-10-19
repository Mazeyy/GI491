using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : StateMachineBehaviour
{
     
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player_Controller.instance.movementBaseSpeed = 0;
        Debug.Log("playerspeed" + Player_Controller.instance.movementBaseSpeed);
    }

     
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
     
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player_Controller.instance.movementBaseSpeed = 5;
        Debug.Log("playerspeed" + Player_Controller.instance.movementBaseSpeed);
    }
    
}
