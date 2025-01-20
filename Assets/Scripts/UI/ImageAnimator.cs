using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Void.UI
{
    public class ImageAnimator : VoidMonoBehaviour
    {
        private SpriteRenderer sprite;

        [SerializeField] private Image image;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.image = GetComponent<Image>();
            this.sprite = GetComponentInChildren<SpriteRenderer>();
        }
        private void Update()
        {
            this.image.sprite = this.sprite.sprite;
        }
    }
}