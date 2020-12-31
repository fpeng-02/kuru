using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class TBChainPound : AbilitySequence
{
    [SerializeField] private float jumpDelay;
    [SerializeField] private float xUpperBound;
    [SerializeField] private float yUpperBound;
    [SerializeField] private float xLowerBound;
    [SerializeField] private float yLowerBound;
    //private List<Collider2D> toWarn = new List<Collider2D>();
    [SerializeField] private float radius;
    [SerializeField] private float fractionHitForDamage;


    public override IEnumerator cast()
    {
        //List<Collider2D> toWarn = new List<Collider2D>();
        List<FloorTile> toWarn = new List<FloorTile>();

        // get the jump position
        Vector2 jumpPos = new Vector2(Random.Range(xLowerBound, xUpperBound), Random.Range(yLowerBound, yUpperBound));
        ContactFilter2D cf = new ContactFilter2D();
        cf.layerMask = LayerMask.GetMask("FloorTile");
        cf.useLayerMask = true;
        cf.useTriggers = true;

        // mark the circle of tiles that will be affected by the jump
        List<Collider2D> res = new List<Collider2D>();
        Physics2D.OverlapCircle(jumpPos, radius, cf, res);
        foreach (Collider2D col in res)
        {
            FloorTile tile = col.GetComponent<FloorTile>();
            toWarn.Add(tile);
            //tile.StopAllCoroutines();
            tile.setHighlight(true);
            if (tile.getState() == FloorState.Floor) {
                tile.setFloor();
            }
        }
        yield return new WaitForSeconds(jumpDelay);

        // do the jump
        CameraShaker.Instance.ShakeOnce(15f, 100f, 0.1f, 0.1f);
        this.transform.position = new Vector3(jumpPos.x, jumpPos.y, -5);
        float lavaCount = 0;
        float floorCount = 0;
        // on impact, count the number of tiles that are floor vs lava
        foreach (FloorTile tile in toWarn) {
            FloorState tileState = tile.getState();
            if (tileState == FloorState.Lava || tileState == FloorState.Warning) {
                lavaCount += 1;
            } else {
                floorCount += 1;
            }
        }
        // if enough tiles were knocked out, damage the boss
        if ( lavaCount / (lavaCount + floorCount) > fractionHitForDamage) {
            caster.cast("Ring"); // maybe replace with "rage" one idk
            foreach (FloorTile tile in toWarn) {
                tile.setHighlight(false);
                FloorState tileState = tile.getState();
                if (!(tileState == FloorState.Lava)) {
                    tile.setLava();
                } else {
                    tile.setExtendedLava();
                }
            }
            caster.setHitPointsBypass(caster.getHitPoints() - 40);
        } else {
            caster.cast("Ring");
            foreach (FloorTile tile in toWarn) {
                tile.setHighlight(false);
                FloorState tileState = tile.getState();
                tile.setWarning();
            }
        }
        
        yield return null;


    }


}
