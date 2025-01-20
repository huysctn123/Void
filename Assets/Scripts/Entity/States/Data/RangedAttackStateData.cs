using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class RangedAttackStateData : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance;

    [Header("Movement")]
    public float Speed;

    [Header("Sound")]
    public AudioClip StateSound;
}
