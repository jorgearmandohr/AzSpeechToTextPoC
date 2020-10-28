namespace SpeechDemo
{

    using Newtonsoft.Json;

    public class TranslationElement
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }
}
