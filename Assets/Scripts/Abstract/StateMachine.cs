using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;
    
    public void SetState(State state)
    {
        this.currentState = state;
        StartCoroutine(state.startState());
    }
}
