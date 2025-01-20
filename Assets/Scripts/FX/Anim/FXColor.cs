using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Void.FX
{
    public class FXColor : MonoBehaviour
    {
        public Color currentColor;
        [SerializeField] private Animator anim;
        private void Start()
        {
            ChangeColor(currentColor);
        }
        public void ChangeColor(Color color)
        {
            switch (color)
            {
                case Color.purple:
                    anim.SetTrigger("purple");
                    break;
                case Color.Green:
                    anim.SetTrigger("green");
                    break;
                case Color.BlueWhite:
                    anim.SetTrigger("blueWhite");
                    break;
            }
        }
        public enum Color
        {
            purple,
            Green,
            BlueWhite
        }
    }

}