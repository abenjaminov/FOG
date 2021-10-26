namespace Game
{
    public static class GameState
    {
        private static bool _freezePlayerActions;

        public static void FreezePlayerActions()
        {
            _freezePlayerActions = true;
        }

        public static void UnFreezePlayerActions()
        {
            _freezePlayerActions = false;
        }

        public static bool CanPerformAction()
        {
            return _freezePlayerActions;
        }
        
        
    }
}