using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventurer
{
    [CreateAssetMenu(fileName = "New ReplicData", menuName = "Data/Replic Data", order = 52)]
    public class ReplicData : ScriptableObject
    {
        public List<VariableReplic> Answer;
    }
}
