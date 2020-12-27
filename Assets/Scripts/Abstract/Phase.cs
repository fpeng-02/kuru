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
    public abstract IEnumerator phaseLoop();  // phaseLoop will have an inf loop; next phase will interrupt it with StopAllCoroutines
    public virtual void exitPhase()
    {
        StopAllCoroutines();
    }
}