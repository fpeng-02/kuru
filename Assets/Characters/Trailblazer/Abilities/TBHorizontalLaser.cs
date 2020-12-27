using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBHorizontalLaser : AbilitySequence
{
    [SerializeField] private GameObject laserEffect;
    [SerializeField] private float chargeTime;
    [SerializeField] private float damageInterval;
    [SerializeField] private float beamPrepWidth;
    [SerializeField] private float beamFireWidth;
    private LineRenderer lr;


    // set "lr" in start
    void Start()
    {
        lr = this.gameObject.GetComponent<LineRenderer>();
        lr.enabled = false;
    }

    public Vector3 getSpawnVector()
    {
        return this.transform.position;
    }

    enum HorizontalDirection { Right, Left }

    public override IEnumerator cast()
    {
        HorizontalDirection castDir = (HorizontalDirection)Random.Range(0, 2);
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        Vector3 randomPos = new Vector3(0, 0, 0);

        // teleport to a random spot along the perimeter of the 9 islands
        // TODO: replace these magic numbers lol
        randomPos.x = castDir == HorizontalDirection.Right ? -5 : 5;
        randomPos.y = Mathf.Round(playerPos.y / 3.0f) * 3;
        this.transform.position = randomPos;

        // prep laser 
        lr.enabled = true;
        Vector2 castDirVector = castDir == HorizontalDirection.Right ? Vector2.right : Vector2.left;
        RaycastHit2D initHit = Physics2D.Raycast(this.transform.position, castDirVector, 50.0f, LayerMask.GetMask("Environment"), -100, 100);
        lr.startWidth = beamPrepWidth;
        lr.endWidth = beamPrepWidth;
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, initHit.point);
        yield return new WaitForSeconds(chargeTime);

        // fire laser
        lr.startWidth = beamFireWidth;
        lr.endWidth = beamFireWidth;
        Vector3 laserDistance = new Vector3(transform.position.x - initHit.point.x, transform.position.y - initHit.point.y, 0);
        float fireUptime = this.getUptime() - chargeTime;
        List<RaycastHit2D> rayResults = new List<RaycastHit2D>();
        ContactFilter2D cf = new ContactFilter2D();
        cf.useLayerMask = true;
        //Filter raycast to only hit ents (rip trees)
        cf.layerMask = LayerMask.GetMask("Entity", "Player");
        while (fireUptime >= 0) {
            yield return new WaitForSeconds(damageInterval);
            Physics2D.CircleCast(this.transform.position, beamFireWidth / 2, playerPos, cf, rayResults, Vector3.Magnitude(laserDistance));
            foreach (RaycastHit2D curHit in rayResults) {
                Vector3 targetCurPos = curHit.transform.position;
                GameObject child = (GameObject)Instantiate(laserEffect, targetCurPos, Quaternion.Euler(0, 0, 0));
                child.GetComponent<Projectile>().setOwner(this.gameObject.GetComponent<Entity>());
                child.GetComponent<Projectile>().initializeDirVector(new Vector3(0, 0, 0));
                child.GetComponent<Projectile>().initializeQuaternion(Quaternion.Euler(0, 0, 0));
            }
            fireUptime -= damageInterval;
            rayResults.Clear();
        }

        lr.enabled = false;
        //GameObject.Destroy(this.gameObject.GetComponent<LineRenderer>());
        //Debug.Log(this.GetComponent<LineRenderer>());
        yield return null;
    }

    public void turnOff()
    {
        lr.enabled = false;
    }

    void Update()
    {
        if (lr.enabled) {
            lr.SetPosition(0, this.transform.position);
        }
    }
}
