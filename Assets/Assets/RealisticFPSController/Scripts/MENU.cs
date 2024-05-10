using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace EvolveGames
{
    public class MENU : MonoBehaviour
    {
        [Header("MENU")]
        [SerializeField] GameObject MenuPanel;
        [SerializeField] Animator ani;
        [Header("Input")]
        [SerializeField] KeyCode BackKey = KeyCode.Escape;

        private PlayerController Player;
        [SerializeField] private Image _logo;

        public Image Logo => _logo;

        [Inject]
        private void Construct(PlayerController player) => Player = player;

        private void Update()
        {
            if (Input.GetKeyDown(BackKey))
            {
                if (MenuPanel.activeInHierarchy)
                {
                    MenuPanel.SetActive(false);
                    Player.EnterPause();
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1.0f;
                    ani.SetBool("START", false);
                }
                else
                {
                    MenuPanel.SetActive(true);
                    Player.ExitPause();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0.0f;
                    ani.SetBool("START", true);
                }
            }
        }
    }
}

   
