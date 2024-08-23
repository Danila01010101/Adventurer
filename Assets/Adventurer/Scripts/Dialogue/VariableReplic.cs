using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Adventurer
{
    [Serializable]public class VariableReplic
    {
        public string Answer;
        public UnityEvent ReactToAnswer;
    }
}
