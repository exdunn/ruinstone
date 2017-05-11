using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class ChatPanel : MonoBehaviour
    {
        public GameObject chatWindow;
        public GameObject inputField;
        public GameObject messagePrefab;

        // number of messages
        int index;

        // Use this for initialization
        void Start()
        {
            index = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DisplayMessage(string message)
        {
            index++;
            
            GameObject newMessage = Instantiate(messagePrefab, chatWindow.transform);
            if ((index * chatWindow.GetComponent<GridLayoutGroup>().cellSize.y) > chatWindow.GetComponent<RectTransform>().rect.height)
            {
                Destroy(chatWindow.transform.GetChild(0).gameObject);
            }
            newMessage.GetComponent<Text>().text = message;
        }
    }
}
