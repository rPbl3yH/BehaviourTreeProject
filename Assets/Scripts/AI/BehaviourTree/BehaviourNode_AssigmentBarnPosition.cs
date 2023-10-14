using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class BehaviourNode_AssigmentBarnPosition : BehaviourNode
    {
        [SerializeField] 
        private Plugins.Blackboard.Blackboard _blackboard;

        [ShowInInspector, ReadOnly] 
        private Vector3 _targetPosition; 
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Vector3>(BlackboardKeys.BARN_POSITION, out var barnPosition))
            {
                Return(false);
            }

            _targetPosition = barnPosition;
            _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, barnPosition);
            
            Return(true);
        }
    }
}