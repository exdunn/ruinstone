using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon.Chat;
using ExitGames.Client.Photon;
using System;

namespace WizardWars
{
    public class ChatHandler : MonoBehaviour
    , IChatClientListener
    {
        public GameObject chatPanel;

        ChatClient chatClient;
        String curChannel;
        ExitGames.Client.Photon.Chat.AuthenticationValues authValues;
        bool connected;

        // Use this for initialization
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            chatClient = new ChatClient(this);
            chatClient.ChatRegion = "US";
            authValues = new ExitGames.Client.Photon.Chat.AuthenticationValues();
            authValues.UserId = PlayerPrefs.GetString("PlayerName");
            authValues.AuthType = ExitGames.Client.Photon.Chat.CustomAuthenticationType.None;

            Connect();
        }

        // Update is called once per frame
        void Update()
        {
            if (chatClient == null)
            {
                return;
            }

            chatClient.Service();
        }

        public void Connect()
        {
            chatClient.Connect(GlobalVariable.CHATAPPID, "1.0", authValues);
        }

        public void SendChatMessage(string text)
        {
            if (text == "")
            {
                return;
            }
            Debug.Log("Chat message send");
            chatClient.PublishMessage(curChannel, text);
        }

        #region Photon Chat

        public void DebugReturn(DebugLevel level, string message)
        {
            throw new NotImplementedException();
        }

        public void OnChatStateChange(ChatState state)
        {

        }

        public void OnConnected()
        {
            connected = true;


            if (PhotonNetwork.room == null)
            {
                Debug.Log("Not currently in a Photon Room");
            }
            else
            {
                chatClient.Subscribe(new string[] { PhotonNetwork.room.Name });
                curChannel = PhotonNetwork.room.Name;
                Debug.Log("Successfully connected to chat channel: " + PhotonNetwork.room.Name);
            }
        }

        public void OnDisconnected()
        {
            throw new NotImplementedException();
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            Debug.Log("Message received");
            Debug.Log("channel: " + channelName);
            Debug.Log("sender: " + senders[0]);
            Debug.Log("message: " + messages[0]);
            // display the message
            for (int i = 0; i < senders.Length; ++i)
            {
                string message = senders[i] + ": " + messages[i];
                chatPanel.GetComponent<ChatPanel>().DisplayMessage(message);
            }          
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            throw new NotImplementedException();
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            throw new NotImplementedException();
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            Debug.Log("Subsribed to a new channel!");
        }

        public void OnUnsubscribed(string[] channels)
        {
            throw new NotImplementedException();
        }

        public void OnApplicationQuit()
        {
            chatClient.Disconnect();
        }

        #endregion
    }
}