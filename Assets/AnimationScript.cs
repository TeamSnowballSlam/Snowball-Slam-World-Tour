using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public void ThrowSnowball()
    {
        gameObject.transform.parent.gameObject.GetComponent<ThrowSnowballs>().SnowballAnimation("Enemy");
    }

    // Start is called before the first frame update
    public void DestroyTurret()
    {
        Destroy(gameObject.transform.parent.gameObject); //Destroys the ability
    }


}
