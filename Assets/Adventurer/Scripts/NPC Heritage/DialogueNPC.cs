//������ ������
using UnityEngine;

namespace Adventurer
{
    public class DialogueNPC : NPC
    {
        public GameObject Window;

        void OpenDialogue() 
        {
            Window.SetActive(true);
            //� ����� �������
        }
    }
}
