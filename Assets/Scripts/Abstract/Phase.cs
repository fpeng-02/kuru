using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Phase : MonoBehaviour
{
    [SerializeField] protected List<string> movesetNames;
    protected Entity owner;
    private bool phaseActive;

    public void setPhaseActive(bool phaseActive) { this.phaseActive = phaseActive; }
    public bool getPhaseActive() { return this.phaseActive; }

    public void init(Entity owner) 
    {
        this.owner = owner;
        setPhaseActive(true);
        StartCoroutine("beginPhase");
    }

    public abstract IEnumerator beginPhase();
    public abstract IEnumerator phaseLoop();  // phaseLoop will have an inf loop; next phase will interrupt it with StopAllCoroutines
    public virtual void exitPhase()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BossProjectile")) {
            Destroy(go);
        }
        setPhaseActive(false);
        StopAllCoroutines();
    }
}