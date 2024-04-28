using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : MonoBehaviour
{
    public int playerID;
    public Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(startingPosition.x, transform.position.y, startingPosition.z); //This uses the current y so the height of the game object can be adjusted on the prefab
    }
}
