using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WizardWars
{
    public class CollectionManager : MonoBehaviour
    {

        #region Public Variables

        public GameObject[] deckSlotButtons;

        #endregion

        #region Private Variables

        PlayerAccount player;

        #endregion

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Account").GetComponent<PlayerAccount>();
            Debug.Log("player: " + player);

            Invoke("UpdateButtonLabels", 0.1f);
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        #region Private Methods

        private void UpdateButtonLabels()
        {
            for (int i = 0; i < GlobalVariable.DECKCOUNT; i++)
            {
                deckSlotButtons[i].GetComponent<ButtonUI>().SetText(player.GetSpellBarList().spellBar[i].barName);
            }
        }

        #endregion
    }

}
