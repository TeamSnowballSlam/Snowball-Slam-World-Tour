using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Different types of power ups that can be thrown
/// </summary>
public enum PowerUpType
{
    Speed, //The speed boost (fish)
    Shield, //Protects from 3 hits (egg hat)
    SnowballLauncher, //Enhanced throwing rate
    Unassigned //No power up assigned
}

public class AudienceMember : MonoBehaviour
{
    public PowerUpType powerUpType;

    [Range(0, 100)]
    [SerializeField]
    /// <summary>
    /// The chance of a power up being thrown
    /// </summary>
    private int powerUpSpawnChance;

    [SerializeField]
    /// <summary>
    /// The time in seconds between each possible power up spawn
    /// </summary>
    private int throwFrequency = 5;
    /// <summary>
    /// The last time a power up was spawned
    /// </summary>
    private float lastSpawnTime = 0;

    [SerializeField]
    private GameObject powerUpPrefab;

    // Start is called before the first frame update
    void Start()
    {
        powerUpType = PowerUpType.Unassigned;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime > throwFrequency)
        {
            Debug.Log("Spawning power up");
            lastSpawnTime = Time.time;
            if (Random.Range(0, 100) < powerUpSpawnChance)
            {
                PickPowerUp();
                SpawnPowerUp();
            }
        }
    }

    /// <summary>
    /// Parses the power up type to a string
    /// </summary>
    /// <returns>The Type of power up as a string</returns>
    private string ParsePowerUpType()
    {
        switch (powerUpType)
        {
            case PowerUpType.Speed:
                return "Speed";
            case PowerUpType.Shield:
                return "Shield";
            case PowerUpType.SnowballLauncher:
                return "Snowball Launcher";
            case PowerUpType.Unassigned:
            default:
                Debug.LogError("Unknown or Unassigned power up type");
                return "Unknown";
        }
    }

    /// <summary>
    /// Picks a random power up type
    /// </summary>
    private void PickPowerUp()
    {
        int random = Random.Range(0, 3);
        powerUpType = (PowerUpType)random;
    }

    private void SpawnPowerUp()
    {
        GameObject powerUp = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
    }
}
