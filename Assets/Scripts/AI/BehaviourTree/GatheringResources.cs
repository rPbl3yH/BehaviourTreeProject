using System.Collections;
using System.Collections.Generic;
using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;
using Tree = Sample.Tree;

namespace AI.BehaviourTree
{
    public class GatheringResources : BehaviourNode
    {
        [SerializeField] 
        private Plugins.Blackboard.Blackboard _blackboard;

        [SerializeField] 
        private float _gatheringDelay = 1f;
        
        private Coroutine _coroutine;
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Tree>(BlackboardKeys.TREE, out var tree))
            {
                Return(false);
                return;
            }

            _coroutine = StartCoroutine(StartGatheringCoroutine(tree));
        }

        private IEnumerator StartGatheringCoroutine(Tree tree)
        {
            while (true)
            {
                if (tree.HasResources())
                {
                    tree.TakeResource();
                }
                else
                {
                    Return(true);
                }

                yield return new WaitForSeconds(_gatheringDelay);
            }       
        }

        protected override void OnReturn(bool success)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }
}