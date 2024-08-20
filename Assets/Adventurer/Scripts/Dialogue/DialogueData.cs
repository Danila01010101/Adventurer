using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventurer
{
    [CreateAssetMenu(fileName = "New DialogueData", menuName = "Dialogue Data", order = 51)]
    public class DialogueData : ScriptableObject
    {
        [SerializeField] private List<string> _replics;
        [SerializeField] private string _name;

        [Header("Command")]
        private string _debugLog = "/Debug";
        private string _menu = "/Menu";

        public string DebugLog
        { get { return _debugLog;} }
        public string Menu
        { get { return _menu;} }
        public List<string> Replic 
        { get { return _replics;} }
        public string Name
        { get { return _name; } }
    }
}
