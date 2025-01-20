using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Data/Entity Data/ Base Data")]
public class EntityData : ScriptableObject
{
    [Header("Stat")]
    public float maxHealth = 30f;
    public float damageHopSpeed = 3f;
    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;
    public GameObject hitParticle;
    public GameObject deathParticle;

    [Header("WallCheck")]
    public float wallCheckDistance = 0.2f;
    public Vector2 WallCheckSize;

    [Header("LedgeCheck")]
    public float ledgeCheckDistance = 0.4f;

    [Header("GroundCheck")]
    public Vector2 GroundCheckSize;
    public LayerMask GroundLayer;

    [Header("PlayerCheck")]
    public float minAgroDistance = 3f;
    public float maxAgroDistance = 4f;
    public float closeRangeActionDistance = 1f;
    public LayerMask PlayerLayer;
}

