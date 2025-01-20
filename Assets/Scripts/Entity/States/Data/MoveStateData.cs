using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]
public class MoveStateData : ScriptableObject
{
    public float MovementSpeed = 5f;
    public AudioClip StateSound;
    public GameObject FX;
}
