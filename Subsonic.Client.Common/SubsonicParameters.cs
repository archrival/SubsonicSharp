using Subsonic.Client.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Subsonic.Client.Common
{
    public class SubsonicParameters
    {
        public ICollection Parameters { get; set; }
        public SubsonicParameterTypes ParameterType { get; private set; }

        private SubsonicParameters() { }

        public static SubsonicParameters Create(SubsonicParameterTypes type = SubsonicParameterTypes.Single)
        {
            var parameters = new SubsonicParameters {ParameterType = type};

            switch (type)
            {
                case SubsonicParameterTypes.List:
                    parameters.Parameters = new List<KeyValuePair<string, string>>();
                    break;
                case SubsonicParameterTypes.Single:
                    parameters.Parameters = new Hashtable();
                    break;
            }

            return parameters;
        }

        public void Add(string key, object value, bool required = false)
        {
            switch (ParameterType)
            {
                case SubsonicParameterTypes.List:
                    {
                        var stringValue = value as string;

                        if (stringValue != null)
                            Add(key, new List<string> {stringValue});
                        else if (required)
                            throw new Exceptions.SubsonicErrorException(string.Format("Parameter '{0}' is required, the value provided is null", key));
                    }
                    break;
                case SubsonicParameterTypes.Single:
                    {
                        var parameters = Parameters as Hashtable;

                        if (parameters != null)
                            if (value != null)
                                parameters.Add(key, value);
                            else if (required)
                                throw new Exceptions.SubsonicErrorException(string.Format("Parameter '{0}' is required, the value provided is null", key));
                    }
                    break;
            }
        }

        public void Add(string key, IEnumerable<string> values, bool required = false)
        {
            var parameters = Parameters as List<KeyValuePair<string, string>>;

            if (parameters != null)
                if (values != null)
                    parameters.AddRange(from value in values where !string.IsNullOrWhiteSpace(value) select new KeyValuePair<string, string>(key, value));
                else if (required)
                    throw new Exceptions.SubsonicErrorException(string.Format("Parameter '{0}' is required, the value provided is null", key));
        }
    }
}
