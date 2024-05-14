using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public enum PowerUpType
    {
        Speed,
        Shield,
        SnowballLauncher,
        Unassigned
    }

public class AudienceMember : MonoBehaviour
{
    public PowerUpType powerUpType;

    [Range(0, 100)]
    public int powerUpSpawnChance;
    private const int POWERUPDELAY = 5;
    private float lastSpawnTime = 0;

    [SerializeField] private GameObject powerUpPrefab;
    // Start is called before the first frame update
    void Start()
    {
        powerUpType = PowerUpType.Unassigned;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawnTime > POWERUPDELAY)
        {
            Debug.Log("Spawning power up");
            lastSpawnTime = Time.time;
            if(Random.Range(0, 100) < powerUpSpawnChance)
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
        Debug.Log("Picked power up: " + ParsePowerUpType());
    }

    private void SpawnPowerUp()
    {
        GameObject powerUp = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
    }
}
