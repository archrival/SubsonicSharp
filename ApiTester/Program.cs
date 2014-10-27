using Subsonic.Client.Windows;
using System;

namespace ApiTester
{
    static class Program
    {
        static void Main(string[] args)
        {
            var test = new SubsonicClientWindows(new Uri("http://localhost:8080/madsonic/"), "user", "password", "ApiTester");
        }
    }
}
