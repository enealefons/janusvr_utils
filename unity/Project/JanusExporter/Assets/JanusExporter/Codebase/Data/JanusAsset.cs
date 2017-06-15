﻿#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JanusVR
{
    [Serializable]
    public class JanusAsset
    {
        [XmlAttribute("src")]
        public string src { get; set; }

        [XmlAttribute("id")]
        public string id { get; set; }

        public override string ToString()
        {
            return string.Format("src: {0}, id: {1}", src, id);
        }
    }
}
#endif