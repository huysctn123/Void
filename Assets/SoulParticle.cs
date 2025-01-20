using DG.Tweening;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Void.Manager;


public class SoulParticle : MonoBehaviour
{

    public UnityEvent OnHitPlayer;

    public int soulAmount = 1;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform target;
    private float startTime;
    private float delayTime = 2f;
    private float speed = 5f;

    private void Start()
    {
        target = null;
        startTime = Time.time;
        RandomAmount();
        ScaleByAmount();
    }
    private void FixedUpdate()
    {
        HandlMoveToTarget();   
    }
    private void CheckTarget()
    {
        var hit = Physics2D.OverlapCircle(this.transform.position, radius, playerLayer);
        if (hit == null) return;
        target = hit.transform;
    }
    private void Update()
    {
        CheckTarget();
    }
    private void HandlMoveToTarget()
    {
        if (target is null) return;
        if (Time.time <= startTime + delayTime) return;
        StartCoroutine(MoveToTarget());
    }
    private IEnumerator MoveToTarget()
    {
        do
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
        }
        while (transform.position == target.position); 
        yield return null;
    }
    private void RandomAmount()
    {
        this.soulAmount = Random.Range(1, 5);
    }
    private void ScaleByAmount()
    {
        switch (soulAmount)
        {
            case 1:
                this.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                break;
            case 2:
                this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case 3:
                this.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                break;
            case 4:
                this.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            case 5:
                this.gameObject.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
                break;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnHitPlayer?.Invoke();
            ScoreManager.Instance.Increase(soulAmount);
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

}

