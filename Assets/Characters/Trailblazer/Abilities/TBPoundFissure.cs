using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class TBPoundFissure : AbilitySequence
{
    [SerializeField] private int additionalFissures;
    [SerializeField] private float fissureWidth;
    private RaycastHit2D[] toWarn;

    private void fireFissure(Vector3 dir)
    {
        toWarn = Physics2D.CircleCastAll(this.transform.position, fissureWidth / 2, dir, Mathf.Infinity, LayerMask.GetMask("FloorTile"));
        foreach (RaycastHit2D rch in toWarn) {
            FloorTile tile = rch.transform.gameObject.GetComponent<FloorTile>();
            FloorState tileState = tile.getState();
            tile.StopAllCoroutines();
            if (tileState == FloorState.Floor || tileState == FloorState.Warning) {
                tile.setWarning();
            } else {
                tile.setExtendedLava();
            }
        }
    }

    public override IEnumerator cast()
    {
        CameraShaker.Instance.ShakeOnce(15f, 100f, 0.1f, 0.1f);
        // always fire one fissure at the player
        fireFissure(GameObject.Find("Player").transform.position - this.transform.position);
        for (int i = 0; i < additionalFissures; i++) {
            Quaternion fissureAngle = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));
            fireFissure(fissureAngle * Vector3.right);
        }
        yield break;
    }
}
