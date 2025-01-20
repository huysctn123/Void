using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class testUI : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {      
        this.button = GetComponent<Button>();
    }

    private void Start()
    {
    }
    public void testButton()
    {
        print("click");
    }
    private void OnEnable()
    {
        button.onClick.AddListener(testButton);
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(testButton);
    }
}
