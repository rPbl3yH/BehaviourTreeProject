using System.Collections.Generic;
using UnityEngine;
using Tree = Sample.Tree;

namespace Sensors
{
    public class TreeService : MonoBehaviour
    {
        [SerializeField] private List<Tree> _trees = new();

        public List<Tree> Trees => _trees;
    }
}