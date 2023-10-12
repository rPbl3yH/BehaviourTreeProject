using System.Collections;
using Lessons.AI.LessonBehaviourTree;
using Plugins.Blackboard;
using UnityEngine;

namespace Sample.AI.BehaviourTree
{
    public class MoveToTreePosition : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        private Coroutine _coroutine;
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Vector3>(BlackboardKeys.MOVE_POSITION, out var value))
            {
                Return(false);
            }

            _coroutine = StartCoroutine(MoveToPositionCoroutine(value));
        }

        private IEnumerator MoveToPositionCoroutine(Vector3 position)
        {
            var stoppingDistance = _blackboard.GetVariable<float>(BlackboardKeys.TREE_STOPPING_DISTANCE);
            
            while (true)
            {
                var distanceDirection = position - transform.position;
                if (distanceDirection.magnitude <= stoppingDistance)
                {
                    Return(true);
                }
                
                var normalizedDirection = distanceDirection.normalized;
                transform.position += normalizedDirection * Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        protected override void OnAbort()
        {
            StopCoroutine(_coroutine);
            base.OnAbort();
        }
    }
}