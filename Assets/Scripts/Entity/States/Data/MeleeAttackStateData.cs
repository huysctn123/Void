using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class MeleeAttackStateData  : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;

    public Vector2 knockbackAngle = Vector2.one;
    public float knockbackStrength = 10f;

    public float PoiseDamage;

    public float Speed;
    public LayerMask PlayerLayer;

    [Header("Sound")]
    public AudioClip StateSound;

    [Header("FX")]
    public GameObject FX;
}

