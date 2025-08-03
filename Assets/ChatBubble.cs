using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    public TextMeshProUGUI chatText;

    public void SetText(string message)
    {
        chatText.text = message;
    }
}