namespace GameConstants
{
    public static class DevMode
    {
        public static bool IsDev { get; } = false;
        public static bool IsOffShader { get; } = false;
        public static bool IsOffAStar { get; } = false;
    }

    public enum POOL_TYPE
    {
        ARROW,
        BARREL_BOOM,
        SPLASH,
        DYNAMITE,
    }

    public enum MATERIAL_TYPE
    {
        ATTACKED,
        DIE,
        APPEAR,
        NORMAL,
    }

    public enum CHARACTER_STATE
    {
        IDLE,
        CHASE,
        ATTACK,
        TEST,
        KNOCK,
        APPEAR,
        DIE,
        HIDE,
    }

    public enum ENERMY_TYPE
    {
        ARCHER,
        WARRIOR,
        TORCH,
        BARREL_BOOM,
        LASER_GUN,
        GOBLIN,
        BOSS,
    }

    public enum PLAYER_TYPE
    {
        WARRIOR,
        ARCHER,
    }

    public enum MAP
    {
        MAP_0 = 0,
        MAP_1 = 1,
    }
}
