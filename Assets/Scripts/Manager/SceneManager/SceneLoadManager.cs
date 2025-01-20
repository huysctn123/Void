using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Void.UI;

namespace Void.Manager.Scene
{
    public class SceneLoadManager : MonoBehaviour
    {

        public static SceneLoadManager Instance;
        [SerializeField] private string currentSceneName;
        [SerializeField] private string loadSceneName;
        [SerializeField] private string transitionName;
        public string CurrentSceneName { get => currentSceneName; set => currentSceneName = value; }
        public string LoadSceneName { get => loadSceneName; set => loadSceneName = value; }
        public string TransitionName { get => transitionName; set => transitionName = value; }


        public Vector3 spawnPosition;
        public float loadDuration = 5f;


        private void Awake()
        {
            if (SceneLoadManager.Instance != null) Debug.LogError("Only SceneLoadManager allow");
            SceneLoadManager.Instance = this;
        }
        private void Start()
        {
            CurrentSceneName = SceneManager.GetActiveScene().name;
            loadDuration = 5f;
        }
        public void GetLoadScene(string name) => LoadSceneName = name;
        public void GetTransitions(string name) => TransitionName = name;
        public void GetDuration(float value) => loadDuration = value;

        public void GetSpawnPosition(Vector3 postion)
        {           
            spawnPosition = postion;
            SaveManager.Instance.SaveSpawnPosition(spawnPosition);
        }
        public void LoadScene()
        {
            LevelManager.Instance.LoadScene(LoadSceneName, TransitionName);
        }
        public void LoadCheckPoint()
        {
            GetTransitions("CrossFade");
            SaveManager.Instance.LoadCheckPointScene();
            LevelManager.Instance.LoadSaveGame(LoadSceneName, TransitionName, loadDuration);
        }
        public void PlayNewGame()
        {
            GetLoadScene("Level_1");
            GetTransitions("CrossFade");
            LevelManager.Instance.LoadMainScene(LoadSceneName, TransitionName);
        }
        public void BackToMenu()
        {
            GetLoadScene("MainMenu");
            GetTransitions("CrossFade");
            LevelManager.Instance.LoadMainScene(LoadSceneName, TransitionName);
        }
    }
}