// -----------------------------------------------------------------------
//  <copyright file="WorkXmlNode.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Xml.MusicXML
{
    using System;
    using System.Xml.Serialization;
    using JetBrains.Annotations;

    [Serializable]
    public class WorkXmlNode
    {
        [XmlElement(elementName: "work-title")]
        [NotNull]
        public string WorkTitle { get; set; } //TODO more
    }
}