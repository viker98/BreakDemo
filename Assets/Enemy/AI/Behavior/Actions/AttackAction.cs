using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Self] attack [Target]", category: "Action", id: "c5edf81cbd13403be8fc133802c6448b")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        IBehaviorInterface selfInterface = Self.Value.GetComponent<IBehaviorInterface>();
        if (selfInterface != null)
        {
            selfInterface.Attack(Target.Value);
        }
        return Status.Success;
    }

}

