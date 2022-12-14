using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : BaseState
{
    private Animator animator;

    private bool startedDying = false;
    private bool isDead = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public override Type Tick()
    {
        if (!startedDying)
        {
            startedDying = true;
            animator.Play("Die");
        }

        return null;
    }
    
    public void OnSpawnCompleted()
    {
        isDead = true;
    }
}
