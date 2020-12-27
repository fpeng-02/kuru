using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBChainPound : AbilitySequence
{
    [SerializeField] private float jumpDelay;
    [SerializeField] private float xUpperBound;
    [SerializeField] private float yUpperBound;
    [SerializeField] private float xLowerBound;
    [SerializeField] private float yLowerBound;
    private List<Collider2D> toWarn = new List<Collider2D>();
    [SerializeField] private float radius;
    [SerializeField] private float fractionHitForDamage;


    public override IEnumerator cast()
    {

        Vector2 jumpPos = new Vector2(Random.Range(xLowerBound, xUpperBound), Random.Range(yLowerBound, yUpperBound));
        ContactFilter2D cf = new ContactFilter2D();
        cf.layerMask = LayerMask.GetMask("FloorTile");
        cf.useLayerMask = true;
        cf.useTriggers = true;
        Physics2D.OverlapCircle(jumpPos, radius, cf, toWarn);
        foreach (Collider2D col in toWarn)
        {
            FloorTile tile = col.gameObject.GetComponent<FloorTile>();
            tile.StopAllCoroutines();
            tile.setHighlight();
        }
        yield return new WaitForSeconds(jumpDelay);
        Debug.Log("dog");
        this.transform.position = new Vector3(jumpPos.x, jumpPos.y, -5);
        float lavaCount = 0;
        float floorCount = 0;
        foreach (Collider2D col in toWarn)
        {
            FloorTile tile = col.gameObject.GetComponent<FloorTile>();
            FloorState tileState = tile.getState();
            if (tileState == FloorState.Lava || tileState == FloorState.Warning)
            {
                lavaCount += 1;
            }
            else
            {
                floorCount += 1;
            }
        }

        if ( lavaCount / (lavaCount + floorCount) > fractionHitForDamage)
        {
            caster.cast("Radial Shotgun");
            foreach (Collider2D col in toWarn)
            {
                FloorTile tile = col.gameObject.GetComponent<FloorTile>();
                FloorState tileState = tile.getState();

                if (!(tileState == FloorState.Lava))
                {
                    tile.setLava();
                }
                else
                {
                    tile.setExtendedLava();
                }
            }
            caster.setHitPoints(caster.getHitPoints() - 40);
        }
        else
        {
            caster.cast("Radial Shotgun");
            foreach (Collider2D col in toWarn)
            {
                FloorTile tile = col.gameObject.GetComponent<FloorTile>();
                FloorState tileState = tile.getState();
                if (tileState == FloorState.Highlight)
                {
                    tile.setFloor();
                }

            }
        }
        
        yield return null;


    }


}
