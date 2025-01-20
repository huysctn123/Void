using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
public class PlayerDetectedStateData : ScriptableObject
{
    public float longRangeActionTime = 1.5f;
    public AudioClip StateSound;
}
