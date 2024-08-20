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

    
    private int _numReplic;
    private string _showReplic;

    private DialogueControl _input;
    //До первого кадра
    private void Awake()
    {
        _input = new DialogueControl();
        //Привязка клавиш к событиям
        _input.Dialogue.Skip.performed += context => ChangeReplic();
    }
    //Чёт не понятно
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
    //Первый кадр и дальше
    void Start()
    {
        _numReplic = 0;
        Text.text = DialogueData.Replic[_numReplic];

        Name.text = DialogueData.Name;

        StartCoroutine(ShowReplic());
    }

    void ChangeReplic() 
    {
        if (_numReplic < DialogueData.Replic.Count - 1)
        {
            StopAllCoroutines();
            _numReplic++;
            if (DialogueData.Replic[_numReplic] == DialogueData.DebugLog)
            {
                Debug.Log("Debug!");
                ChangeReplic();
                return;
            }
            if (DialogueData.Replic[_numReplic] == DialogueData.Menu)
            {
                Debug.Log("Dialogue Menu");
                ChangeReplic();
                return;
            }
            _showReplic = "";
            StartCoroutine(ShowReplic());
        }
        else
        {
            Debug.Log("Конец диалога");
            Destroy(gameObject);
        }
    }
    IEnumerator ShowReplic()
    {
        foreach (var replic in DialogueData.Replic[_numReplic])
        {
            yield return new WaitForSeconds(ReplicSpeed);
            
            _showReplic += replic;
            Text.text = _showReplic;
        }
        yield break;
    }
}
