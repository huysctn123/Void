using UnityEngine;

namespace Void.Projectiles
{
    public class Projectile : VoidMonoBehaviour
    {
        [SerializeField] private bool isCtl = true;
        [SerializeField] private float speed;
        private float travelDistance;
        private bool isGravityOn;

        public Rigidbody2D rb;
        public BoxCollider2D boxCollider;
        public bool hasHitGround { get; private set; }
        [HideInInspector] public bool IsGravityOn { get => isGravityOn; set => isGravityOn = value; }

        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private LayerMask whatIsPlayer;
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.rb = GetComponent<Rigidbody2D>();
            this.boxCollider = GetComponent<BoxCollider2D>();

            hasHitGround = false;
        }
        protected override void Start()
        {
            base.Start();
            if (isCtl)
            {
                SetVelocity(speed);
            }
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {

            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                hasHitGround = true;
            }
        }
        public void FireProjectile(float speed, float travelDistance)
        {
            this.speed = speed;
            this.travelDistance = travelDistance;
            rb.velocity = transform.right * speed;
        }
        public void FireProjectile(Vector2 force)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        public void SetVelocity(float amount)
        {
            rb.velocity = transform.right * speed;
        }
    }
}