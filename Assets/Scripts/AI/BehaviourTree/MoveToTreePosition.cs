using System.Collections;
using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class MoveToTreePosition : BehaviourNode
    {
        [SerializeField] private Plugins.Blackboard.Blackboard _blackboard;
        
        [SerializeField] private float _speed;
        
        private Coroutine _coroutine;
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Vector3>(BlackboardKeys.MOVE_POSITION, out var movePosition) ||
                !_blackboard.TryGetVariable<Character>(BlackboardKeys.CHARACTER_ENTITY, out var character))
            {
                Return(false);
                return;
            }

            _coroutine = StartCoroutine(MoveToPositionCoroutine(character.transform, movePosition));
        }

        private IEnumerator MoveToPositionCoroutine(Transform character, Vector3 position)
        {
            var stoppingDistance = _blackboard.GetVariable<float>(BlackboardKeys.TREE_STOPPING_DISTANCE);
            
            while (true)
            {
                var distanceDirection = position - character.position;
                
                if (distanceDirection.magnitude <= stoppingDistance)
                {
                    Return(true);
                }
                
                var normalizedDirection = distanceDirection.normalized;
                character.position += normalizedDirection * (Time.deltaTime * _speed);
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