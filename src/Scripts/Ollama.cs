using UnityEngine;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.AI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HardCodeDev.SimpleOllamaUnity
{
    /// <summary>
    /// Structure with Ollama settings
    /// </summary>
    [System.Serializable]
    public struct OllamaConfig
    {
        public string modelName, uri, systemPrompt;

        public OllamaConfig(string modelName, string systemPrompt, string uri = "http://localhost:11434")
        {
            this.modelName = modelName;
            this.uri = uri;
            this.systemPrompt = systemPrompt;
        }
    }

    /// <summary>
    /// Structure with request settings
    /// </summary>
    [System.Serializable]
    public struct OllamaRequest
    {
        public string userPrompt;
        public bool clearThinking;

        public OllamaRequest(string userPrompt, bool clearThinking = false)
        {
            this.userPrompt = userPrompt;
            this.clearThinking = clearThinking;
        }
    }

    /// <summary>
    /// Class for communicating with Ollama
    /// </summary>
    public class Ollama : MonoBehaviour
    {
        private List<ChatMessage> _chatHistory = new();
        private IChatClient _chatClient;

        /// <summary>
        /// Constructor to initialize Ollama
        /// </summary>
        /// <param name="config">Ollama settings</param>
        public Ollama(OllamaConfig config) => InitOllama(config);

        private void InitOllama(OllamaConfig config)
        {
            var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
            { DisableDefaults = true });

            builder.Services.AddChatClient(new OllamaChatClient(new System.Uri(config.uri), config.modelName));

            _chatClient = builder.Build().Services.GetRequiredService<IChatClient>();

            _chatHistory.Add(new(ChatRole.System, config.systemPrompt));
        }

        /// <summary>
        /// Method to send and get messages from LLM via Ollama
        /// </summary>
        /// <param name="request">Request settings</param>
        /// <returns>LLM's response</returns>
        public async Task<string> SendMessage(OllamaRequest request)
        {
            _chatHistory.Add(new(ChatRole.User, request.userPrompt));

            var chatResponse = "";
            await foreach (var item in _chatClient.GetStreamingResponseAsync(_chatHistory)) chatResponse += item.Text;

            _chatHistory.Add(new(ChatRole.Assistant, chatResponse));

            if (request.clearThinking == true)
            {
                var newChatResponse = ClearThinking(chatResponse);
                return newChatResponse;
            }
            else return chatResponse;
        }

        private string ClearThinking(string input)
        {
            try
            {
                var start = input.IndexOf("<think>");
                var end = input.IndexOf("</think>");
                return input.Remove(start, (end + "</think>".Length) - start);
            }
            catch
            {
                Debug.LogError("No <think> were found!");
                return "";
            }
        }
    }
}