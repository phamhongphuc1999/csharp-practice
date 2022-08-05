using MyNumber.Services;

namespace ConsoleText
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            bool check = UIntService.IsNumber("123abc");
            Console.WriteLine(check);
        }
    }
}