using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public List<string> Replic;
    public TextMeshProUGUI TextMeshProUGUI;

    public int _numReplic;
    void Start()
    {
        _numReplic = 0;
        TextMeshProUGUI.text = Replic[_numReplic];
    }

    void Update()
    {
        Debug.Log(Replic.Count < _numReplic);
        if (Input.GetMouseButtonDown(0) && Replic.Count - 1 > _numReplic)
        {
            _numReplic++;
            TextMeshProUGUI.text = Replic[_numReplic];
        }
    }
}
