/// <remarks>
/// Author: Erika Stuart
/// Date Created: 30/04/2024
/// Bugs: None known at this time.
/// </remarks>
/// <summary>
/// This script allows the player to reload their ammo from the snowball tray
/// </summary>
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SnowballCollision : MonoBehaviour
{
    // [HideInInspector]
    public string owner; //The owner of the snowball
    public GameObject ownerObject; //The owner of the snowball
    private PlaySFX playSFX;

    // Start is called before the first frame update
    private void Start()
    {
        playSFX = GetComponent<PlaySFX>();
    }

    /// <summary>
    /// When the snowball collides with another object, it will be destroyed.
    /// </summary>
    /// <param name="collision">The collider that collided with the snowball</param>
    void OnCollisionEnter(Collision collision)
    {
        if(TutorialManager.instance != null && collision.gameObject.CompareTag("Enemy"))
        {
            TutorialManager.instance.UpdateScore();
            Destroy(gameObject); //destroys itself no matter what it hits, snowball or border
            return;
        }
        if (collision.gameObject == ownerObject) //If the snowball hits the border
        {
            return;
        }
        else if (!collision.gameObject.CompareTag(owner))
        {
            if (collision.gameObject.GetComponent<ThrowSnowballs>() != null) //If the snowball hits another snowball
            {
                ThrowSnowballs ts = collision.gameObject.GetComponent<ThrowSnowballs>();
                if (!ts.Invulnerable)
                {
                    if (collision.gameObject.CompareTag("Enemy")) //If the snowball hits an enemy
                    {
                        LevelManager.instance.UpdateScore("Player"); //Update the player's score
                    }
                    else if (collision.gameObject.CompareTag("Player")) //If the snowball hits the player
                    {
                        LevelManager.instance.UpdateScore("Enemy"); //Update the enemy's score
                    }
                    else if (collision.gameObject.CompareTag("Snowball")) //If the snowball hits another snowball
                    {
                        return; //snowballs ignore each other
                    }
                    ts.Invulnerable = true; //Makes the snowball invulnerable
                }
            }
            //playSFX.playSound("SnowballHit");
            Destroy(gameObject); //destroys itself no matter what it hits, snowball or border
        }
        else //if the snowball hits something on same team
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            Destroy(gameObject);
        }
    }
}
