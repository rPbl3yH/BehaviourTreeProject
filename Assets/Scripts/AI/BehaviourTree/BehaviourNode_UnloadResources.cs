using System.Collections;
using AI.Blackboard;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

namespace AI.BehaviourTree
{
    public class BehaviourNode_UnloadResources : BehaviourNode
    {
        [SerializeField] 
        private Plugins.Blackboard.Blackboard _blackboard;

        private Barn _barn;
        private Character _character;

        private Coroutine _coroutine;
        
        protected override void Run()
        {
            if (!_blackboard.TryGetVariable<Barn>(BlackboardKeys.BARN_ENTITY, out var barn) ||
                !_blackboard.TryGetVariable<Character>(BlackboardKeys.CHARACTER_ENTITY, out var character))
            {
                Return(false);
                return;
            }

            _barn = barn;
            _character = character;
            _coroutine = StartCoroutine(MoveResourcesCoroutine(barn));
        }

        private IEnumerator MoveResourcesCoroutine(Barn barn)
        {
            if (!_character.IsResourceBagFull())
            {
                print("Character bag is not full!");
                Return(false);
                yield break;
            }
            
            var resourceCount = _character.UnloadResources();
            var canAddResources = barn.CanAddResources(resourceCount);
            
            if (canAddResources)
            {
                UnloadResources(resourceCount);
                Return(true);
            }
            else
            {
                while (!canAddResources)
                {
                    yield return new WaitForSeconds(0.1f);
                    canAddResources = barn.CanAddResources(resourceCount);
                }
                
                UnloadResources(resourceCount);
                Return(true);
            }
        }

        protected override void OnReturn(bool success)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }

        private void UnloadResources(int resourceCount)
        {
            _barn.AddResources(resourceCount);
            print("Add resources = " + resourceCount);
        }
    }
}