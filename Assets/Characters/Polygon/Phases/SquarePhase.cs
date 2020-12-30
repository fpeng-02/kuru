using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePhase : Phase
{
    [SerializeField] private Sprite[] diceSprites = new Sprite[6];

    public override IEnumerator beginPhase()
    {
        yield return phaseLoop();
    }

    public override IEnumerator phaseLoop()
    {
        // MOVEMENT
        // move in a straight line
        // when hit wall, reflect off and bonk on walls

        // ATTACK
        // choose what to spawn based on roll number
        // stay still? idk lol
        yield break;
    }
}
