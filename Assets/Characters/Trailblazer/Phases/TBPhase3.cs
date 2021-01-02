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
    [SerializeField] private float islandSide; 
    private List<Collider2D> toSet = new List<Collider2D>();
    private GameObject player;
    private List<List<FloorTile>> islandTilesList = new List<List<FloorTile>>();

    public override IEnumerator beginPhase()
    {

        player = GameObject.Find("Player");
        player.transform.position = new Vector3(0, 0, 0);
        // choose the islands that aren't going to be permanently destroyed
        List<FloorTile> safeTiles = new List<FloorTile>();
        ContactFilter2D cf = new ContactFilter2D();
        cf.layerMask = LayerMask.GetMask("FloorTile");
        cf.useLayerMask = true;
        cf.useTriggers = true;

        // for each island center, unmark it as destroyed and then store the tiles for each island
        foreach (Vector2 center in islandCenters) {
            List<FloorTile> island = new List<FloorTile>();
            island.Clear();
            Physics2D.OverlapBox(center, new Vector2(islandSide, islandSide), 0.0f, cf, toSet);
            foreach (Collider2D col in toSet) {
                safeTiles.Add(col.gameObject.GetComponent<FloorTile>());
                island.Add(col.gameObject.GetComponent<FloorTile>());
            }
            islandTilesList.Add(island);
        }

        // mark the whole floor for permanent destruction, then interrupt the states of the island tiles
        List<FloorTile> allTiles = GameObject.Find("FloorTileManager").GetComponent<FloorTileManager>().getAllTiles();
        foreach (FloorTile tile in allTiles) {
            tile.StopAllCoroutines();
            tile.setPermanentDisable(true);
            tile.setWarning();
        }
        foreach (FloorTile tile in safeTiles) {
            tile.StopAllCoroutines();
            tile.setPermanentDisable(false);
            tile.setFloor();
        }

        //Pause and teleport to a random spot on the edge while invincible
        owner.setInvulnerable(true);
        this.transform.position = new Vector3(-5, 0, 0);
        yield return new WaitForSeconds(interval);
        owner.setInvulnerable(false);

        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            // horizontal/vertical laser
            owner.cast(movesetNames[Random.Range(0, movesetNames.Count)]);
            // also sink a random island
            foreach (FloorTile tile in islandTilesList[Random.Range(0, islandTilesList.Count)]) {
                tile.StopAllCoroutines();
                tile.setWarning();
            }
            yield return new WaitForSeconds(interval);
            // laser!!   
        }
    }

    public override void exitPhase()
    {
        base.exitPhase();
        GetComponent<LineRenderer>().enabled = false;
    }
}
