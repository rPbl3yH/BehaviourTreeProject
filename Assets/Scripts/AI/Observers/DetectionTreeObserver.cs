using AI.Blackboard;
using AI.Sensors;
using Plugins.Blackboard;
using Sample;
using UnityEngine;
using Tree = Sample.Tree;

namespace AI.Observers
{
    public class DetectionTreeObserver : MonoBehaviour
    {
        [SerializeField] private DetectionTreeSensor _detectionTreeSensor;
        [SerializeField] private Plugins.Blackboard.Blackboard _blackboard;

        private void OnEnable()
        {
            _detectionTreeSensor.ClosetTreeFound += OnClosetTreeFound;
        }

        private void OnClosetTreeFound(Tree closetTree)
        {
            if (closetTree == null)
            {
                _blackboard.RemoveVariable(BlackboardKeys.TREE);
            }
            else
            {
                _blackboard.SetVariable(BlackboardKeys.TREE, closetTree);
            }
        }
    }
}