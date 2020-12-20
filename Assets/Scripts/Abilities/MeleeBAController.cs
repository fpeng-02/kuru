using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBAController : Ability, ICastable
{
    [SerializeField] private GameObject meleeAttack;
    [SerializeField] private float spawnDistance;
    private Vector3 spawnVec;

    public override void abilityAction()
    {
        // GameObject.Find always means "kind of hacky" imo
        Vector3 spawn = GameObject.Find("Player").GetComponent<Transform>().position + getSpawnVector();
        Quaternion spawnQuat = Quaternion.Euler(new Vector3(0, 0, rotationAngle()));
        GameObject child = (GameObject)Instantiate(meleeAttack, spawn, spawnQuat);
        child.GetComponent<MeleeBAHit>().setOwner(this.transform.parent.gameObject.GetComponent<Entity>());
        Destroy(this.gameObject);
    }

    public Vector3 getSpawnVector() { 
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.0F;
        Vector3 dirVector = Camera.main.ScreenToWorldPoint(mousePos) - this.transform.position;
        dirVector.z = 0;
        dirVector = dirVector.normalized;
        return dirVector * spawnDistance;
    }

    public float rotationAngle()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0.0F;
        Vector3 dirVector = Camera.main.ScreenToWorldPoint(mousePos) - this.transform.position;
        dirVector.z = 0;
        dirVector = dirVector.normalized;
        float angle = Mathf.Atan2(dirVector.y, dirVector.x) * Mathf.Rad2Deg;
        return angle;
    }
}
