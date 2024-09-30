using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class BoxTriggerDamageComponent : DamageComponent
{
    [SerializeField] float damage = 10;

    HashSet<GameObject> _currentOverlappingTargets;
    public override void DoDamage()
    {
        foreach(GameObject target in _currentOverlappingTargets)
        {
            ApplyDamage(target, damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ShouldDamage(other.gameObject))
        {
            _currentOverlappingTargets.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _currentOverlappingTargets.Remove(other.gameObject);
    }
}
