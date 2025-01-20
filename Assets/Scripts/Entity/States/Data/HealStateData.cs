using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newHealStateData", menuName = "Data/State Data/Heal State")]

public class HealStateData : ScriptableObject
{
    public int amount = 10;
    public float coolDownTime = 1f;
    public AudioClip StateSound;
    public GameObject FX;
}
