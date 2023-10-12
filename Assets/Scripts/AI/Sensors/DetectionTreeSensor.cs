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
        

        [SerializeField] private TreeService _treeService;

        [SerializeField] private float _detectedDistance = 1f;

        [ShowInInspector]
        [ReadOnly]
        private Tree _currentTree;
    
        private void Update()
        {
            if (_currentTree)
            {
                CheckDistance();
            }
            else
            {
                SetClosetTree(GetClosetTree());
            }
        }

        private Tree GetClosetTree()
        {
            var detectedTrees = GetDetectedTrees(transform.position, _detectedDistance);
            
            if (detectedTrees.Count > 0)
            {
                var randomId = Random.Range(0, detectedTrees.Count);
                return detectedTrees[randomId];
            }

            return null;
        }

        private void CheckDistance()
        {
            var distance = Vector3.Distance(_currentTree.transform.position, transform.position);
            if (distance > _detectedDistance)
            {
                SetClosetTree(null);
            }
        }

        private List<Tree> GetDetectedTrees(Vector3 position, float detectedDistance)
        {
            var result = new List<Tree>();

            for (int i = 0; i < _treeService.Trees.Count; i++)
            {
                var distance = Vector3.Distance(_treeService.Trees[i].transform.position, position);
            
                if (distance <= detectedDistance)
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