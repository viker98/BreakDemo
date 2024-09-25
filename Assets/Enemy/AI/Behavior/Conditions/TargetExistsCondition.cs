using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "TargetExists", story: "[Target] Exists [Condition]", category: "Conditions", id: "cc65d3d0d08a713a22de64b573c72d5f")]
public partial class TargetExistsCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<bool> Condition;

    public override bool IsTrue()
    {
        bool targetExists = Target.Value != null;
        return targetExists && Condition.Value;
    }
}
