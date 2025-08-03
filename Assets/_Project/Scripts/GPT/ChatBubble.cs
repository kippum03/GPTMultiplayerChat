using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    public TextMeshProUGUI chatText;

    public void ShowMessage(string message, float duration = 5f)
    {
        chatText.text = message;
        gameObject.SetActive(true);
        StopAllCoroutines(); // Cancel any existing hide timers
        StartCoroutine(HideAfterSeconds(duration));
    }

    private IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        chatText.text = "";
        gameObject.SetActive(false);
    }
}
