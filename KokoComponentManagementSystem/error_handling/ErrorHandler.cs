namespace KokoComponentManagementSystem.error_handling;
public static class ErrorHandler {
    public static Action<string> _handleError = Console.WriteLine;
    public static Action<string> _handleWarning = Console.WriteLine;
}
