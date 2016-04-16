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
        private SubsonicParameterType ParameterType { get; set; }

        private SubsonicParameters()
        {
        }

        public static SubsonicParameters Create(SubsonicParameterType type = SubsonicParameterType.Single)
        {
            var parameters = new SubsonicParameters { ParameterType = type };

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
                        var stringValue = value as string;

                        if (stringValue != null)
                            Add(key, new List<string> { stringValue });
                        else if (required)
                            throw new SubsonicErrorException($"Parameter '{key}' is required, the value provided is null");
                    }
                    break;

                case SubsonicParameterType.Single:
                    {
                        var parameters = Parameters as Dictionary<string, string>;

                        if (parameters != null)
                            if (value != null)
                                parameters.Add(key, value.ToString());
                            else if (required)
                                throw new SubsonicErrorException($"Parameter '{key}' is required, the value provided is null");
                    }
                    break;
            }
        }

        public void Add(string key, IEnumerable<string> values, bool required = false)
        {
            var parameters = Parameters as List<KeyValuePair<string, string>>;

            if (parameters == null)
                return;

            if (values != null)
                parameters.AddRange(values.Where(value => !string.IsNullOrWhiteSpace(value)).Select(value => new KeyValuePair<string, string>(key, value)));
            else if (required)
                throw new SubsonicErrorException($"Parameter '{key}' is required, the value provided is null");
        }
    }
}