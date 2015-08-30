namespace Subsonic.Client
{
    public interface ISubsonicAuthentication
    {
        void SetPassword(string password);
        void SetSaltComplexity(int complexity);
        SubsonicToken GetToken();
    }
}

