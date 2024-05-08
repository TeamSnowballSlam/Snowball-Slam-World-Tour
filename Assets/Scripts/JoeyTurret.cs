/// <remarks>
/// Author: Chase Bennett-Hill
/// Date Created: May 7, 2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
///Manages the Joey Turret Object
////// </summary>
using UnityEditor.SceneManagement;
using UnityEngine;

public class JoeyTurret : MonoBehaviour
{
    public float expireTime = 15f; //The time before the ability expires
    public GameObject parent; //The parent object of the ability
    private float throwTime; //The time to throw the snowball
    private float delayTime = 0.75f; //The delay time between throws

    private float spawnTime; //The time the ability was spawned
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0.65f, transform.position.z); //Sets the position of the ability to the parent's position
        spawnTime = Time.time; //Sets the spawn time to the current time
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Time.time > throwTime + delayTime)
            {
                GetComponent<ThrowSnowballs>().ThrowSnowball(); //Throws a snowball
                throwTime = Time.time; //Sets the throw time to the current time
            }
        if(Time.time - spawnTime >= expireTime) //If the ability has expired
        {
            Destroy(gameObject); //Destroy the ability
        }
    }
}
