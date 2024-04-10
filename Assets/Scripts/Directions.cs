using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public static class Directions
{
    
    public static Vector3[] directions = new Vector3[8]
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