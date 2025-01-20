using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ShowHealthBar : MonoBehaviour
{
    public void enableHealthBar()
    {
        this.gameObject.SetActive(true);
    }
    public void DisableHealthBar()
    {
        this.gameObject.SetActive(false);
    }
}

