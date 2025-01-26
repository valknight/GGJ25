using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
    public static class ChatManager
    {
        public static List<string> PositiveMessages { get; private set; } = new();
        public static List<string> NegativeMessages { get; private set; } = new();
        public static List<string> ChatterUsernames { get; private set; } = new();
        
        public static void Init()
        {
            PositiveMessages = LoadMessages("Positive");
            NegativeMessages = LoadMessages("Negative");
            ChatterUsernames = LoadMessages("Usernames");
        }
        
        private static List<string> LoadMessages(string fileName)
        {
            List<string> messages = new List<string>();

            TextAsset chatMessagesFile = Resources.Load<TextAsset>($"ChatMessages/{fileName}");

            if (chatMessagesFile == null)
            {
                Debug.LogError($"{fileName} file not found in Resources/ChatMessages");
                return messages;
            }

            using (StringReader reader = new StringReader(chatMessagesFile.text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) && line != "Message")
                    {
                        messages.Add(line.Trim());
                    }
                }
            }

            return messages;
        }

        public static string GetRandomPositiveMessage()
        {
            return PositiveMessages[Random.Range(0, PositiveMessages.Count)];
        }

        public static string GetRandomNegativeMessage()
        {
            return NegativeMessages[Random.Range(0, NegativeMessages.Count)];
        }

        public static string GetRandomUsername()
        {
            return ChatterUsernames[Random.Range(0, ChatterUsernames.Count)];
        }
    }
}