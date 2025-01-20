using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "newSpawnPosition", menuName = "Data/Scence Data/Spawn Position", order = 0)]
public class SpawnPositionSO : ScriptableObject
{
    public Vector3 position;
}
