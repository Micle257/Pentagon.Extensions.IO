// -----------------------------------------------------------------------
//  <copyright file="ScorePartXmlNode.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Xml.MusicXML
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ScorePartXmlNode
    {
        [XmlAttribute(attributeName: "id")]
        public string Id { get; set; }

        [XmlElement(elementName: "part-name")]
        public string PartName { get; set; }
    }
}