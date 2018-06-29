// -----------------------------------------------------------------------
//  <copyright file="WorkXmlNode.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Xml.MusicXML
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class WorkXmlNode
    {
        [XmlElement(elementName: "work-title")]
        public string WorkTitle { get; set; } //TODO more
    }
}