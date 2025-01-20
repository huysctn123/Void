using System.Collections;
using UnityEngine;

namespace Void.Projectiles
{
    public class ProjectileFalling : MonoBehaviour
    {

        [SerializeField] private float gravity;
        private float xStartPos;
        private float travelDistance;

        private bool hasHitGround;

        private projectileStickToCollider projectileStickToCollider;
        private Projectile projectile;
        private Rigidbody2D rb;

        private void Start()
        {
            projectile = GetComponent<Projectile>();
            rb = projectile.rb;
            hasHitGround = projectile.hasHitGround;
            rb.gravityScale = 0.0f;
            projectile.IsGravityOn = false;
            xStartPos = transform.position.x;
        }
        private void Update()
        {
            if (!hasHitGround)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        private void FixedUpdate()
        {
            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !projectile.IsGravityOn)
            {
                projectile.IsGravityOn = true;
                rb.gravityScale = gravity;
            }
        }
    }
}