using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBPhase4 : Phase
{
    [SerializeField] private float survivalTime;
    [SerializeField] private float interval;

    public override IEnumerator beginPhase()
    {
        // go back to the center
        owner.setInvulnerable(true);
        this.transform.position = new Vector3(0, 0, 0);
        // reset all tiles, set them to permanently disappear
        // mark the whole floor for permanent destruction, then interrupt the states of the island tiles
        List<FloorTile> allTiles = GameObject.Find("FloorTileManager").GetComponent<FloorTileManager>().getAllTiles();
        foreach (FloorTile tile in allTiles) {
            tile.StopAllCoroutines();
            tile.setPermanentDisable(true);
            tile.setFloor();
        }
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            // delay
            yield return new WaitForSeconds(interval);
            // laser!!
            owner.cast("Radial Shotgun");
        }
    }

    public override void exitPhase()
    {
        StopAllCoroutines();
        owner.setInvulnerable(false);
    }
}
