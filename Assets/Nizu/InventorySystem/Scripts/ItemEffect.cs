namespace Nizu.InventorySystem
{
    public enum ItemEffectOptions
    {
        NO_ACTION,
        HEAL_PLAYER
    }


    public static class ItemEffect
    {
        public static void DoAction(ItemEffectOptions effect)
        {
            switch (effect)
            {
                case ItemEffectOptions.NO_ACTION:
                    NoAction();
                    break;
                case ItemEffectOptions.HEAL_PLAYER:
                    HealPlayer();
                    break;
            }
        }

        private static void NoAction()
        {
            return;
        }
        private static void HealPlayer()
        {
            PlayerMovement.Instance.Heal(10);
        }




    }
}