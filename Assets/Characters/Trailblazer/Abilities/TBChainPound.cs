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
    [SerializeField] private float landingTime;
    [SerializeField] private float leavingTime;
    [SerializeField] private float ouchTime;
    [SerializeField] private float fractionHitForDamage;
    private float t;
    private bool wasHit;

    public override IEnumerator cast()
    {
        wasHit = false;

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
            tile.setHighlight(true);
            if (tile.getState() == FloorState.Floor) {
                tile.setFloor();
            }
        }
        yield return new WaitForSeconds(jumpDelay);

        // landing animation
        // teleport the boss outside the screen first
        caster.transform.position = (Vector3)jumpPos + new Vector3(-20, 20, 0);
        // move him in
        Vector2 currentPos = caster.transform.position;
        t = 0f;
        while (t < 1) {
            t += Time.deltaTime / landingTime;
            transform.position = Vector2.Lerp(currentPos, jumpPos, t);
            yield return null;
        }

        // actual jump
        caster.cast("Landing Hit");
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
            caster.cast("Final Rage Ring");
            wasHit = true;
             // maybe replace with "rage" one idk
            foreach (FloorTile tile in toWarn) {
                tile.setHighlight(false);
                FloorState tileState = tile.getState();
                if (!(tileState == FloorState.Lava)) {
                    tile.setLava();
                } else {
                    tile.setExtendedLava();
                }
            }
            caster.setHitPointsBypass(caster.getHitPoints() - 30);
        } else {
            caster.cast("Final Phase Ring");
            foreach (FloorTile tile in toWarn) {
                tile.setHighlight(false);
                FloorState tileState = tile.getState();
                tile.setWarning();
            }
        }

        // leaving animation: if hit, do a little shake first then actually leave
        if (wasHit) {
            currentPos = caster.transform.position;
            Vector2 ouchPos = caster.transform.position + new Vector3(0, 1f, 0);
            t = 0f;
            while (t < 1) {
                t += Time.deltaTime / ouchTime;
                transform.position = Vector2.Lerp(currentPos, ouchPos, t);
                this.transform.rotation = Quaternion.Euler(0, 0, this.transform.eulerAngles.z + 15);
                yield return null;
            }
        }
        this.transform.rotation = Quaternion.identity;
        Vector2 leavingPos = caster.transform.position + new Vector3(20, 20, 0);
        // move him out
        currentPos = caster.transform.position;
        t = 0f;
        while (t < 1) {
            t += Time.deltaTime / leavingTime;
            transform.position = Vector2.Lerp(currentPos, leavingPos, t);
            yield return null;
        }
        yield return null;
    }
}
