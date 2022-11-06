using System;
using CodeBase.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.GameResources
{
    public class StartScene : MonoBehaviour
    {
        public Button StartGame;
        public Button QuitGame;
        public CanvasGroup Invalid;

        private void OnEnable()
        {
            StartGame.onClick.AddListener(OnStartGame);
            QuitGame.onClick.AddListener(OnQuitGame);
        }

        private void OnDisable()
        {
            StartGame.onClick.RemoveListener(OnStartGame);
            QuitGame.onClick.RemoveListener(OnQuitGame);
        }

        private void OnQuitGame()
        {
            if (Invalid != null)
            {
                Message();
            }
            else
            {
                //Application.Quit();
            }
        }

        private void OnStartGame() => 
            SceneManager.LoadScene("Initial");
        
        private void Message()
        {
            Invalid.DOKill();
            Invalid.gameObject.SetActive(true);
            Invalid.DOFade(1f, 0.2f).OnComplete(() => Invalid.DOFade(0f, 2f).OnComplete(() => Invalid.gameObject.SetActive(false)));
        }
    }
}