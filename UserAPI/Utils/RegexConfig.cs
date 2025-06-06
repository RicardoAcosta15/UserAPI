public static class RegexConfig
{
    public static string Email => "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";  // Valida formato de email
    public static string Password => "^(?=.*[A-Z])(?=.*\\d).{8,}$";  // Mínimo 8 caracteres, una mayúscula y un número
}