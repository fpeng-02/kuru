using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class StateMachine : MonoBehaviour
{
    protected IState currentState;
    
    public void setState(IState state)
    {
        this.currentState = state;
        StartCoroutine(state.startState());
    }
}
