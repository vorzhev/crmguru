using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp1.Controllers
{
    public static class ExceptionsHandler
    {
        public static void CallExceptionMessage()
        {
            Console.WriteLine("An error has occured\n");
        }
        public static void CallExceptionMessage(Exception ex)
        {
            Console.WriteLine("An error has occured. Error message:\n" + ex.Message + "\n");
        }
        public static void CallExceptionMessage(string message)
        {
            Console.WriteLine("An error has occured. Note:\n" + message + "\n");
        }
        public static void CallExceptionMessage(Exception ex, string message)
        {
            Console.WriteLine("An error has occured. Error message:\n" + ex.Message + "\nNote:\n" + message + "\n");
        }
    }
}