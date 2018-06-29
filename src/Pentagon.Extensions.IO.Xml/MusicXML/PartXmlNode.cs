// -----------------------------------------------------------------------
//  <copyright file="PartXmlNode.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Xml.MusicXML
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class PartXmlNode
    {
        [XmlAttribute(attributeName: "id")]
        public string Id { get; set; }

        [XmlElement(elementName: "measure")]
        public MeasureXmlNode[] Measures { get; set; }
    }
}