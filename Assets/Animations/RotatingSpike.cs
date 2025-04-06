using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpike : MonoBehaviour
{
    public float rotationSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

        if (transform.rotation.z > 360)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
