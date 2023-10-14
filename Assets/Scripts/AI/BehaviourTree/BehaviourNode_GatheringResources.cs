using System.Collections;
using System.Collections.Generic;
using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;
using Tree = Sample.Tree;

namespace AI.BehaviourTree
{
    public class BehaviourNode_GatheringResources : BehaviourNode
    {
        [SerializeField] 
        private Plugins.Blackboard.Blackboard _blackboard;

        [SerializeField] 
        private float _gatheringDelay = 1f;
        
        private Coroutine _coroutine;
        private Character _character;
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Tree>(BlackboardKeys.TREE, out var tree) ||
                !_blackboard.TryGetVariable<Character>(BlackboardKeys.CHARACTER_ENTITY, out var character))
            {
                Return(false);
                return;
            }

            _character = character;
            _coroutine = StartCoroutine(StartGatheringCoroutine(tree));
        }

        private IEnumerator StartGatheringCoroutine(Tree tree)
        {
            while (!_character.IsResourceBagFull())
            {
                if (!tree.HasResources())
                {
                    Return(false);
                    yield break;
                }
                
                _character.Chop(tree);

                yield return new WaitForSeconds(_gatheringDelay);
            }   
            
            Return(true);
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