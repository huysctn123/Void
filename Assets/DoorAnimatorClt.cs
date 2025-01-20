using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Void
{
    public class DoorAnimatorClt : MonoBehaviour
    {
        Animator animator;
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OpenDoor()
        {
            animator.Play("DoorOpen");
        }
        public void CloseDoor()
        {
            animator.Play("DoorClose");
        }
        public void DoorNormal()
        {
            animator.Play("DoorNormal");
        }
    }
}
