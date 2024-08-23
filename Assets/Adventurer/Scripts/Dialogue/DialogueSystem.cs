using Adventurer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// (https://vk.com/video-149258223_456242495)
public class DialogueSystem : MonoBehaviour
{
    public DialogueData DialogueData;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;

    public float ReplicSpeed;
    
    private int numReplic;
    private int numLabel = 0;

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
        Text.text = DialogueData.Label[numLabel].Replic[numReplic];

        Name.text = DialogueData.Name;

        StartCoroutine(ShowReplic());
    }

    void ChangeReplic() 
    {
        if (numReplic < DialogueData.Label[numLabel].Replic.Count - 1)
        {
            StopAllCoroutines();
            numReplic++;

            if (DialogueData.Label[numLabel].Replic[numReplic] == DialogueCommands.Debug)
            {
                Debug.Log("Debug!");
                ChangeReplic();
                return;
            }

            if (DialogueData.Label[numLabel].Replic[numReplic] == DialogueCommands.Menu)
            {
                Debug.Log("Dialogue Menu");
                ShowMenu();
                return;
            }

            showReplic = "";
            StartCoroutine(ShowReplic());
        }
        else
        {
            numLabel = DialogueData.Label[numLabel].NextLabel;
            numReplic = 0;
            showReplic = "";
            StopAllCoroutines();
            StartCoroutine(ShowReplic());
        }
    }

    void ShowMenu()
    {

    }

    IEnumerator ShowReplic()
    {
        foreach (var replic in DialogueData.Label[numLabel].Replic[numReplic])
        {
            yield return new WaitForSeconds(ReplicSpeed);
            
            showReplic += replic;
            Text.text = showReplic;
        }
        yield break;
    }

   
}
