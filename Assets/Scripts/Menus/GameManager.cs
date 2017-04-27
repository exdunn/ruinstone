using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region Public Variables

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    /// <summary>
    /// Game menu which can be opened by pressing ESC
    /// </summary>
    public GameObject gameMenu;

    #endregion

    #region Private Variables

    #endregion

    #region Public Methods

    /// <summary>
    /// Takes player back to main menu
    /// </summary>
    public void LeaveClick()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Close the in game menu
    /// </summary>
    public void CancelClick()
    {
        gameMenu.SetActive(false);
    }

    #endregion

    #region Private Methods

    // Use this for initialization
    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            //Debug.Log("We are Instantiating LocalPlayer");
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameMenu.activeSelf)
            {
                gameMenu.SetActive(true);
            }
            else
            {
                gameMenu.SetActive(false);
            }
        }
    }

    #endregion

}
