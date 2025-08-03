using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ChatManager : MonoBehaviourPun
{
    public TMP_InputField chatInput;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrWhiteSpace(chatInput.text))
        {
            string message = chatInput.text;
            chatInput.text = "";
            photonView.RPC("BroadcastMessage", RpcTarget.All, PhotonNetwork.NickName, message);
        }
    }

    [PunRPC]
    void BroadcastMessage(string sender, string message)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.TryGetComponent(out ChatBubble bubble))
            {
                bubble.ShowMessage($"{sender}: {message}");
            }
        }
    }
}

