using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardWars
{

    public class PlayerControllerV2 : Photon.MonoBehaviour
    {

        #region variables 

        // position of mouse click that player moves to
        public Vector3 newPosition
        {
            get; set;
        }

        GameObject[] spells;
        CursorManager cursorManager;
        GameManager gameManager;
        PlayerUI playerUI;

        bool targetting = false;

        // index of spell being targeted
        int curSpell = 0;

        [Tooltip("Movement speed of the player")]
        [SerializeField]
        float speed;

        [Tooltip("Minimum range that the player will move to")]
        [SerializeField]
        float walkRange;

        [Tooltip("Rotation speed of the player")]
        [SerializeField]
        float rotationSpeed;

        [Tooltip("Model of the player")]
        [SerializeField]
        public GameObject playerModel;

        #endregion

        #region monobehaviour callbacks

        private void Awake()
        {
            spells = new GameObject[4];

            // get spell IDs from the current spell bar
            int[] spellIDs = PlayerPrefsX.GetIntArray("CurSpells");
            for (int i = 0; i < spellIDs.Length; ++i)
            {
                // instantiate spell by using Resource.Load and fetching the filename located in Constants
                spells[i] = (GameObject)Instantiate(Resources.Load(Constants.spellPrefabString[spellIDs[i]]));
            }
        }

        // Use this for initialization
        void Start()
        {
            newPosition = transform.position;

            // set game manager 
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

            // set cursor manager
            cursorManager = GameObject.Find("Cursor Manager").GetComponent<CursorManager>();

            // set player ui
            playerUI = GameObject.Find("Canvas/PlayerUI").GetComponent<PlayerUI>();
        }

        // Update is called once per frame
        void Update()
        {
            // if player is dead then return
            if (GetComponent<PlayerManager>().dead || gameManager.gameOver)
            {
                return;
            }

            // move player to clicked location
            moveChar();

            // move player when user presses RMB
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    newPosition = hit.point;
                }
            }

            // *********************** CASTING **************************

            // enter targeting state when user presses spell button
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CastHelper(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CastHelper(1);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                CastHelper(2);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                CastHelper(3);
            } 

            // go into targetting mode
            if (targetting)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                        spells[curSpell].GetComponent<SpellSystem.Spell>().Cast(gameObject, null, hit.point);

                        // make player look at target of spell
                        playerModel.transform.LookAt(hit.point);
                        playerModel.transform.rotation = new Quaternion(0, playerModel.transform.rotation.y, 0, playerModel.transform.rotation.w);

                        // play casting animation
                        GetComponent<PhotonView>().RPC("BroadcastProjectileCastAnim", PhotonTargets.All);

                        // set spell globe fill
                        playerUI.OnCast(curSpell);

                        // if player is moving, stop moving
                        newPosition = transform.position;
                    }

                    // set normal cursor sprite
                    cursorManager.activated = false;

                    // leave targetting mode
                    targetting = false;
                }
            }
        }

        #endregion

        #region public methods 

        #endregion

        #region private methods 

        private void moveChar()
        {
            // ignore y-coordinate for measuring distance
            Vector3 d1 = new Vector3(newPosition.x, 0, newPosition.z);
            Vector3 d2 = new Vector3(transform.position.x, 0, transform.position.z);
            if (Vector3.Distance(d1, d2) > walkRange)
            {
                // play run animation
                playerModel.GetComponent<Animator>().SetBool("moving", true);
                GetComponent<PhotonView>().RPC("BroadcastMoveAnim", PhotonTargets.All, true);


                transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * GetComponent<PlayerManager>().moveSpeedModifier * Time.deltaTime);

                // update player rotation
                Quaternion lookRotation = Quaternion.LookRotation(newPosition - transform.position, Vector3.up);
                lookRotation = new Quaternion(0, lookRotation.y, 0, lookRotation.w);

                // prevent unnecessary rotation
                if (Vector3.Distance(newPosition, transform.position) > 1f)
                {
                    playerModel.transform.rotation = Quaternion.Slerp(lookRotation, playerModel.transform.rotation, rotationSpeed);
                }

                // debug raycast
                Debug.DrawLine(transform.position, newPosition, Color.red);
            }
            else
            {
                playerModel.GetComponent<Animator>().SetBool("moving", false);
                GetComponent<PhotonView>().RPC("BroadcastMoveAnim", PhotonTargets.All, false);
            }
        }

        private void CastHelper(int index)
        {
            curSpell = index;
            
            if (spells[curSpell].GetComponent<SpellSystem.Spell>()._stats.behaviour.Equals("Self"))
            {
                if (spells[curSpell].GetComponent<SpellSystem.Spell>().isCastable)
                {
                    GetComponent<PhotonView>().RPC("BroadcastSelfCastAnim", PhotonTargets.All);

                    // set normal cursor sprite
                    cursorManager.activated = false;

                    // exit targetting mode
                    targetting = false;

                    // set spell globe fill
                    playerUI.OnCast(curSpell);
                }
                spells[curSpell].GetComponent<SpellSystem.Spell>().Cast(gameObject, gameObject, new Vector3(0, 0, 0));
            }
            else
            {
                if (spells[curSpell].GetComponent<SpellSystem.Spell>().isCastable)
                {
                    // set activated cursor sprite
                    cursorManager.activated = true;

                    // enter targetting mode
                    targetting = true;
                }
            }
        }

        #endregion

        #region PUN RPC

        // Play death animation
        [PunRPC]
        public void ReceivedDyingAnim()
        {
            playerModel.GetComponent<Animator>().SetTrigger("dying");
        }

        [PunRPC]
        public void BroadcastMoveAnim(bool moving)
        {
            playerModel.GetComponent<Animator>().SetBool("moving", moving);
        }

        [PunRPC]
        public void BroadcastProjectileCastAnim()
        {
            playerModel.GetComponent<Animator>().SetTrigger("projectile cast");
        }

        [PunRPC]
        public void BroadcastSelfCastAnim()
        {
            playerModel.GetComponent<Animator>().SetTrigger("self cast");
        }

        #endregion

    }
}
