using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Void.UI;

namespace Void.Interaction.Interactables
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Door : VoidMonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject interactUI;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private Animator anim;
        [SerializeField] private ShadowCaster2D shadow;

        private bool isOpen;
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.interactUI = transform.Find("InteractUI").gameObject;
            this.boxCollider = transform.Find("Collider").GetComponent<BoxCollider2D>();
            this.shadow = transform.Find("Collider").GetComponent<ShadowCaster2D>();
            this.anim = transform.Find("Graphics").GetComponent<Animator>(); ;
        }
        protected override void Start()
        {
            this.interactUI.SetActive(false);
            this.isOpen = false;
        }
        public void DisableInteraction()
        {
            if (isOpen) return;
            this.interactUI.SetActive(false);
        }

        public void EnableInteraction()
        {
            if (isOpen) return;
            this.interactUI.SetActive(true);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void Interact()
        {
            Debug.Log("DoorInteract");
            if (!this.isOpen)
            {
                OpenDoor();
                anim.SetTrigger("open");
                this.isOpen = true;
                shadow.castsShadows = false;
                this.interactUI.SetActive(false);

            }
            else return;
        }
        public void OpenDoor()
        {
            boxCollider.isTrigger = true;
        }

    }
}

