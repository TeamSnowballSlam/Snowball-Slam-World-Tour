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
    [SerializeField] private float abiltyCooldown = 10f; //The cooldown time for the ability from after the turret expires
    private const float TURRETOFFSET = 1.5f;
    public GameObject joeyPrefab; //The joey prefab
    private JoeyTurret activeTurret; //The active turret
    [HideInInspector] public bool canUseTurret; //Whether or not the ability is active
    private float abilityTime; //The time the ability was used

     [Range(0, 100)]
    public int turretSpawnChance = 50; //percentage chance of spawning a turret
    
    // Start is called before the first frame update
    void Start()
    {
        canUseTurret = true;
    }
/// <summary>
/// Places the turret on the map
/// </summary>
    public void PlaceTurret()
    {//If the ability is on cooldown or there is already an active turret, return
        if (canUseTurret) 
        { 
            Instantiate(joeyPrefab, transform.position + transform.forward * TURRETOFFSET, transform.rotation);
            JoeyTurret joeyTurret = joeyPrefab.GetComponent<JoeyTurret>();
            joeyTurret.parent = gameObject;
            abilityTime = Time.time;
            activeTurret = joeyTurret;
            canUseTurret = false;

         } 
    }


    void Update()
    {
        if (canUseTurret == false)
        {
            CheckAbilityCooldown();
        }
   
    }

    /// <summary>
    /// Checks whether the ability is on cooldown
    /// </summary>
    public void CheckAbilityCooldown()
    {
        
        if (Time.time - abilityTime >= (abiltyCooldown + activeTurret.expireTime ))
        {
            canUseTurret = true;
        }
    }

}
