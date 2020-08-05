using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPoses : MonoBehaviour
{
    [SerializeField] int animindex = 0;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("Index", animindex);
    }
}
