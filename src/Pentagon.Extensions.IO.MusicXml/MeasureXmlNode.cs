// -----------------------------------------------------------------------
//  <copyright file="MeasureXmlNode.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Xml.MusicXML
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class MeasureXmlNode
    {
        [XmlAttribute(attributeName: "number")]
        public int Number { get; set; }

        [XmlElement(elementName: "note")]
        public NoteXmlNode[] Notes { get; set; }
    }
}