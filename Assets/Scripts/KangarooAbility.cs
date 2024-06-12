/// <remarks>
/// Author: Chase Bennett-Hill
/// Date Created: May 7, 2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// This class is used to create a turret that the kangaroo can place on the map.
///</summary>

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
    public bool canUseTurret; //Whether or not the ability is active
    private float abilityTime = 0; //The time the ability was used

    [Range(0, 100)]
    public int turretSpawnChance = 50; //percentage chance of spawning a turret
    //Animator
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        canUseTurret = false;
        animator = GetComponent<Animator>();
    }
    /// <summary>
    /// Places the turret on the map
    /// </summary>
    public void PlaceTurret()
    {
        //If the ability is on cooldown or there is already an active turret, return
        if (canUseTurret)
        {
            if (Physics.CheckBox(transform.position + transform.forward * TURRETOFFSET, new Vector3(0.05f, 0.05f, 0.05f), Quaternion.identity))
            {
                return;
            }
            animator.SetTrigger("doSpawnJoey");
            Instantiate(joeyPrefab, transform.position + transform.forward * TURRETOFFSET, transform.rotation);
            JoeyTurret joeyTurret = joeyPrefab.GetComponent<JoeyTurret>();
            joeyTurret.parent = gameObject;
            abilityTime = Time.time;
            activeTurret = joeyTurret;
            canUseTurret = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canUseTurret == false)
        {
            CheckAbilityCooldown();
        }
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        if (turrets.Length > 2)
        {
            if (turrets[2] != null)
            {
                Destroy(turrets[2]);
            }
        }

    }

    /// <summary>
    /// Checks whether the ability is on cooldown
    /// </summary>
    public void CheckAbilityCooldown()
    {
        if (activeTurret)
        {
            if (Time.time - abilityTime >= (abiltyCooldown + activeTurret.expireTime))
            {
                if (GameObject.FindGameObjectsWithTag("Turret").Length < 2)
                {
                    canUseTurret = true;
                }
                canUseTurret = true;
            }
        }
        else
        {
            if (Time.time - abilityTime >= abiltyCooldown)
            {
                if (GameObject.FindGameObjectsWithTag("Turret").Length < 2)
                {
                    canUseTurret = true;
                }
            }

        }
    }

}
