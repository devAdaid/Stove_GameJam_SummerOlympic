public static class Constant
{
    public static readonly int STAMINA_DEFAULT = 300;
    public static readonly int STAMINA_MAX = 500;
    public static readonly int SWIMSTAT_MAX = 1500;
    public static readonly int INITIAL_MONTH = 8;
    public static readonly int GOLD_DELIVERED_AT_MONTH = 50000;
    public static readonly int STAMINA_DELIVERED_AT_MONTH = 0;
    public static readonly int DAY_PER_WEEK_COUNT = 7;
    public static readonly int WEEK_PER_MONTH_COUNT = 4;
    public static readonly int MONTH_COUNT = 12;
    public static int DAY_PER_MONTH_COUNT => DAY_PER_WEEK_COUNT * WEEK_PER_MONTH_COUNT;
    public static int DAY_TOTAL_COUNT => DAY_PER_MONTH_COUNT * MONTH_COUNT;
}