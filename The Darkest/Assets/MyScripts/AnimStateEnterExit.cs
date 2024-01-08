using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateEnterExit : StateMachineBehaviour
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsResting", true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.anyKeyDown)
        {
            animator.SetBool("IsResting", false);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
