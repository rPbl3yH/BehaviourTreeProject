using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using Sample;
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
            if (!_blackboard.TryGetVariable<Barn>(BlackboardKeys.BARN_ENTITY, out var barn))
            {
                Return(false);
            }

            _targetPosition = barn.transform.position;
            _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, barn.transform.position);
            
            Return(true);
        }
    }
}