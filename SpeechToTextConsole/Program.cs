using System;
using System.Threading.Tasks;

using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Intent;

namespace SpeechToTextTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando reconocimiento ");
            Console.WriteLine("Elija 1 para reconocer desde un archivo o 3 para iniciar reconocimento activo");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    RecognizeRecordFileAsync().GetAwaiter().GetResult();
                    break;
                case "2":
                    RecognizeSpeechAndIntentAsync().GetAwaiter().GetResult();
                    break;
                case "3":
                    RecognizeSpeechAsync().GetAwaiter().GetResult();
                    break;
                default:
                    break;
            }

            Console.WriteLine("Please press Enter to continue.");
            Console.ReadLine();
        }

        private async static Task RecognizeRecordFileAsync()
        {
            var config = SpeechConfig.FromEndpoint(
                new Uri("https://eastus2.api.cognitive.microsoft.com/sts/v1.0/issuetoken"),
                "MySuscriptionKey");
            //config.SpeechRecognitionLanguage = "en-US";
            //config.EnableDictation();
            config.RequestWordLevelTimestamps();
            config.EnableAudioLogging();

            var autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(
                new string[] { "en-US", "es-MX", "fr-FR", "pt-BR" });

            //using (var recognizer = new SpeechRecognizer(
            //    config,
            //    autoDetectSourceLanguageConfig,
            //    audioConfig))
            //{
            //    var speechRecognitionResult = await recognizer.RecognizeOnceAsync();
            //    var autoDetectSourceLanguageResult =
            //        AutoDetectSourceLanguageResult.FromResult(speechRecognitionResult);
            //    var detectedLanguage = autoDetectSourceLanguageResult.Language;
            //}

            using var inputConfig = AudioConfig.FromWavFileInput(@"D:\Downloads\Llamada con Jorge y 2 más (1).wav");
            using var speechRecognition = new SpeechRecognizer(config, autoDetectSourceLanguageConfig, inputConfig);

            //var result = await speechRecognition.RecognizeOnceAsync();
            //switch (result.Reason)
            //{
            //    case ResultReason.NoMatch:
            //        Console.WriteLine($"No entendí na':{result.Text}");
            //        break;
            //    case ResultReason.Canceled:
            //        Console.WriteLine($":x:{result.Text}");
            //        break;
            //    case ResultReason.RecognizingSpeech:
            //        Console.WriteLine($"...:{result.Text}");
            //        break;
            //    case ResultReason.RecognizedSpeech:
            //        Console.WriteLine($">:{result.Text}");
            //        break;
            //    case ResultReason.RecognizedIntent:
            //        Console.WriteLine($"Detectado comando de voz:{result.Text}");
            //        Console.WriteLine($"Saliendo ....");
            //        break;
            //    default:
            //        Console.WriteLine($"LLegué aquí porque:{result.Reason}");
            //        break;
            //}

            var endRecognition = new TaskCompletionSource<int>();

            speechRecognition.Recognized += (s, e) =>
            {
                switch (e.Result.Reason)
                {
                    case ResultReason.NoMatch:
                        if (!endRecognition.Task.IsCompleted)
                        {
                            Console.WriteLine($"::{e.Result.Text}");
                        }
                        break;
                    case ResultReason.Canceled:
                        Console.WriteLine($":x:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizingSpeech:
                        Console.WriteLine($":..:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizedSpeech:
                        Console.WriteLine($">:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizedIntent:
                        Console.WriteLine($"#:{e.Result.Text}");
                        Console.WriteLine($"Saliendo ....");
                        endRecognition.TrySetResult(0);
                        break;
                    default:
                        Console.WriteLine($"*:{e.Result.Reason}");
                        break;
                }
            };

            speechRecognition.Canceled += (s, e) =>
            {
                if (e.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"ocurrió un error:{e.ErrorCode} => {e.ErrorDetails}");
                }

                endRecognition.TrySetResult(0);
            };

            speechRecognition.SessionStopped += (s, e) =>
            {
                Console.WriteLine("Deteniendo");
                endRecognition.TrySetResult(0);
            };

            await speechRecognition.StartContinuousRecognitionAsync().ConfigureAwait(false);

            Task.WaitAny(new[] { endRecognition.Task });
            await speechRecognition.StopContinuousRecognitionAsync().ConfigureAwait(false);
        }

        private async static Task RecognizeSpeechAndIntentAsync()
        {
            var config = SpeechConfig.FromEndpoint(
                new Uri("https://eastus2.api.cognitive.microsoft.com/sts/v1.0/issuetoken"),
                "MySuscriptionKey");

            config.SpeechRecognitionLanguage = "es-ES";

            using var speechRecognition = new IntentRecognizer(config);

            var luisModel = LanguageUnderstandingModel.FromAppId("ba417c40-bb51-4704-966a-f9c58afaf1c8");

            speechRecognition.AddAllIntents(luisModel);
            speechRecognition.AddIntent("chao");

            var endRecognition = new TaskCompletionSource<int>();

            speechRecognition.Recognized += (s, e) =>
            {
                switch (e.Result.Reason)
                {
                    case ResultReason.NoMatch:
                        if (!endRecognition.Task.IsCompleted)
                        {
                            Console.WriteLine($"No entendí na':{e.Result.Text}");
                        }
                        break;
                    case ResultReason.Canceled:
                        Console.WriteLine($"Se canceló la escucha:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizingSpeech:
                        Console.WriteLine($"Escuchando:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizedSpeech:
                        Console.WriteLine($"Entendí esto:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizedIntent:
                        Console.WriteLine($"Detectado comando de voz:{e.Result.Text}");
                        Console.WriteLine($"Saliendo ....");
                        endRecognition.TrySetResult(0);
                        break;
                    default:
                        Console.WriteLine($"LLegué aquí porque:{e.Result.Reason}");
                        break;
                }
            };

            speechRecognition.Canceled += (s, e) =>
            {
                if (e.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"ocurrió un error:{e.ErrorCode} => {e.ErrorDetails}");
                }

                endRecognition.TrySetResult(0);
            };

            speechRecognition.SessionStopped += (s, e) =>
            {
                Console.WriteLine("Deteniendo");
                endRecognition.TrySetResult(0);
            };

            Console.WriteLine("Ahora empieza a hablar...");
            await speechRecognition.StartContinuousRecognitionAsync().ConfigureAwait(false);

            Task.WaitAny(new[] { endRecognition.Task });
            await speechRecognition.StopContinuousRecognitionAsync().ConfigureAwait(false);
        }

        private async static Task RecognizeSpeechAsync()
        {
            var config = SpeechConfig.FromEndpoint(
                new Uri("https://eastus2.api.cognitive.microsoft.com/sts/v1.0/issuetoken"),
                "MySuscriptionKey");
            //config.SetProperty("ConversationTranscriptionInRoomAndOnline", "true");
            //config.RequestWordLevelTimestamps();
            //config.EnableAudioLogging();
            //config.SpeechSynthesisLanguage = "en-US";
            //config.SpeechRecognitionLanguage = "en-US";
            var autoDetectSourceLanguageConfig = AutoDetectSourceLanguageConfig.FromLanguages(
                new string[] { "en-US", "es-MX", "pt-BR", "fr-FR" });

            using var inputConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var speechRecognition = new SpeechRecognizer(config, autoDetectSourceLanguageConfig, inputConfig);

            var endRecognition = new TaskCompletionSource<int>();

            speechRecognition.Recognized += (s, e) =>
            {
                switch (e.Result.Reason)
                {
                    case ResultReason.NoMatch:
                        if (!endRecognition.Task.IsCompleted)
                        {
                            Console.WriteLine($"::{e.Result.Text}");
                        }
                        break;
                    case ResultReason.Canceled:
                        Console.WriteLine($":x:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizingSpeech:
                        Console.WriteLine($":..:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizedSpeech:
                        Console.WriteLine($">:{e.Result.Text}");
                        break;
                    case ResultReason.RecognizedIntent:
                        Console.WriteLine($"#:{e.Result.Text}");
                        Console.WriteLine($"Saliendo ....");
                        endRecognition.TrySetResult(0);
                        break;
                    default:
                        Console.WriteLine($"*:{e.Result.Reason}");
                        break;
                }
            };

            speechRecognition.Canceled += (s, e) =>
            {
                if (e.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"ocurrió un error:{e.ErrorCode} => {e.ErrorDetails}");
                }

                endRecognition.TrySetResult(0);
            };

            speechRecognition.SessionStopped += (s, e) =>
            {
                Console.WriteLine("Deteniendo");
                endRecognition.TrySetResult(0);
            };

            await speechRecognition.StartContinuousRecognitionAsync().ConfigureAwait(false);

            Task.WaitAny(new[] { endRecognition.Task });
            await speechRecognition.StopContinuousRecognitionAsync().ConfigureAwait(false);
        }

        //public static async Task ConversationWithPullAudioStreamAsync()
        //{
        //    // Creates an instance of a speech config with specified subscription key and service region
        //    // Replace with your own subscription key and region
        //    var config = SpeechConfig.FromEndpoint(
        //        new Uri("https://eastus2.api.cognitive.microsoft.com/sts/v1.0/issuetoken"),
        //        "MySuscriptionKey");
        //    config.SetProperty("ConversationTranscriptionInRoomAndOnline", "true");
        //    var stopTranscription = new TaskCompletionSource<int>();

        //    // Create an audio stream from a wav file or from the default microphone if you want to stream live audio from the supported devices
        //    // Replace with your own audio file name and Helper class which implements AudioConfig using PullAudioInputStreamCallback
        //    using (var audioInput = Helper.OpenWavFile(@"8channelsOfRecordedPCMAudio.wav"))
        //    {
        //        var meetingId = Guid.NewGuid().ToString();
        //        using (var conversation = await Conversation.CreateConversationAsync(config, meetingId).ConfigureAwait(false))
        //        {
        //            // Create a conversation transcriber using audio stream input
        //            using (var conversationTranscriber = new ConversationTranscriber(audioInput))
        //            {
        //                await conversationTranscriber.JoinConversationAsync(conversation);

        //                // Subscribe to events
        //                conversationTranscriber.Transcribing += (s, e) =>
        //                {
        //                    Console.WriteLine($"TRANSCRIBING: Text={e.Result.Text}");
        //                };

        //                conversationTranscriber.Transcribed += (s, e) =>
        //                {
        //                    if (e.Result.Reason == ResultReason.RecognizedSpeech)
        //                    {
        //                        Console.WriteLine($"TRANSCRIBED: Text={e.Result.Text}, UserID={e.Result.UserId}");
        //                    }
        //                    else if (e.Result.Reason == ResultReason.NoMatch)
        //                    {
        //                        Console.WriteLine($"NOMATCH: Speech could not be recognized.");
        //                    }
        //                };

        //                conversationTranscriber.Canceled += (s, e) =>
        //                {
        //                    Console.WriteLine($"CANCELED: Reason={e.Reason}");

        //                    if (e.Reason == CancellationReason.Error)
        //                    {
        //                        Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
        //                        Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
        //                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
        //                        stopTranscription.TrySetResult(0);
        //                    }
        //                };

        //                conversationTranscriber.SessionStarted += (s, e) =>
        //                {
        //                    Console.WriteLine("\nSession started event.");
        //                };

        //                conversationTranscriber.SessionStopped += (s, e) =>
        //                {
        //                    Console.WriteLine("\nSession stopped event.");
        //                    Console.WriteLine("\nStop recognition.");
        //                    stopTranscription.TrySetResult(0);
        //                };

        //                // Add participants to the conversation.
        //                // Create voice signatures using REST API described in the earlier section in this document.
        //                // Voice signature needs to be in the following format:
        //                // { "Version": <Numeric string or integer value>, "Tag": "string", "Data": "string" }

        //                //var speakerA = Participant.From("Speaker_A", "en-us", signatureA);
        //                //var speakerB = Participant.From("Speaker_B", "en-us", signatureB);
        //                //var speakerC = Participant.From("SPeaker_C", "en-us", signatureC);
        //                //await conversation.AddParticipantAsync(speakerA);
        //                //await conversation.AddParticipantAsync(speakerB);
        //                //await conversation.AddParticipantAsync(speakerC);

        //                // Starts transcribing of the conversation. Uses StopTranscribingAsync() to stop transcribing when all participants leave.
        //                await conversationTranscriber.StartTranscribingAsync().ConfigureAwait(false);

        //                // Waits for completion.
        //                // Use Task.WaitAny to keep the task rooted.
        //                Task.WaitAny(new[] { stopTranscription.Task });

        //                // Stop transcribing the conversation.
        //                await conversationTranscriber.StopTranscribingAsync().ConfigureAwait(false);
        //            }
        //        }
        //    }
        //}
    }
}
