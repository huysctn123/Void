using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBG : MonoBehaviour
{
    public Vector3 EndPoint;
    public Vector3 StartPoint;
    [Range(-1, 1)]
    public int direction;
    public float moveSpeed;
    public float closeDistance = 1f;
    public float distance;
    private void FixedUpdate()
    {
        this.distance = Vector3.Distance(this.transform.position, EndPoint);
        if (distance <= closeDistance)
        {
            transform.position = StartPoint;
        }
        else
        {
            transform.position += new Vector3(moveSpeed * direction, 0f, 0f);
        }
    }
}


