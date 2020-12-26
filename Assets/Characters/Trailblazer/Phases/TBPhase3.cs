using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBPhase3 : Phase
{
    [SerializeField] private float playerSafeRadius;
    [SerializeField] private float interval;
    [SerializeField] private float waitAfterTeleport;
    [SerializeField] private List<Vector2> islandCenters;
    [SerializeField] private float islandRadius; // all islands constant for size for now, but can be changed to a list if diff radii are wanted
    private List<Collider2D> toSet = new List<Collider2D>();

    public override IEnumerator beginPhase()
    {
        // choose the islands that aren't going to be permanently destroyed
        List<FloorTile> safeTiles = new List<FloorTile>();
        ContactFilter2D cf = new ContactFilter2D();
        cf.layerMask = LayerMask.GetMask("FloorTile");
        cf.useLayerMask = true;
        cf.useTriggers = true;
        foreach (Vector2 center in islandCenters) {
            Physics2D.OverlapCircle(center, islandRadius, cf, toSet);
            foreach (Collider2D col in toSet) {
                safeTiles.Add(col.gameObject.GetComponent<FloorTile>());
            }
        }

        // mark the whole floor for permanent destruction, then interrupt the states of the island tiles
        List<FloorTile> allTiles = GameObject.Find("FloorTileManager").GetComponent<FloorTileManager>().getAllTiles();
        foreach (FloorTile tile in allTiles) {
            tile.StopAllCoroutines();
            tile.setWarningToPermanentLava();
        }
        foreach (FloorTile tile in safeTiles) {
            tile.StopAllCoroutines();
            tile.setFloor();
        }

        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        yield break;
    }
}
