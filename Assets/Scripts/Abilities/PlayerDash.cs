using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : AbilitySequence
{
    [SerializeField] private float dashSpeedBonus;
    [SerializeField] private float dashTime;

    public override IEnumerator cast()
    {
        Debug.Log("Dashing");
        // TODO: dash animation?
        Player player = (Player)caster;
        player.setDashing(true);
        caster.setInvulnerable(true);
        caster.setSpeed(player.getSpeed() + dashSpeedBonus);
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        dir.z = 0;
        player.setDirVect(dir.normalized);
        yield return new WaitForSeconds(dashTime);
        caster.setInvulnerable(false);
        caster.setSpeed(player.getBaseMoveSpeed());
        player.setDashing(false);
    }
}
