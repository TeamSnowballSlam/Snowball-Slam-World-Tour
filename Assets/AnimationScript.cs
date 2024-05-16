using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void DestroyTurret()
    {
        Destroy(gameObject.transform.parent.gameObject); //Destroys the ability
    }
}
