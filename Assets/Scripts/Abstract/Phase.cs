using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Phase : MonoBehaviour
{
    [SerializeField] protected List<string> movesetNames;
    protected Entity owner;

    public void init(Entity owner) 
    {
        this.owner = owner;    
        StartCoroutine("beginPhase"); 
    }
    public abstract IEnumerator beginPhase();
    public abstract IEnumerator delayAction();
    public abstract IEnumerator attackAction();
}