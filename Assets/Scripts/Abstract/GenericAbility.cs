using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericAbility : MonoBehaviour
{
    [SerializeField] private string abilityName;
    // intervals[i] is the time before attacks[i] is fired
    [SerializeField] protected List<GameObject> attacks;
    [SerializeField] protected List<float> intervals;
    [SerializeField] protected float spawnDistance;
    [SerializeField] protected int size;

    private void fireAttack(int index)
    {
        GameObject attack = getAttack(index);
        Vector3 spawnVector = getSpawnVector(index);
        Quaternion spawnAngle = getSpawnAngle(index);
        GameObject child = (GameObject)Instantiate(attack, spawnVector, spawnAngle);
        child.GetComponent<Damaging>().setOwner(this.transform.parent.gameObject.GetComponent<Entity>());
    }

    public IEnumerator cast()
    {
        size = Math.Max(size, attacks.Count); // if size was set in the inspector, it could be larger here?
        for (int i = 0; i < size; i++) {
            // wait for the interval, then actually fire
            yield return new WaitForSeconds(getInterval(i));
            fireAttack(i);
        }
        Destroy(this.gameObject);
    }

    public string getAbilityName() { return this.abilityName; }

    public abstract GameObject getAttack(int index);
    public abstract Vector3 getSpawnVector(int index);
    public abstract Quaternion getSpawnAngle(int index);
    public abstract float getInterval(int index);
}
