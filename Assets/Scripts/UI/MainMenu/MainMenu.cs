using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Void.Manager;
using Void.Manager.GameManager;
using Void.Manager.Scene;

namespace Void.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneLoadManager.Instance.PlayNewGame();
        }
        public void ContinueGame()
        {
            SceneLoadManager.Instance.GetDuration(0.1f);
            SceneLoadManager.Instance.LoadCheckPoint();
        }
        public void Setting()
        {
            
        }
        public void Credits()
        {

        }
        public void MainMenuScene()
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Gameplay);
            SceneLoadManager.Instance.BackToMenu();
        }
        public void QuitGame()
        {
            Debug.Log("quit Game");
            Application.Quit();
        }
    }
}


