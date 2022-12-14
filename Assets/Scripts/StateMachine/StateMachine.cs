using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField]
        private BaseState currentState;

        [SerializeField]
        private BaseState firstState;

        private Dictionary<Type, BaseState> availableStates;

        public BaseState CurrentState { get => currentState; set => currentState = value; }
        public Dictionary<Type, BaseState> AvailableStates { get => availableStates; set => availableStates = value; }

        public event Action<BaseState> OnStateChanged;
        public event Action<Dictionary<Type, BaseState>> OnListOfStatesCreated;

        public void SetStates(Dictionary<Type, BaseState> states)
        {
            AvailableStates = states;
        }

        private void OnEnable()
        {
            CurrentState = firstState;
            OnListOfStatesCreated?.Invoke(AvailableStates);
        }

        private void FixedUpdate()
        {
            var nextState = CurrentState?.Tick();
            if (nextState != null &&
                nextState != CurrentState?.GetType())
            {
                SwitchToNewState(nextState);
            }
        }

        public void SwitchToNewState(Type nextState)
        {
            CurrentState = AvailableStates[nextState];
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}
