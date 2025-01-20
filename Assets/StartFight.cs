using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : MonoBehaviour
{
    [SerializeField] public GameObject[] enableObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player");
            foreach(GameObject gameObject in enableObj)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
