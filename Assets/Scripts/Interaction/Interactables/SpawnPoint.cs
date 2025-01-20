using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;


namespace Void.Interaction.Interactables
{
    [RequireComponent(typeof(BoxCollider2D))]

    public class SpawnPoint : VoidMonoBehaviour, IInteractable
    {
        public UnityEvent OnInteract;

        private bool isActive;

        [SerializeField] private ParticleSystem particle;
        private GameObject interactUI;

        [Header("Spawn Infor")]
        private int Direction;
        [SerializeField] private Vector2 spawnDirection;
        [SerializeField] private float spawnVelocity;
        [SerializeField] private Vector2 spawnOffset;

        [SerializeField] private GameObject[] spawnObj;
        public void DisableInteraction()
        {
            interactUI.SetActive(false);
        }
        public void EnableInteraction()
        {
            if (isActive) return;
            interactUI.SetActive(true);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Interact()
        {
            if (isActive) return;
            OnInteract?.Invoke();
            HandleInteract();
        }
        public void HandleInteract()
        {
            StartCoroutine(SpawnObj());
        }
        private IEnumerator SpawnObj()
        {
            for (int i = 0; i < spawnObj.Length; i++)
            {
                var spawnPoint = new Vector2(transform.position.x + spawnOffset.x, transform.position.y + spawnOffset.y);
                var _SpawnObj = Instantiate(spawnObj[i], spawnPoint, Quaternion.identity);
                var adjustedSpawnDirection = new Vector2(
                    spawnDirection.x * Direction,
                    spawnDirection.y
                );
                var rigibody2d = _SpawnObj.GetComponent<Rigidbody2D>();
                rigibody2d.velocity = adjustedSpawnDirection.normalized * spawnVelocity;
                Direction *= -1;
                yield return null;
            }
            interactUI.SetActive(false);
            isActive = true;
            gameObject.SetActive(false);
        }
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.interactUI = transform.Find("InteractUI").gameObject;
        }
        protected override void Start()
        {
            base.Start();
            interactUI.SetActive(false);
            isActive = false;
            Direction = 1;
        }
    }
}