using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class ChatWindow : MonoBehaviour
{
    public float messageGenerationDelay;

    public Transform messageParent;
    public ChatEntry chatEntryPrefab;
    private void Start()
    {
        StartCoroutine(UpdateChat());
    }

    private IEnumerator UpdateChat()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(messageGenerationDelay);
            var newEntry = Instantiate(chatEntryPrefab, messageParent);
            var newMessage = SimManager.State.lastDelta > 0
                ? ChatManager.GetRandomPositiveMessage()
                : ChatManager.GetRandomNegativeMessage();
            var username = ChatManager.GetRandomUsername();
            
            newEntry.Init(username,newMessage);
        }
    }
}
