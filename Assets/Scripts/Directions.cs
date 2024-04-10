using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Directions
{
    
    public static List<Vector3> directions = new()
    {
        Vector3.forward,
        Vector3.back,
        Vector3.right,
        Vector3.left,
        Vector3.forward + Vector3.right,
        Vector3.forward + Vector3.left,
        Vector3.back + Vector3.right,
        Vector3.back + Vector3.left
    };
}