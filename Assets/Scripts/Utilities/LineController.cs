using System.Collections;
using UnityEngine;

namespace Void.Utilities
{
    public class LineController : VoidMonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        #region ANIMATION

        [Header("Animation")]
        public Texture[] textures;

        private float fpsCouter;
        private int animationStep = 0;

        public int fps = 30;
        #endregion
        #region TRAIL
        [Header("Trail")]
        public int length;
        public Vector3[] segmentPoses;
        private Vector3[] segmentV;

        public Transform targerDir;
        public float targetDist;
        public float smoothSpeed;
        public float trailSpeed;
        #endregion

        protected override void LoadComponents()
        {
            base.LoadComponents();
            lineRenderer = GetComponent<LineRenderer>();
        }
        protected override void Start()
        {
            base.Start();
            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            lineRenderer.positionCount = length;
            segmentPoses = new Vector3[length];
            segmentV = new Vector3[length];
        }
        private void Update()
        {
            //changed texture
            fpsCouter += Time.deltaTime;
            if (fpsCouter >= 1f / fps)
            {
                animationStep++;
                if (animationStep == textures.Length) animationStep = 0;

                lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

                fpsCouter = 0f;
            }

            //
            segmentPoses[0] = targerDir.position;
            for (int i = 1; i < segmentPoses.Length; i++)
            {
                Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDist;
                segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentV[i], smoothSpeed);
            }
            lineRenderer.SetPositions(segmentPoses);
        }

    }
}