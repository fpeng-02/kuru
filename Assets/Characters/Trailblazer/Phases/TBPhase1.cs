using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBPhase1 : Phase
{
    [SerializeField] private float playerSafeRadius;
    [SerializeField] private float interval;
    [SerializeField] private float waitAfterTeleport;
    [SerializeField] private float xUpperBound;
    [SerializeField] private float xLowerBound;
    [SerializeField] private float yUpperBound;
    [SerializeField] private float yLowerBound;

    private GameObject player;

    // for the very very very beginning
    private float beginBoxSize = 1f;
    private bool playerExitedBeginBox()
    {

        return Mathf.Abs(player.transform.position.x) > beginBoxSize || Mathf.Abs(player.transform.position.y) > beginBoxSize;
    }

    public override IEnumerator beginPhase()
    {
        player = GameObject.Find("Player");
        owner.setInvulnerable(true);
        yield return new WaitForEndOfFrame(); // apparently the tile sprites don't work if there isn't a small delay here

        // animations maybe; be sure to freeze player movement while animations are playing i guess (maybe invisible walls, maybe in the script idk)

        // shake the tiles around the player until they step off
        ContactFilter2D cf = new ContactFilter2D();
        cf.layerMask = LayerMask.GetMask("FloorTile");
        cf.useLayerMask = true;
        cf.useTriggers = true;
        List<Collider2D> toSet = new List<Collider2D>();
        List<FloorTile> beginTiles = new List<FloorTile>();
        Physics2D.OverlapBox(Vector2.zero, new Vector2(1f, 1f), 0.0f, cf, toSet);
        foreach (Collider2D col in toSet) {
            FloorTile tile = col.gameObject.GetComponent<FloorTile>();
            tile.StopAllCoroutines();
            tile.setPermanentWarning();
            beginTiles.Add(tile);
        }

        // teleport the player to the center.
        player.transform.position = Vector3.zero;

        // wait until the player moves out of the initial box, then begin the fight
        yield return new WaitUntil(() => playerExitedBeginBox());
        foreach (FloorTile tile in beginTiles) {
            tile.StopAllCoroutines();
            tile.setLava();
        }
        owner.setInvulnerable(false);

        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        while (true) {
            // choose a random position to teleport to, but make sure it's far enough away from the player
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            Vector3 randomPos = playerPos;
            while ((randomPos - playerPos).magnitude < playerSafeRadius) {
                randomPos = new Vector3(Random.Range(xLowerBound, xUpperBound), Random.Range(yLowerBound, yUpperBound), 0);
            }
            this.transform.position = randomPos;
            yield return new WaitForSeconds(waitAfterTeleport);
            // then shoot a shotgun
            owner.cast("Shotgun");
            //It shoots two single shots (for loops for losers)
            yield return new WaitForSeconds(interval / 3);
            owner.cast("Single Shot");
            yield return new WaitForSeconds(interval / 3);
            owner.cast("Single Shot");
            yield return new WaitForSeconds(interval / 3);
            owner.cast("Single Shot");
        }
    }
}
