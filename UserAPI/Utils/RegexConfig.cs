public static class RegexConfig
{
    public static string Email => "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
    public static string Password => "^(?=.*[A-Z])(?=.*\\d).{8,}$";
}