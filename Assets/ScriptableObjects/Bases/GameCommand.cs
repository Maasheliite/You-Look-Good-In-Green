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
        public bool DecreaseValueUntilZero(float val)
        {
            if (variableToModify.Value - val < 0)
            {
                return false;
            }
            else
            {
                variableToModify.Value -= val;
                return true;
            }

        }
        public void MultiplyValue(float val)
        {
            variableToModify.Value *= val;
        }
        public void DivideValue(float val)
        {
            if (val != 0)
            {
                variableToModify.Value /= val;
            }
            else
            {
                Debug.Log("Attempted to divide'" + variableToModify.name + "' by zero at" + this);
            }
        }

    }
}
