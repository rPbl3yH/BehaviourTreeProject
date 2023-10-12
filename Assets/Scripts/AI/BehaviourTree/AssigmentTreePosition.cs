using Lessons.AI.LessonBehaviourTree;
using Plugins.Blackboard;
using UnityEngine;

namespace Sample.AI.BehaviourTree
{
    public class AssigmentTreePosition : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Tree>(BlackboardKeys.TREE, out var value))
            {
                Return(false);
            }
            
            _blackboard.SetVariable(BlackboardKeys.MOVE_POSITION, value.transform.position);
            
            Return(true);
        }
    }
}