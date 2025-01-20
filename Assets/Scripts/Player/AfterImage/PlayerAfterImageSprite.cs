using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Void;


public class PlayerAfterImageSprite : MonoBehaviour
{
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField]
    private float alphaSet = 0.8f;
    [SerializeField]
    private float alphaDecay = 0.85f;

    [SerializeField] private Transform player;

    [SerializeField] private SpriteRenderer SR;
    [SerializeField] private SpriteRenderer playerSR;

    private Color color;
    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindObjectOfType<Player>().transform;
        playerSR = player.GetComponent<SpriteRenderer>();

    }
    private void OnEnable()
    {
        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }
    private void Update()
    {
        alpha -= alphaDecay * Time.deltaTime;
        color = new Color(0f, 0f, 0f, alpha);
        SR.color = color;
        if (Time.time >= (timeActivated + activeTime))
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }

    }
}

