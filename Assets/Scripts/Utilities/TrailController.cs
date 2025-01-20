using System.Collections;
using UnityEngine;

namespace Void.Utilities
{
    public class TrailController : VoidMonoBehaviour
    {

        [SerializeField] private TrailRenderer trailRenderer;
        public Texture[] textures;

        private float fpsCouter;
        private int animationStep = 0;

        public int fps = 30;
        protected override void LoadComponents()
        {
            base.LoadComponents();
            trailRenderer = GetComponent<TrailRenderer>();
        }
        protected override void Start()
        {
            base.Start();
            trailRenderer.material.SetTexture("_MainTex", textures[animationStep]);
        }
        private void Update()
        {
            fpsCouter += Time.deltaTime;
            if (fpsCouter >= 1f / fps)
            {
                animationStep++;
                if (animationStep == textures.Length) animationStep = 0;

                trailRenderer.material.SetTexture("_MainTex", textures[animationStep]);

                fpsCouter = 0f;
            }
        }
    }
}