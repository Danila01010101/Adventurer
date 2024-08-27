using Adventurer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// (https://vk.com/video-149258223_456242495)
public class DialogueSystem : MonoBehaviour
{
    public DialogueData DialogueData;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;
    public GameObject AnswerPanel;
    [Header("Меню выбора реплики")]
    public List<Button> Buttons = new List<Button>();
    public List<TextMeshProUGUI> Answers = new List<TextMeshProUGUI>();
    [Header("Скорость текста")]
    public float ReplicSpeed;
    
    private int numReplic;
    private int numLabel = 0;

    private string showReplic;
    private int countReplic;

    private bool activeChangeReplic = true;
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

        foreach (Button button in Buttons)
        {
            button.gameObject.SetActive(false);
        }

        AnswerPanel.SetActive(false);
        StartCoroutine(ShowReplic());
    }

    void ChangeReplic() 
    {
        if (activeChangeReplic == false)
        {
            return;
        }

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

            showReplic = "";
            StartCoroutine(ShowReplic());
        }

        if (DialogueData.Label[numLabel].Answers == null)
        {
            numLabel = DialogueData.Label[numLabel].NextLabel;
            numReplic = 0;
            showReplic = "";
            StopAllCoroutines();
            StartCoroutine(ShowReplic());
        }

        else
        {
            ShowMenu();
        }
    }

    void ShowMenu()
    {
        activeChangeReplic = false;
        AnswerPanel.SetActive(true);
        foreach (Answer answer in DialogueData.Label[numLabel].Answers)
        {
            countReplic++;
        }

        for (int i = 0; i < countReplic; i++)
        {
            Buttons[i].gameObject.SetActive(true);
            Answers[i].text = DialogueData.Label[numLabel].Answers[i].AnswerText;
        }
    }

    public void ChoiseReplic(int num)
    {
        activeChangeReplic = true;
        numLabel = DialogueData.Label[numLabel].Answers[num].MoveTo;
        Debug.Log("NumLabel: " + numLabel);
        numReplic = 0;
        showReplic = "";
        
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
