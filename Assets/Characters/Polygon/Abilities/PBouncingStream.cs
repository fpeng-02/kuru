using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBouncingStream : AbilitySequence
{
    private Color[] prismColors = { Color.red, new Color(1f, 0.64f, 0f), Color.yellow, Color.green, Color.blue, new Color(0.29f, 0f, 0.51f), Color.magenta };
    [SerializeField] private GameObject bullet;
    [SerializeField] private int numShots;
    [SerializeField] private float burstDelay;
    [SerializeField] private float angleChange;


    // Start is called before the first frame update
    public override IEnumerator cast()
    {
        Quaternion spawnAngle = Quaternion.Euler(0, 0, Random.Range(0, 360));
        int color = Random.Range(0, 7);
        for (int i = 0; i<numShots; i++)
        {
            GameObject go = Instantiate(bullet, this.transform.position, spawnAngle*Quaternion.Euler(0,0,i*angleChange));
            go.transform.localScale = new Vector3(1.0f, 1.0f, 1f);
            go.GetComponent<SpriteRenderer>().color = prismColors[color];
            yield return new WaitForSeconds(burstDelay);
        }  
        yield return null;
    }
}
