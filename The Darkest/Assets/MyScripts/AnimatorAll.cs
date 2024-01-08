using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorAll : MonoBehaviour
{
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();   
                if(_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }
}
