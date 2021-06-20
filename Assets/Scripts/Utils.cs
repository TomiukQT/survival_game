public static class Utils
{

    
    public static int Fib(int n)
    {
        return n == 1 ? 1 : n == 2 ? 1 : Fib(n - 1) + Fib(n - 2);
    }
    
}