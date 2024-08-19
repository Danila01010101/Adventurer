using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    public string NPCName;
    public List<string> Replic;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;

    private int _numReplic;

    private DialogueControl _input;
    private void Awake()
    {
        _input = new DialogueControl();
        _input.Dialogue.Skip.performed += context => ChangeReplic();
    }

    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }

    void Start()
    {
        _numReplic = 0;
        Text.text = Replic[_numReplic];

        Name.text = NPCName;
    }

    void ChangeReplic() 
    {
        if (_numReplic < Replic.Count - 1)
        {
            _numReplic++;
            Text.text = Replic[_numReplic];
        }
        else
        {
            OnDisable();
        }
    }
}
