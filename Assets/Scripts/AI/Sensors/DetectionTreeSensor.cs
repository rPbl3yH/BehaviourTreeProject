using System;
using System.Collections.Generic;
using Core.Service;
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

        [SerializeField] 
        private Transform _centerPoint;
        
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
            var trees = _treeService.GetAvailableTrees();
            if (trees.Count == 0)
            {
                return null;
            }

            var closetTree = trees[0];
            var closetDistance = Vector3.Distance(_centerPoint.position, closetTree.transform.position);            
            
            for (int i = 0; i < trees.Count; i++)
            {
                var tree = trees[i];
                if (!tree.HasResources())
                {
                    continue;
                }

                var distance = Vector3.Distance(_centerPoint.position, tree.transform.position);
                
                if (distance < closetDistance)
                {
                    closetTree = tree;
                }
            }

            return closetTree;
        }

        private void SetClosetTree(Tree point)
        {
            _currentTree = point;
            ClosetTreeFound?.Invoke(_currentTree);
        }
    }
}