/// <remarks>
/// Author: Chase Bennett-Hill
/// Date Created: May 7, 2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
///This class handles the joey summoning ability for the kangaroo enemy
////// </summary>

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KangarooAbility : MonoBehaviour
{

    public GameObject joeyPrefab; //The joey prefab
    public bool hasActiveTurret; //The list of active turrets
    
    // Start is called before the first frame update
    void Start()
    {
        hasActiveTurret = false;
    }
/// <summary>
/// Places the turret on the map
/// </summary>
    public void PlaceTurret()
    {
        if (hasActiveTurret) { return; }
        Instantiate(joeyPrefab, transform.position, Quaternion.identity);
        JoeyTurret joeyTurret = joeyPrefab.GetComponent<JoeyTurret>();
        joeyTurret.parent = gameObject;
        hasActiveTurret = true;
    }

}
