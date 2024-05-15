// /// <remarks>
// /// Author: asperatology
// /// Date Created: Unknown
// /// Bugs: Sometimes the powerup will launch in a random direction completely missing the target
// /// Source: https://forum.unity.com/threads/sharing-a-projectile-arc-kinematic-motion-arc-script.542775/
// /// </remarks>
// // <summary>
// /// This class manages the level, it will keep track of each teams score , the time ramaining and the current state of the level
// /// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPowerup : MonoBehaviour
{
    /// <summary>
    /// The Rigidbody component of the powerup
    /// </summary>
    public Rigidbody rigidBbody;

    /// <summary>
    /// The target transform that the powerup will be launched towards
    /// </summary>
    public Transform target;

    /// <summary>
    /// an empty gameobject that will be instantiated at a random position within the bounds of the level
    /// </summary>
    public GameObject targetObject;

    /// <summary>
    /// The maximum height of the arc that the powerup will travel
    /// </summary>
    public float maximumHeightOfArc;

    /// <summary>
    /// The strength of gravity that will be applied to the powerup
    /// </summary>
    public float gravity;

    /// <summary>
    /// A boolean to check if the powerup is currently being launched
    /// </summary>
    private bool isLaunching;

    void Awake()
    {
        this.rigidBbody = GetComponent<Rigidbody>(); //Assign the Rigidbody component
    }

    void Start()
    {
        rigidBbody.useGravity = false;
        this.isLaunching = false;

        Bounds bounds = LevelManager.instance.bounds;

        Vector3 randomPosition = GenerateRandomPosition(bounds);
        GameObject tO = Instantiate(targetObject, randomPosition, Quaternion.identity);

        tO.transform.position = randomPosition;
        this.target = tO.transform;
        this.maximumHeightOfArc = 10;
        this.gravity = -9.81f;
        Debug.Log("Target Position: " + this.target.position);
        Invoke("Launch", 1.0f); //Invoke the launch method after 1 second to ensure the target is set
    }

    /// <summary>
    /// Struct to hold the data required to launch the powerup to the target position
    /// </summary>
    private struct LaunchData
    {
        /// <summary>
        /// The initial velocity of the powerup when launched
        /// </summary>
        public readonly Vector3 initialVelocity;

        /// <summary>
        /// The time it will take for the powerup to reach the target
        /// </summary>
        public readonly float durationTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchData"/> struct.
        /// </summary>
        /// <param name="velocity">The initial velocity of the powerup when launched</param>
        /// <param name="time">The time it will take for the powerup to reach the target</param>
        public LaunchData(Vector3 velocity, float time)
        {
            this.initialVelocity = velocity;
            this.durationTime = time;
        }
    }

    /// <summary>
    /// Calculates where the powerup should be launched to hit the target Created by asperatology
    /// </summary>
    /// <returns>The Data required to launch and land at the target position</returns>
    private LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - rigidBbody.position.y;
        Vector3 displacementXZ = new Vector3(
            target.position.x - rigidBbody.position.x,
            0,
            target.position.z - rigidBbody.position.z
        );

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * maximumHeightOfArc);
        Vector3 velocityXZ =
            displacementXZ
            / (
                Mathf.Sqrt(-2 * maximumHeightOfArc / gravity)
                + Mathf.Sqrt(2 * (displacementY - maximumHeightOfArc) / gravity)
            );

        float time =
            Mathf.Sqrt(-2 * maximumHeightOfArc / gravity)
            + Mathf.Sqrt(2 * (displacementY - maximumHeightOfArc) / gravity);

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    /// <summary>
    /// This method will launch the powerup towards the target position created by asperatology
    /// </summary>
    void Launch()
    {
        this.isLaunching = true;
        Physics.gravity = Vector3.up * this.gravity;
        rigidBbody.useGravity = true;

        LaunchData data = CalculateLaunchData();
        if (!float.IsNaN(data.initialVelocity.y) && !float.IsInfinity(data.initialVelocity.y))
            rigidBbody.velocity = data.initialVelocity;
        else
        {
            this.isLaunching = false;
            rigidBbody.useGravity = false;
            rigidBbody.position = new Vector3(
                Random.insideUnitCircle.x * Random.Range(-100, 100),
                0,
                Random.insideUnitCircle.y * Random.Range(-100, 100)
            );
            this.maximumHeightOfArc = Random.Range(
                this.target.position.y + 1,
                this.target.position.y + 30
            );
            rigidBbody.velocity = Vector3.zero;
            Debug.LogError("Something went wrong with the launch data");
        }
        Debug.Log("Velocity: " + rigidBbody.velocity);
        Debug.Log("Gravity: " + Physics.gravity);
        Debug.Log("Position: " + rigidBbody.position);
        Debug.Log("Target Position: " + this.target.position);
        Debug.Log("Maximum Height: " + this.maximumHeightOfArc);
    }

    /// <summary>
    /// Generates a random position within the bounds of the level
    /// </summary>
    /// <param name="bounds">The Bounds that the Vector3 can be within</param>
    /// <returns>The Position as a Vector3</returns>
    private Vector3 GenerateRandomPosition(Bounds bounds)
    {
        Debug.Log("Bounds for powerup: " + bounds);

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        Debug.Log("RandomX: " + randomX + " RandomZ: " + randomZ);

        return new Vector3(randomX, 0, randomZ);
    }
}
