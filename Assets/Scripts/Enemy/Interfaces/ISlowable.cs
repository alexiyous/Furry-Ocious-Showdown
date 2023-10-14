using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlowable
{
    void ApplySlow(float amount, float duration, Color color);
    void RemoveSlow();
}
