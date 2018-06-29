// -----------------------------------------------------------------------
//  <copyright file="ScorePartwiseXmlNode.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Xml.MusicXML
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot(elementName: "score-partwise")]
    public class ScorePartwiseXmlNode
    {
        [XmlElement(elementName: "work")]
        public WorkXmlNode Work { get; set; }

        [XmlElement(elementName: "identification")]
        public IdentificationXmlNode Identification { get; set; }

        [XmlElement(elementName: "part-list")]
        public PartListXmlNode PartList { get; set; }

        [XmlElement(elementName: "part")]
        public PartXmlNode[] Parts { get; set; }
    }
}