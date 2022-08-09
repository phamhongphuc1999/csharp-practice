using MyNumber.Services;

namespace ConsoleText
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string check = UIntService.Divide("10", "2");
            Console.WriteLine(check);
            //Console.WriteLine(UIntService.Add("1", "2"));
        }
    }
}