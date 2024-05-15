using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SnowballCollision : MonoBehaviour
{
    [HideInInspector]
    public string owner; //The owner of the snowball

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Snowball hit: " + collision.gameObject.tag + " with owner: " + owner);
        if (!collision.gameObject.CompareTag(owner))
        {
            if (collision.gameObject.CompareTag("Enemy")) //If the snowball hits an enemy
            {
                LevelManager.instance.UpdateScore("Player"); //Update the player's score
            }
            else if (collision.gameObject.CompareTag("Player")) //If the snowball hits the player
            {
                LevelManager.instance.UpdateScore("Enemy"); //Update the enemy's score
            }
        }

        Destroy(gameObject); //destroys itself no matter what it hits, snowball or border
    }
}
