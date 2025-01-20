using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Void.Manager;

public class CutsceneEnter : MonoBehaviour
{ 
    public GameObject Boss;
    public GameObject cutsceneTimeline;

    [SerializeField] private float cutsceneTime;
    [SerializeField] private BoxCollider2D boxCollider;


    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartScene();
        }
    }
    private void StartScene()
    {
        StartCoroutine(StartBoss(cutsceneTime));
    }
    private IEnumerator StartBoss(float cutsceneTime)
    {
        cutsceneTimeline.SetActive(true);
        LevelManager.Instance.StartTimelineCutScene(cutsceneTime);
        yield return new WaitForSeconds(cutsceneTime);
        Boss.SetActive(true);
        cutsceneTimeline.SetActive(false);
        boxCollider.enabled = false;
    }
}