using System;
using System.Collections.Generic;
using Sensors;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using Tree = Sample.Tree;

namespace AI.Sensors
{
    public class DetectionTreeSensor : MonoBehaviour
    {
        public event Action<Tree> ClosetTreeFound;
        
        [SerializeField] 
        private TreeService _treeService;

        [ShowInInspector]
        [ReadOnly]
        private Tree _currentTree;
    
        private void Update()
        {
            if (_currentTree)
            {
                if (!_currentTree.HasResources())
                {
                    SetClosetTree(null);
                }
            }
            else
            {
                SetClosetTree(GetClosetTree());
            }
        }

        private Tree GetClosetTree()
        {
            var detectedTrees = GetDetectedTrees();
            
            if (detectedTrees.Count > 0)
            {
                var randomId = Random.Range(0, detectedTrees.Count);
                return detectedTrees[randomId];
            }

            return null;
        }

        private List<Tree> GetDetectedTrees()
        {
            var result = new List<Tree>();

            for (int i = 0; i < _treeService.Trees.Count; i++)
            {
                if (_treeService.Trees[i].HasResources())
                {
                    result.Add(_treeService.Trees[i]);
                }
            }

            return result;
        }

        private void SetClosetTree(Tree point)
        {
            _currentTree = point;
            ClosetTreeFound?.Invoke(_currentTree);
        }
    }
}