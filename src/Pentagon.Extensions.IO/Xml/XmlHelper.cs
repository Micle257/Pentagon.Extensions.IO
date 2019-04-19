// -----------------------------------------------------------------------
//  <copyright file="XmlHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Xml
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
	
    public class XmlHelper
    {
        public static T Deserialize<T>(string xmlValue)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new XmlTextReader(new StringReader(xmlValue)))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public static T DeserializeFile<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = XmlReader.Create(path))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public static string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, obj);
                    return sww.ToString();
                }
            }
        }
    }
}