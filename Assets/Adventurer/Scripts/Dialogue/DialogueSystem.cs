using Adventurer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
// Однажды я обрету вечную мудрость и начну писать подобный код без ошибок. Каждый починеный баг\моя затупость будет комментироваться этим прекрасным символом "&"
// &&
public class DialogueSystem : MonoBehaviour
{
    public DialogueData DialogueData;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;

    public float ReplicSpeed;

    
    private int numReplic;
    private string showReplic;

    private DialogueControl input;
    //До первого кадра
    private void Awake()
    {
        input = new DialogueControl();
        //Привязка клавиш к событиям
        input.Dialogue.Skip.performed += context => ChangeReplic();
    }
    //Чёт не понятно
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    //Первый кадр и дальше
    void Start()
    {
        numReplic = 0;
        Text.text = DialogueData.Replic[numReplic];

        Name.text = DialogueData.Name;

        StartCoroutine(ShowReplic());
    }

    void ChangeReplic() 
    {
        if (numReplic < DialogueData.Replic.Count - 1)
        {
            StopAllCoroutines();
            numReplic++;

            if (DialogueData.Replic[numReplic] == DialogueCommands.Debug)
            {
                Debug.Log("Debug!");
                ChangeReplic();
                return;
            }

            if (DialogueData.Replic[numReplic] == DialogueCommands.Menu)
            {
                Debug.Log("Dialogue Menu");
                ChangeReplic();
                return;
            }

            if (DialogueData.Replic[numReplic] == DialogueCommands.Label)
            {
                ChangeReplic();
                return;
            }

            if (DialogueData.Replic[numReplic] == DialogueCommands.Jump)
            {
                MoveToLabel();
                ChangeReplic();
                return;
            }

            showReplic = "";
            StartCoroutine(ShowReplic());
        }
        else
        {
            Debug.Log("Конец диалога");
            Destroy(gameObject);
        }
    }

    void MoveToLabel()
    {
        foreach (var replic in DialogueData.Replic)
        {
            if(replic == DialogueCommands.Label + DialogueData.Replic[numReplic + 1])
            {
                
            }
        }
    }
    IEnumerator ShowReplic()
    {
        foreach (var replic in DialogueData.Replic[numReplic])
        {
            yield return new WaitForSeconds(ReplicSpeed);
            
            showReplic += replic;
            Text.text = showReplic;
        }
        yield break;
    }
}
