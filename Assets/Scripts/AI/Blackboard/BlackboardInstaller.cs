using Sample;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.Blackboard
{
    [RequireComponent(typeof(Plugins.Blackboard.Blackboard))]
    public class BlackboardInstaller : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Transform _barnPoint;
        [SerializeField] private float _stoppingDistance = 1f;
        [ReadOnly] private Plugins.Blackboard.Blackboard _blackboard;

        private void Awake()
        {
            _blackboard = GetComponent<Plugins.Blackboard.Blackboard>();
            _blackboard.SetVariable(BlackboardKeys.BARN_POSITION, _barnPoint.position);
            _blackboard.SetVariable(BlackboardKeys.TREE_STOPPING_DISTANCE, _stoppingDistance);
            _blackboard.SetVariable(BlackboardKeys.CHARACTER_ENTITY, _character);
        }
    }
}