using System;
using System.Collections;
using System.Collections.Generic;
using Settings;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnState : BaseState
{
    private Animator animator;
    
    private bool isSpawned = false;
    private bool startedSpawning = false;

    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override Type Tick()
    {
        if (!startedSpawning)
        {
            startedSpawning = true;
            animator.Play("Spawn");
        }

        if (isSpawned)
        {
            return NextStateType;
        }

        return null;
    }
    
    public void OnSpawnCompleted()
    {
        isSpawned = true;
    }
}
