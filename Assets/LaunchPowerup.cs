/// <remarks>
/// Author: asperatology
/// Date Created: Unknown
/// Bugs: None known at this time.
/// Source: https://forum.unity.com/threads/sharing-a-projectile-arc-kinematic-motion-arc-script.542775/
/// </remarks>
// <summary>
/// This class manages the level, it will keep track of each teams score , the time ramaining and the current state of the level
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPowerup : MonoBehaviour
{   /**
        Kinematic equations:
        1. s = ((u + v) * t) / 2
        2. v = u + a * t
        3. s = u * t + (a * t * t) / 2
        4. s = v * t - (a * t * t) / 2
        5. v * v = u * u + 2 * a * s
 
        v = final velocity
        u = initial velocity
        a = acceleration (gravity)
        s = displacement
        t = duration time
     */

    public new Rigidbody rigidbody;
    private Transform target;
    public Vector3 InitialVelocity;

    public float maximumHeightOfArc;
    public float gravity;
    public int pathResolution;
    public bool showDebugPath;

    private bool isLaunching;
    private Vector3 savedPosition;

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float durationTime;

        public LaunchData(Vector3 velocity, float time)
        {
            this.initialVelocity = velocity;
            this.durationTime = time;
        }
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - rigidbody.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - rigidbody.position.x, 0, target.position.z - rigidbody.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * maximumHeightOfArc);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * maximumHeightOfArc / gravity) + Mathf.Sqrt(2 * (displacementY - maximumHeightOfArc) / gravity));

        float time = Mathf.Sqrt(-2 * maximumHeightOfArc / gravity) + Mathf.Sqrt(2 * (displacementY - maximumHeightOfArc) / gravity);
        Debug.Log("DisplacementY: " + displacementY + " DisplacementXZ: " + displacementXZ + " VelocityY: " + velocityY + " VelocityXZ: " + velocityXZ + " Time: " + time);

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void Launch()
    {
        this.isLaunching = true;
        this.savedPosition = rigidbody.position;
        Physics.gravity = Vector3.up * this.gravity;
        rigidbody.useGravity = true;

        LaunchData data = CalculateLaunchData();
        if (!float.IsNaN(data.initialVelocity.y) && !float.IsInfinity(data.initialVelocity.y))
            rigidbody.velocity = data.initialVelocity;
        else
        {
            this.isLaunching = false;
            rigidbody.useGravity = false;
            rigidbody.position = new Vector3(Random.insideUnitCircle.x * Random.Range(-100, 100), 0, Random.insideUnitCircle.y * Random.Range(-100, 100));
            this.maximumHeightOfArc = Random.Range(this.target.position.y + 1, this.target.position.y + 30);
            rigidbody.velocity = Vector3.zero;
        }
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        if (float.IsNaN(launchData.initialVelocity.y) || float.IsInfinity(launchData.initialVelocity.y))
        {
            return;
        }
        Vector3 originalPosition = rigidbody.position;

        Vector3[] positions = new Vector3[this.pathResolution + 1];
        for (int i = 0; i <= this.pathResolution; i++)
        {
            float simulationTime = (i / (float)this.pathResolution) * launchData.durationTime;
            Vector3 displacement = launchData.initialVelocity * simulationTime + (Vector3.up * gravity) * simulationTime * simulationTime / 2f;
            positions[i] = originalPosition + displacement;
        }


        this.InitialVelocity = launchData.initialVelocity;
    }

    void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigidbody.useGravity = false;
        this.isLaunching = false;
        Bounds bounds = LevelManager.instance.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        Debug.Log("RandomX: " + randomX + " RandomZ: " + randomZ);
        Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
        GameObject targetObject = new GameObject("Target");
        targetObject.transform.position = randomPosition;
        this.target = targetObject.transform;
        this.maximumHeightOfArc = 10;
        this.gravity = -9.81f;
        Debug.Log("Target Position: " + this.target.position);
    }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DrawPath();
                Launch();
            }
            if (showDebugPath && !this.isLaunching)
            {
                DrawPath();
            }
        }

        void OnValidate()
        {
            if (showDebugPath && !this.isLaunching)
            {
                DrawPath();
            }
        }

    }
