using UnityEngine;

namespace Nizu.InventorySystem
{
    public enum ItemEffectOptions
    {
        NO_ACTION,
        ACTION
    }


    public static class ItemEffect
    {
        public static void DoAction(ItemEffectOptions effect, GameObject target, float value)
        {
            switch (effect)
            {
                case ItemEffectOptions.NO_ACTION:
                    NoAction(target, value);
                    break;
                case ItemEffectOptions.ACTION:
                    SomeAction(target, value);
                    break;
            }
        }

        private static void NoAction(GameObject target, float value)
        {
            return;
        }
        private static void SomeAction(GameObject target, float value)
        {
            Debug.Log("action performed");
            return;
        }




    }
}