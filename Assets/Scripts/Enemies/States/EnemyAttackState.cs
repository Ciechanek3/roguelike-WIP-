using System;
using Settings;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : BaseState
{
    [SerializeField] private EnemyStats enemyStats;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override Type Tick()
    {
        if (enemyStats.CurrentHp <= 0)
        {
            return NextStateType;
        }

        navMeshAgent.destination = GameSettings.Instance.PlayerPosition.position;
        return null;
    }
}

    