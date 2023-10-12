using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.Blackboard
{
    [RequireComponent(typeof(Blackboard))]
    public class BlackboardInstaller : MonoBehaviour
    {
        [SerializeField] private Transform _barnPoint;
        [ReadOnly] private Blackboard _blackboard;

        private void Awake()
        {
            _blackboard = GetComponent<Blackboard>();
            _blackboard.SetVariable(BlackboardKeys.BARN_POSITION, _barnPoint.position);
        }
    }
}