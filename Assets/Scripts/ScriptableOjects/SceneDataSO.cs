using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "newScenceLoadData", menuName = "Data/Scence Data/Scence", order = 0)]

public class SceneDataSO : ScriptableObject
{

    public SceneField scene;
    [SerializeField] public string sceneName { get => sceneName; private set => sceneName = scene.SceneName; }
}


