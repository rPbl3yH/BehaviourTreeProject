using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sensors
{
    public class TreeFindSensor : MonoBehaviour
    {
        [SerializeField] private TreeService _treeService;

        [SerializeField] private float _detectedDistance = 1f;

        [ShowInInspector]
        [ReadOnly]
        private Transform _currentTree;
    
        private void Update()
        {
            if (_currentTree)
            {
                CheckDistance();
            }
            else
            {
                _currentTree = GetClosetTree();
            }
        }

        private Transform GetClosetTree()
        {
            var detectedTrees = GetDetectedTrees(transform.position, _detectedDistance);
            
            if (detectedTrees.Count > 0)
            {
                var randomId = Random.Range(0, detectedTrees.Count);
                return detectedTrees[randomId].transform;
            }

            return null;
        }

        private void CheckDistance()
        {
            var distance = Vector3.Distance(_currentTree.transform.position, transform.position);
            if (distance > _detectedDistance)
            {
                _currentTree = null;
            }
        }

        private List<GameObject> GetDetectedTrees(Vector3 position, float detectedDistance)
        {
            var result = new List<GameObject>();

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
    }
}