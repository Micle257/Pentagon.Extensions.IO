// -----------------------------------------------------------------------
//  <copyright file="PartListXmlNode.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions.IO.Xml.MusicXML
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class PartListXmlNode
    {
        [XmlElement(elementName: "score-part")]
        public ScorePartXmlNode[] ScoreParts { get; set; }
    }
}