using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ChatEntry : MonoBehaviour
    {
        public TMP_Text usernameLabel;
        public TMP_Text messageBody;

        public void Init(string u, string m)
        {
            usernameLabel.text = u+":";
            messageBody.text = m;
        }
        
    }
}