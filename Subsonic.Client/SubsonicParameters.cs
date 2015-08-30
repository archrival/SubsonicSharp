using Subsonic.Client.Enums;
using Subsonic.Client.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Subsonic.Client
{
    public class SubsonicParameters
    {
        public ICollection Parameters { get; private set; }
        SubsonicParameterType ParameterType { get; set; }

        SubsonicParameters() { }

        public static SubsonicParameters Create(SubsonicParameterType type = SubsonicParameterType.Single)
        {
            SubsonicParameters parameters = new SubsonicParameters {ParameterType = type};

            switch (type)
            {
                case SubsonicParameterType.List:
                    parameters.Parameters = new List<KeyValuePair<string, string>>();
                    break;
                case SubsonicParameterType.Single:
                    parameters.Parameters = new Dictionary<string, string>();
                    break;
            }

            return parameters;
        }

        public void Add(string key, object value, bool required = false)
        {
            switch (ParameterType)
            {
                case SubsonicParameterType.List:
                    {
                        string stringValue = value as string;

                        if (stringValue != null)
                            Add(key, new List<string> { stringValue });
                        else if (required)
                            throw new SubsonicErrorException(string.Format("Parameter '{0}' is required, the value provided is null", key));
                    }
                    break;
                case SubsonicParameterType.Single:
                    {
                        Dictionary<string, string> parameters = Parameters as Dictionary<string, string>;

                        if (parameters != null)
                            if (value != null)
                                parameters.Add(key, value.ToString());
                            else if (required)
                                throw new SubsonicErrorException(string.Format("Parameter '{0}' is required, the value provided is null", key));
                    }
                    break;
            }
        }

        public void Add(string key, IEnumerable<string> values, bool required = false)
        {
            List<KeyValuePair<string, string>> parameters = Parameters as List<KeyValuePair<string, string>>;

            if (parameters != null)
                if (values != null)
                    parameters.AddRange(from value in values where !string.IsNullOrWhiteSpace(value) select new KeyValuePair<string, string>(key, value));
                else if (required)
                    throw new SubsonicErrorException(string.Format("Parameter '{0}' is required, the value provided is null", key));
        }
    }
}