using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Void.Manager;

namespace Void.UI
{
    public class ContinueButton : VoidMonoBehaviour
    {
        [SerializeField] private Button button;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.button = GetComponent<Button>();
        }
        // Use this for initialization
        protected override void Start()
        {
            if (SaveManager.Instance.IsNullData())
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }     
    }
}