using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStrike : MonoBehaviour
{
    [SerializeField] private GameObject strike;
    [SerializeField] private float strikeDelay;
    private Entity owner;

    public void setOwner(Entity owner) { this.owner = owner; }

    void Start()
    {
        StartCoroutine("initialize");
    }

    IEnumerator initialize()
    {
        yield return new WaitForSeconds(strikeDelay);
        GameObject projectile = (GameObject)Instantiate(strike, this.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().setOwner(owner);
        Destroy(this.gameObject);
    }
}
