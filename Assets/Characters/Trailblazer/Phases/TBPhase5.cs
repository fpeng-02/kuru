using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBPhase5 : Phase
{
    [SerializeField] private float interval;
    public override IEnumerator beginPhase()
    {
        //Introduction cutscene
        owner.setInvulnerable(true);
        // set all tiles to non-permanent and back to floor
        List<FloorTile> allTiles = GameObject.Find("FloorTileManager").GetComponent<FloorTileManager>().getAllTiles();
        foreach (FloorTile tile in allTiles) {
            tile.StopAllCoroutines();
            tile.setPermanentDisable(false);
            tile.setFloor();
        }
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true)
        {
            // delay
            owner.cast("Chain Pound");

            yield return new WaitForSeconds(interval);
            // laser!!
        }
    }
}
