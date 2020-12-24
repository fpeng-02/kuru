using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class StateMachine
{
    protected IState currentState;
    
    public void SetState(IState state)
    {
        this.currentState = state;
        //StartCoroutine(state.startState());
    }
}
