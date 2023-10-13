using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using Sirenix.OdinInspector;
using UnityEngine;
using Tree = Sample.Tree;

namespace AI.BehaviourTree
{
    public class AssigmentTreePosition : BehaviourNode
    {
        [SerializeField] 
        private Plugins.Blackboard.Blackboard _blackboard;

        [ShowInInspector, ReadOnly] 
        private Vector3 _targetPosition; 
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Tree>(BlackboardKeys.TREE, out var value))
            {
                Return(false);
            }

            _targetPosition = value.transform.position;
            _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, value.transform.position);
            
            Return(true);
        }
    }
}