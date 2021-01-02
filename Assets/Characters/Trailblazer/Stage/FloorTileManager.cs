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
    private List<FloorTile> allTiles = new List<FloorTile>();  // can make 2D later if necessary, using this to get a ref to *all* tiles (ex. for a whole floor wipe)

    public List<FloorTile> getAllTiles() { return this.allTiles; }

    void Start()
    {
        for (float hor = xLowerBound; hor <= xUpperBound; hor += distance)
        {
            for (float ver = yLowerBound; ver <= yUpperBound; ver += distance)
            {
                GameObject go = (GameObject)Instantiate(tile, new Vector3(hor, ver, 2), Quaternion.Euler(0,0,0), this.transform);
                allTiles.Add(go.GetComponent<FloorTile>());
                go.GetComponent<FloorTile>().setOriginalPos(new Vector3(hor, ver, 2));
            }
        }
    }
}
