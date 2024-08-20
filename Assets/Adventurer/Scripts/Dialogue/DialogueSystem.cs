using Adventurer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    public DialogueData DialogueData;

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;

    public float ReplicSpeed;

    private string _name;
    private List<string> _replic;
    
    private int _numReplic;
    private string _showReplic;

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
        Text.text = _replic[_numReplic];

        Name.text = _name;

        StartCoroutine(ShowReplic());
    }
    void ChangeReplic() 
    {
        if (_numReplic < _replic.Count - 1 && _replic[_numReplic] == _showReplic)
        {
            _numReplic++;
            Text.text = _replic[_numReplic];
        }
        else
        {
            Debug.Log("Конец диалога");
            Destroy(gameObject);
        }
    }

    IEnumerator ShowReplic()
    {
        foreach (var replic in _replic[_numReplic])
        {
            yield return new WaitForSeconds(ReplicSpeed);
            _showReplic += replic;
            Text.text = _showReplic;
        }
        yield break;
    }
}
