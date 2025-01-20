using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Void;
using Void.Interaction;
using Void.Manager;

namespace Void.Interaction.Interactables
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CheckPoint : MonoBehaviour, IInteractable
    {
        public UnityEvent OnInteract;
        [SerializeField] private GameObject SavePoint;
        [SerializeField] private GameObject interactUI;
        [SerializeField] private GameObject Fire;
        private bool isActive;

        private void Start()
        {
            this.DisableInteraction();
            isActive = false;
        }
        public void DisableInteraction()
        {
            interactUI.SetActive(false);
        }
        public void EnableInteraction()
        {
            interactUI.SetActive(true);
        }
        public Vector3 GetPosition()
        {
            return transform.position;
        }
        public void Interact()
        {
            if(isActive == false)
            {
                Fire.SetActive(true);
                isActive = true;
            }
            OnInteract?.Invoke();
        }
        protected void Awake()
        {
            this.interactUI = transform.Find("InteractUI").gameObject;
        }  
    }
}