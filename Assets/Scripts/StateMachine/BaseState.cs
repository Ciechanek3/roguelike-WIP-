using System;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeField] private BaseState nextState;

    protected Type NextStateType => nextState.GetType();
    public abstract Type Tick();
}
