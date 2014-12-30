using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum ErrorCode
    {
        [XmlEnum("-1")] Unspecified = -1,
        [XmlEnum("0")] GenericError = 0,
        [XmlEnum("10")] RequiredParameterMissing = 10,
        [XmlEnum("20")] IncompatibleClientVersion = 20,
        [XmlEnum("30")] IncompatibleServerVersion = 30,
        [XmlEnum("40")] WrongUsernameOrPassword = 40,
        [XmlEnum("50")] UserNotAuthorized = 50,
        [XmlEnum("60")] TrialPeriodOver = 60,
        [XmlEnum("70")] RequestedDataNotFound = 70
    }
}

