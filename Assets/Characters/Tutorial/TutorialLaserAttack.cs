using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLaserAttack : AbilitySequence
{
    [SerializeField] private List<Effect> effectList = new List<Effect>(); 
    [SerializeField] private float chargeTime;
    [SerializeField] private float damageInterval;
    [SerializeField] private float beamWidth;

    public Vector3 getSpawnVector()
    {
        return this.transform.position;
    }

    public Vector3 getPlayerVector()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        playerPos.z = 0.0F;
        Vector3 dirVector = playerPos - this.transform.position;
        dirVector.z = 0;
        return dirVector.normalized;
    }

    public override IEnumerator cast()
    {
        //Create the line to initial hit spot.
        Vector3 playerPos = getPlayerVector();
        LineRenderer lr = this.gameObject.AddComponent<LineRenderer>() as LineRenderer;

        RaycastHit2D initHit = Physics2D.Raycast(this.transform.position, playerPos, 50.0f, LayerMask.GetMask("Environment"),-100,100);

        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr. startColor = Color.red;
        lr.endColor = Color.green;
        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, initHit.point);

        Debug.Log("dont open ur door");
        yield return new WaitForSeconds(chargeTime);
        lr.startWidth = beamWidth;
        lr.endWidth = beamWidth;

        
        Vector3 laserDistance = new Vector3(transform.position.x - initHit.point.x, transform.position.y - initHit.point.y, 0);
        float tempUptime = this.getUptime()-chargeTime;
        List<RaycastHit2D> rayResults = new List<RaycastHit2D>();
        ContactFilter2D cf = new ContactFilter2D();
        cf.useLayerMask = true;
        //Filter raycast to only hit ents (rip trees)
        cf.layerMask = LayerMask.GetMask("Entity");

        while (tempUptime >= 0)
        {
            yield return new WaitForSeconds(damageInterval);
            Physics2D.CircleCast(this.transform.position, beamWidth/2,playerPos, cf, rayResults, Vector3.Magnitude(laserDistance));
            foreach (RaycastHit2D curHit in rayResults)
            {
                Entity curEnt = curHit.transform.gameObject.GetComponent<Entity>();
                foreach (Effect effect in effectList)
                {
                    effect.applyEffect(curEnt);
                }
            }
            tempUptime -= damageInterval;
            rayResults.Clear();
        }
        
        GameObject.Destroy(this.gameObject.GetComponent<LineRenderer>());
        Debug.Log(this.GetComponent<LineRenderer>());
        yield return null;
        
    }
}
