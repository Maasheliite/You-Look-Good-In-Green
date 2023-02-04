using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nizu.Util.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Utility/ScriptableObjects/GameCommand")]
    public class GameCommand : ScriptableObject
    {
        public FloatVariable variableToModify;


        public void IncreaseValue(float val)
        {
            variableToModify.Value += val;
        }
        public void DecreaseValue(float val)
        {
            variableToModify.Value -= val;
        }
        public void MultiplyValue(float val)
        {
            variableToModify.Value *= val;
        }
        public void DivideValue(float val)
        {
            if (val != 0)
                variableToModify.Value /= val;
            else Debug.Log("Attempted to divide'" + variableToModify.name + "' by zero at" + this);
        }

    }
}
