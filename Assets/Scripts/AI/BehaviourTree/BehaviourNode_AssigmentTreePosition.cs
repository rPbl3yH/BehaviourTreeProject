using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using Sirenix.OdinInspector;
using UnityEngine;
using Tree = Sample.Tree;

namespace AI.BehaviourTree
{
    public class BehaviourNode_AssigmentTreePosition : BehaviourNode
    {
        [SerializeField] 
        private Plugins.Blackboard.Blackboard _blackboard;

        [Header("Debug")]
        [ShowInInspector, ReadOnly] 
        private Vector3 _targetPosition; 
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Tree>(BlackboardKeys.TREE, out var tree) ||
                !_blackboard.TryGetVariable<Character>(BlackboardKeys.CHARACTER_ENTITY, out var character))
            {
                Return(false);
                return;
            }

            if (character.IsResourceBagFull())
            {
                Return(false);
                return;
            }

            _targetPosition = tree.transform.position;
            _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, tree.transform.position);
            
            Return(true);
        }
    }
}