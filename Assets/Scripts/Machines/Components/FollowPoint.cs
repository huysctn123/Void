using System.Collections;
using UnityEngine;

namespace Void.Machine
{
    public class FollowPoint : MachineComponents
    {
        public Transform[] point;

        public float Speed;

        private int pointIndex;

        protected override void Start()
        {
            base.Start();
            this.transform.position = point[pointIndex].position;
        }

        protected void Update()
        {
            MoveToPoint();
        }
        private void MoveToPoint()
        {
            if(this.transform.position == point[pointIndex].position)
            {
                pointIndex++;
                if (pointIndex >= point.Length)
                {
                    pointIndex = 0;

                }
            }
            else
            {
                this.transform.position = Vector2.MoveTowards(transform.position, point[pointIndex].position, Speed * Time.deltaTime);
            }
        }
    }
}