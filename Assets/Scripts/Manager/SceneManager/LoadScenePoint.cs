using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


namespace Void.Manager.Scene
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LoadScenePoint : VoidMonoBehaviour
    {

        //Data
        private string transitionsName;
        [SerializeField] private SpawnPositionSO spawnPositionData;
        [SerializeField] private Transitions transitions;

        [Header("Scene")]
        [SerializeField] private SceneField scene;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                HandleOnTrigger();
                SceneLoadManager.Instance.LoadScene();    
            }
        }
        private void HandleOnTrigger()
        {           

            SceneLoadManager.Instance.GetTransitions(transitionsName);
            SceneLoadManager.Instance.GetSpawnPosition(spawnPositionData.position);
            SceneLoadManager.Instance.GetLoadScene(scene.SceneName);
        }
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            this.transitionsName = transitions.ToString();
        }
    }
}