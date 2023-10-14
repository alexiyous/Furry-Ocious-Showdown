using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool isAggroed { get; set; }

    bool isWithinAttackingRadius { get; set; }
    void SetAggroStatus(bool isAggroed);
    void SetAttackingDistanceBool(bool isWithinAttackingRadius);
}
