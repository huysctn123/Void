using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{

    [Header("DefaultVariables")]
    public float DefaultGravity = 5f;
    public float crouchColliderHeight = 1.8f;
    public float standColliderHeight = 2.6f;


    [Header("Move State")]
    public float MovementSpeed = 8f;
    public AudioClip PlayerWalkSound;

    [Header("Crouch State")]
    public float CrouchSpeed = 3f;

    [Header("Jump State")]
    public int AmountOfJumps = 2;
    public float JumpFocre = 15f;
    public AudioClip playerJumpSound;


    [Header("InAir State")]
    public float CoyoteTime = 0.1f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float FallSpeed = 7f;
    public float MaxFallSpeed = 25f;
    public float FallGravity = 5f;

    [Header("Wall Jump State")]
    public float WallJumpVelocity = 25f;
    public float WallJumpTime = 0.4f;
    public Vector2 WallJumpAngle = new Vector2(1, 2);

    [Header("Land State")]
    public GameObject LandFX;


    [Header("Wall Silde State")]
    public float WallSildeVelocity = 6f;

    [Header("Wall Climb State")]
    public float WallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 StartOffset;
    public Vector2 StopOffset;
    [Header("Dash State")]
    public float DashCooldown = 0.3f;
    public float DashVelocity = 30f;
    public float DashTime = 0.2f;
    public float distBetweenAfterImages = 1f;
    public GameObject DashFX;


    [Header("Stun State")]
    public float StunTime = 0.5f;

    [Header("UsePotion")]
    public AudioClip UsePotionAudio;

    [Header("FX")]
    public GameObject HealFX;
    public GameObject ManaFX;

    [Header("Hurt")]
    public AudioClip PlayerHurtSound;

}
