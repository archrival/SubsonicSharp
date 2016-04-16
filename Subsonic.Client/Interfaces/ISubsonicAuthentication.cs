namespace Subsonic.Client.Interfaces
{
    public interface ISubsonicAuthentication
    {
        void SetPassword(string password);

        void SetSaltComplexity(int complexity);

        SubsonicToken GetToken();
    }
}