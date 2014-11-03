using Subsonic.Client.Windows;
using System;
using Subsonic.Common.Enums;

namespace ApiTester
{
    static class Program
    {
        static void Main(string[] args)
        {
            var test = new SubsonicClientWindows(new Uri("http://localhost:8080/madsonic/"), "user", "password", "ApiTester");
            var test2 = test.GetUsersAsync();
            test2.Wait();
        }
    }
}
