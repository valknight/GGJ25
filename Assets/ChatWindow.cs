using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChatWindow : MonoBehaviour
{
    public Vector2 messageGenerationDelay;
    public int maxMessages = 6;

    public Transform messageParent;
    public ChatEntry chatEntryPrefab;

   private int messageCount = 0;
    private void Start()
    {
        messageCount = 0;
        StartCoroutine(UpdateChat());
    }

    private IEnumerator UpdateChat()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(messageGenerationDelay.x, messageGenerationDelay.y));
            var newEntry = Instantiate(chatEntryPrefab, messageParent);
            var newMessage = SimManager.State.lastDelta > 0
                ? ChatManager.GetRandomPositiveMessage()
                : ChatManager.GetRandomNegativeMessage();
            var username = ChatManager.GetRandomUsername();

            messageCount++;
            if (messageCount > maxMessages)
            {
                Destroy(messageParent.GetChild(0).gameObject);
                messageCount = maxMessages;
            }
            newEntry.Init(username,newMessage);
        }
    }
}
