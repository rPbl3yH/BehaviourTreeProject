using System.Collections;
using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class BehaviourNode_MoveToPosition : BehaviourNode
    {
        [SerializeField] private Plugins.Blackboard.Blackboard _blackboard;
        
        private Coroutine _coroutine;
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Vector3>(BlackboardKeys.MOVE_POSITION, out var movePosition) ||
                !_blackboard.TryGetVariable<Character>(BlackboardKeys.CHARACTER_ENTITY, out var character))
            {
                Return(false);
                return;
            }

            _coroutine = StartCoroutine(MoveToPositionCoroutine(character, movePosition));
        }

        private IEnumerator MoveToPositionCoroutine(Character character, Vector3 position)
        {
            var stoppingDistance = _blackboard.GetVariable<float>(BlackboardKeys.STOPPING_DISTANCE);
            
            while (true)
            {
                var distanceDirection = position - character.transform.position;
                
                if (distanceDirection.magnitude <= stoppingDistance)
                {
                    Return(true);
                }
                
                var normalizedDirection = distanceDirection.normalized;
                character.Move(normalizedDirection);
                yield return new WaitForFixedUpdate();
            }
        }

        protected override void OnReturn(bool success)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                
            }
        }

        protected override void OnAbort()
        {
            StopCoroutine(_coroutine);
            base.OnAbort();
        }
    }
}