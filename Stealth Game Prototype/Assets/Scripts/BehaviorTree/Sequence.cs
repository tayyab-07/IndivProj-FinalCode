using System.Collections.Generic;

// Tutorial from: https://www.youtube.com/watch?v=aR6wt5BlE-E&ab_channel=MinaP%C3%AAcheux
// Github link for tutorial: https://github.com/MinaPecheux/UnityTutorials-BehaviourTrees
// This file was NOT written by me

// File provides basic SEQUENCE (AND Gate) functionality for use in the behaviour tree architecture

namespace BehaviorTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }

    }

}
