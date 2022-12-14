using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private int startingHp;

    public int CurrentHp { get; private set; }

    private void Awake()
    {
        CurrentHp = startingHp;
    }
}
