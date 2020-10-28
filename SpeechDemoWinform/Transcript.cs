using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

using Newtonsoft.Json;

namespace SpeechDemo
{
    public class Transcript
    {
        internal async Task<StringBuilder> AzSpeechtoText(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbConsole = new StringBuilder();
            var config = SpeechConfig.FromEndpoint(
                new Uri("https://eastus2.api.cognitive.microsoft.com/sts/v1.0/issuetoken"),
                "MySuscriptionKey");

            config.EnableDictation();
            config.RequestWordLevelTimestamps();
            config.EnableAudioLogging();

            /* var autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(
                new string[] { "en-US", "es-ES", "fr-FR", "pt-BR" }); */

            var sourceLanguageConfigs = new SourceLanguageConfig[]
            {
                SourceLanguageConfig.FromLanguage("en-US"),
                SourceLanguageConfig.FromLanguage("fr-FR", "The Endpoint Id for custom model of fr-FR"),
                SourceLanguageConfig.FromLanguage("es-ES"),
                SourceLanguageConfig.FromLanguage("pt-BR", "The Endpoint Id for custom model of pt-BR")
            };

            var autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromSourceLanguageConfigs(sourceLanguageConfigs);

            using var inputConfig = AudioConfig.FromWavFileInput(filePath);
            using var speechRecognition = new SpeechRecognizer(config, autoDetectSourceLanguageConfig, inputConfig);

            var endRecognition = new TaskCompletionSource<int>();

            speechRecognition.Recognized += (s, e) =>
            {
                switch (e.Result.Reason)
                {
                    case ResultReason.NoMatch:
                        if (!endRecognition.Task.IsCompleted)
                        {
                            sbConsole.AppendLine(e.Result.Text);
                        }
                        break;
                    case ResultReason.Canceled:
                        sbConsole.AppendLine(e.Result.Text);
                        break;
                    case ResultReason.RecognizingSpeech:
                        sb.AppendLine(e.Result.Text);
                        break;
                    case ResultReason.RecognizedSpeech:
                        sb.AppendLine(e.Result.Text);
                        break;
                    case ResultReason.RecognizedIntent:
                        sbConsole.AppendLine(e.Result.Text);
                        endRecognition.TrySetResult(0);
                        break;
                    default:
                        sbConsole.AppendLine(e.Result.Text);
                        break;
                }
            };

            speechRecognition.Canceled += (s, e) =>
            {
                if (e.Reason == CancellationReason.Error)
                {
                    sbConsole.AppendLine($"ocurrió un error:{e.ErrorCode} => {e.ErrorDetails}");
                }

                endRecognition.TrySetResult(0);
            };

            speechRecognition.SessionStopped += (s, e) =>
            {
                sbConsole.AppendLine("##End Transcript##");
                endRecognition.TrySetResult(0);
            };

            await speechRecognition.StartContinuousRecognitionAsync().ConfigureAwait(false);

            Task.WaitAny(new[] { endRecognition.Task });
            await speechRecognition.StopContinuousRecognitionAsync().ConfigureAwait(false);
            sb.Append(sbConsole);
            sb.ToString();
            return sb;
        }

        internal async Task<string> AzTranslate(string text, string language)
        {
            string subscriptionKey = "mySuscriptionKey";
            string endpoint = "https://api.cognitive.microsofttranslator.com/";
            string route = $"/translate?api-version=3.0&to={language}";

            object[] body = new object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(endpoint + route);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", "eastus2");

                HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

                var responseText = await response.Content.ReadAsStringAsync();

                var resultTes = JsonConvert.DeserializeObject<IList<Translation>>(responseText);

                return resultTes.FirstOrDefault().Translations.FirstOrDefault().Text;
            }
        }
    }
}
