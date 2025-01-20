using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newChaseStateData", menuName = "Data/State Data/Chase State")]
public class ChaseStateData : ScriptableObject
{
    public float chargeSpeed = 6f;

    public float chargeTime = 2f;

    public AudioClip StateSound;

    public GameObject FX;
    public Vector2 FXPos;
}
