﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : StateMachineBehaviour
{
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {               
        Boss.thisboss.currenAttacking = true;

    }    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        
    }   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boss.thisboss.currenAttacking = false;
    }

}