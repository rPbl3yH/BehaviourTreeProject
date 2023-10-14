using System.Collections.Generic;
using UnityEngine;
using Tree = Sample.Tree;

namespace Core.Service
{
    public class TreeService : MonoBehaviour
    {
        [SerializeField] private List<Tree> _trees = new();

        public List<Tree> GetAvailableTrees()
        {
            var result = new List<Tree>();
            
            for (int i = 0; i < _trees.Count; i++)
            {
                if (_trees[i].HasResources() && _trees[i].isActiveAndEnabled)
                {
                    result.Add(_trees[i]);
                }    
            }

            return result;
        }
    }
}