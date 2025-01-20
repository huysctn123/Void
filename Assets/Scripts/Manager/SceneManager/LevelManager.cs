using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;
using static System.TimeZoneInfo;
using System;

namespace Void.Manager
{
    public class LevelManager : VoidMonoBehaviour
    {
        public event Action OnTransitionIn;
        public event Action OnTransitionOut;
        public event Action EnterTimeLine;
        public event Action ExitTimeLine;

        public static LevelManager Instance;

        public Slider progressBar;
        public GameObject transitionsContainer;


        [SerializeField] private SceneTransition[] Transitions;

        protected override void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        protected override void Start()
        {
            Transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
        }

        public void LoadScene(string sceneName, string transitionName)
        {
            StartCoroutine(LoadSceneAsync(sceneName, transitionName));
        }
        public void LoadSaveGame(string sceneName, string transitionName, float duration)
        {
            StartCoroutine(LoadCheckPoint(sceneName, transitionName, duration));
        }
        public void StartTimelineCutScene(float duration)
        {
            StartCoroutine(StartTimeLine(duration, "TwoLine"));
        }
        public void LoadMainScene(string sceneName, string transitionName)
        {
            StartCoroutine(LoadGame(sceneName, transitionName));
        }
        private IEnumerator LoadCheckPoint(string sceneName, string transitionName, float duration)
        {
            //Disable player input
            Player.Instance.SetInputActive(false);
            yield return new WaitForSeconds(duration);
            SceneTransition transition = Transitions.First(t => t.name == transitionName);
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;

            //Start Transition In
            yield return transition.AnimateTransitionIn();

            yield return new WaitForSeconds(1f);

            scene.allowSceneActivation = true;

            yield return new WaitForSecondsRealtime(1f);
            SaveManager.Instance.LoadCheckPointSave();
            //Start Transition Out
            yield return transition.AnimateTransitionOut();
            //Enable player input
            Player.Instance.SetInputActive(true);
        }
        private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
        {
            //Save Data to next scene
            SaveManager.Instance.SaveSceneData();
            OnTransitionIn?.Invoke();

            SceneTransition transition = Transitions.First(t => t.name == transitionName);
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;


            //Start Transition In
            yield return transition.AnimateTransitionIn();

            yield return new WaitForSeconds(1f);

            // Enable progressBar
            progressBar.gameObject.SetActive(true);

            do
            {
                progressBar.value = scene.progress;
                yield return null;
            } while (scene.progress < 0.9f);

            yield return new WaitForSeconds(1f);

            scene.allowSceneActivation = true;

            //disable progressBar
            progressBar.gameObject.SetActive(false);

            yield return new WaitForSeconds(1f);
            //Load Data
            SaveManager.Instance.LoadSceneData();

            yield return new WaitForSeconds(1f);
            //Start Transition Out
            yield return transition.AnimateTransitionOut();

            OnTransitionOut?.Invoke();
        }
        private IEnumerator StartTimeLine(float duration, string transitionName)
        {
            SceneTransition transition = Transitions.First(t => t.name == transitionName);
            EnterTimeLine?.Invoke();
            OnTransitionIn?.Invoke();
            yield return transition.AnimateTransitionIn();

            yield return new WaitForSeconds(duration);

            yield return transition.AnimateTransitionOut();
            ExitTimeLine?.Invoke();
            OnTransitionOut?.Invoke();
        }
        private IEnumerator LoadGame(string sceneName, string transitionName)
        {
            SaveManager.Instance.SaveVolume();

            SceneTransition transition = Transitions.First(t => t.name == transitionName);
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;

            //Start Transition In
            yield return transition.AnimateTransitionIn();

            yield return new WaitForSeconds(1f);

            // Enable progressBar
            progressBar.gameObject.SetActive(true);

            do
            {
                progressBar.value = scene.progress;
                yield return null;
            } while (scene.progress < 0.9f);

            yield return new WaitForSeconds(1f);

            scene.allowSceneActivation = true;

            //disable progressBar
            progressBar.gameObject.SetActive(false);

            yield return new WaitForSeconds(1f);

            SaveManager.Instance.LoadVolume();
            yield return new WaitForSeconds(1f);
            //Start Transition Out
            yield return transition.AnimateTransitionOut();
        }
    }
}