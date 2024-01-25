using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Klem.Samples
{
    [CreateAssetMenu(fileName = "SomeSO", menuName = "Klem/Samples/SomeSO")]
    public class SomeSO : ScriptableObject
    {
        public List<GameObject> gameObjects;
    }
}
