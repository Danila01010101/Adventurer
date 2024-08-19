using System;
using UnityEngine;
using UnityEngine.UI;

namespace EvolveGames
{
    public class MENU : MonoBehaviour
    {
        [Header("MENU")]
        [SerializeField] GameObject MenuPanel;
        [SerializeField] Animator ani;
        [Header("Input")]
        [SerializeField] KeyCode BackKey = KeyCode.Escape;

        [SerializeField] private Image _logo;

        public static Action GamePaused;
        public static Action GameStarted;

        public Image Logo => _logo;

        private void Update()
        {
            //if (Input.GetKeyDown(BackKey))
            //{
            //    if (MenuPanel.activeInHierarchy)
            //    {
            //        MenuPanel.SetActive(false);
            //        GameStarted?.Invoke();
            //        Cursor.visible = false;
            //        Cursor.lockState = CursorLockMode.Locked;
            //        Time.timeScale = 1.0f;
            //        ani.SetBool("START", false);
            //    }
            //    else
            //    {
            //        MenuPanel.SetActive(true);
            //        GamePaused?.Invoke();
            //        Cursor.visible = true;
            //        Cursor.lockState = CursorLockMode.None;
            //        Time.timeScale = 0.0f;
            //        ani.SetBool("START", true);
            //    }
            //}
        }
    }
}

   
