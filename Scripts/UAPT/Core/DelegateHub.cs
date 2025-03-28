using BIS.Define;
using UnityEngine;

namespace BIS.Core
{
    public class DelegateHub { }
    public delegate void ValueChangeEvent(float currentVlaue, float maxValue, float minValue);
    public delegate void WeaponChangeEvent( EWeaponType newWeapon);
}
