using System;
using System.Collections.Generic;
using System.Text;

namespace SpeechDemo
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Translation
    {
        [JsonProperty("detectedLanguage")]
        public DetectedLanguage DetectedLanguage { get; set; }

        [JsonProperty("translations")]
        public TranslationElement[] Translations { get; set; }
    }
}
