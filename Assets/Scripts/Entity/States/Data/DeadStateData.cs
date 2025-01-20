using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
public class DeadStateData : ScriptableObject
{
    public GameObject deathChunkParticle;
    public GameObject deathBloodParticle;
    public AudioClip StateSound;
}
