using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Void.UI
{
    public class BillBoard : VoidMonoBehaviour
    {
        public Transform cam;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.cam = GameObject.Find("Main Camera").transform;
        }
        void Update()
        {
            TryGetCam();
        }
        private void TryGetCam()
        {
            if (cam is null) return;
            transform.LookAt(transform.position + cam.forward);
        }
    }
}


