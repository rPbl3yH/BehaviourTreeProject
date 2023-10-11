using System.Collections.Generic;
using UnityEngine;

namespace Sensors
{
    public class TreeService : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _trees = new();

        public List<GameObject> Trees => _trees;
    }
}