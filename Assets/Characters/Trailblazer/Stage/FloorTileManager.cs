using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileManager : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private float xLowerBound;
    [SerializeField] private float xUpperBound;
    [SerializeField] private float yLowerBound;
    [SerializeField] private float yUpperBound;
    [SerializeField] private float distance;
    // Start is called before the first frame update
    void Start()
    {
        for (float hor = xLowerBound; hor <= xUpperBound; hor += distance)
        {
            for (float ver = yLowerBound; ver <= yUpperBound; ver += distance)
            {
                Instantiate(tile, new Vector3(hor, ver, 2), Quaternion.Euler(0,0,0), this.transform);
            }
        }
    }
}
