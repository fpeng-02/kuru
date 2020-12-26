using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBPoundPlayerRegion : AbilitySequence
{
    [SerializeField] private float radius;
    private List<Collider2D> toWarn = new List<Collider2D>();

    public override IEnumerator cast()
    {
        // find player position
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        // overlap circle on the player, trigger warning state on the tiles
        toWarn.Clear();
        ContactFilter2D cf = new ContactFilter2D();
        cf.layerMask = LayerMask.GetMask("FloorTile");
        cf.useLayerMask = true;
        cf.useTriggers = true;
        Physics2D.OverlapCircle(playerPos, radius, cf, toWarn);
        foreach (Collider2D col in toWarn) {
            FloorTile tile = col.gameObject.GetComponent<FloorTile>();
            FloorState tileState = tile.getState();
            tile.StopAllCoroutines();
            if (tileState == FloorState.Floor || tileState == FloorState.Warning) {
                tile.setWarning();
            }
            else {
                tile.setExtendedLava();
            }
        }
        yield break;
    }
}
