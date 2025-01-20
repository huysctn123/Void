using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Angle;

    void Update()
    {
        transform.Rotate(0, 0, Angle * Time.deltaTime);
    }
}

