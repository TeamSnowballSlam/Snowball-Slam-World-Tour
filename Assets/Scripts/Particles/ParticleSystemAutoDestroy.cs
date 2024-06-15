/// <remarks>
/// Author: Benjamin Mead
/// Date Created: 23/05/2024
/// Bugs: None known at this time.
/// </remarks>
// <summary>
/// This class is used to destroy a particle system when it is no longer active.
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{

    private ParticleSystem _ps;

    // Start is called before the first frame update
    public void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    // FixedUpdate is called at fixed intervals
    public void FixedUpdate()
    {
        if (_ps && !_ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}