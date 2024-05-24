using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBobbing : MonoBehaviour
{
    public float _bobPower;
    public float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition += new Vector3(0, _bobPower * Mathf.Sin(Time.time * _speed), 0);
    }
}
