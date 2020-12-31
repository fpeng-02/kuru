using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDirectionalLaser : MonoBehaviour
{
    [SerializeField] private GameObject laserEffect;
    [SerializeField] private float chargeTime;
    [SerializeField] private float damageInterval;
    [SerializeField] private float beamWidth;
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = this.gameObject.GetComponent<LineRenderer>() as LineRenderer;
        lr.enabled = true;
        //Create the line to initial hit spot
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.startColor = Color.green;
        lr.endColor = Color.green;
        StartCoroutine("laserHit");
    }
    public void turnOff()
    {
        lr.enabled = false;
    }


    public IEnumerator laserHit()
    {
        yield return new WaitForSeconds(chargeTime);
        lr.startWidth = beamWidth;
        lr.endWidth = beamWidth;
        float laserDistance = Vector3.Magnitude(new Vector3(transform.position.x - getLaserDir().x, transform.position.y - getLaserDir().y, 0));
        List<RaycastHit2D> rayResults = new List<RaycastHit2D>();
        ContactFilter2D cf = new ContactFilter2D();
        cf.useLayerMask = true;
        //Filter raycast to only hit ents (rip trees)
        cf.layerMask = LayerMask.GetMask("Entity", "Player");
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);
            Physics2D.CircleCast(this.transform.position, beamWidth / 2, getLaserDir(), cf, rayResults, laserDistance);
            foreach (RaycastHit2D curHit in rayResults)
            {
                Vector3 targetCurPos = curHit.transform.position;
                GameObject child = (GameObject)Instantiate(laserEffect, targetCurPos, Quaternion.Euler(0, 0, 0));
                child.GetComponent<Projectile>().setOwner(this.transform.parent.gameObject.GetComponent<Entity>());
                child.GetComponent<Projectile>().initializeDirVector(new Vector3(0, 0, 0));
                child.GetComponent<Projectile>().initializeQuaternion(Quaternion.Euler(0, 0, 0));
            } 
            rayResults.Clear();
        }

    }
    public Vector3 getLaserDir()
    {
        Vector3 firePos =  ((this.transform.parent.transform.rotation * transform.localRotation) * Vector3.up);
        RaycastHit2D initHit = Physics2D.Raycast(this.transform.parent.transform.position, firePos, 50.0f, LayerMask.GetMask("Environment"), -100, 100);
        return initHit.point;
    }

    void Update()
    {
        //Check for relative position
        lr.SetPosition(0, this.transform.parent.transform.position);
        lr.SetPosition(1, getLaserDir());
    }
}
