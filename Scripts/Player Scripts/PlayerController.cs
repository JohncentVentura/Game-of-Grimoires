using System.Collections;
using System.Collections.Generic;
//using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour //Inputs & Cards
{
    [HideInInspector] public PlayerManager playerManager;
    [HideInInspector] public PlayerData playerData;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Transform centerPosition;
    [HideInInspector] public Transform frontHand;
    [HideInInspector] public Transform backHand;
    [HideInInspector] public Transform spellPosition;
    [HideInInspector] public Transform equipPosition;

    private Vector2 playerMoveInput;

    //[Header("UI")]
    //public CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerData = playerManager.playerData;

        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", playerData.GetStat(PLAYERSTATS.PlayerDirection).value);
        rb = GetComponent<Rigidbody2D>();
        centerPosition = transform.GetChild(0);
        frontHand = centerPosition.transform.GetChild(0);
        backHand = centerPosition.transform.GetChild(1);
        spellPosition = transform.GetChild(1);
        equipPosition = transform.GetChild(2);

        transform.position = playerData.worldPosition;

        //cinemachineVirtualCamera.Follow = centerPosition.transform;
    }

    /* Attack Speed
    public float animSpeed = 1;
    animator.speed = animSpeed;
    if (playerData.activeWeapon) playerData.activeWeapon.animator.speed = animSpeed;
    */

    private void Update()
    {
        if (playerData.canPlayerInput)
        {
            playerData.worldPosition = transform.position;
            playerMoveInput = new Vector2(Input.GetAxisRaw(GameManager.Instance.settingsData.movementInputKeys[0]), 
                Input.GetAxisRaw(GameManager.Instance.settingsData.movementInputKeys[1])).normalized;
        }

        if (Input.GetButtonDown(GameManager.Instance.settingsData.attackInputKey) && playerData.canPlayerInput)
        {
            playerMoveInput = Vector2.zero;
            //PlayerAttack();
        }
        else if ((Input.GetButtonDown(GameManager.Instance.settingsData.cardInputKeys[0]) 
        || Input.GetButtonDown(GameManager.Instance.settingsData.cardInputKeys[1])
        || Input.GetButtonDown(GameManager.Instance.settingsData.cardInputKeys[2]) 
        || Input.GetButtonDown(GameManager.Instance.settingsData.cardInputKeys[3])) && playerData.canPlayerInput)
        {   
            for (int i = 0; i < playerData.deckSize; i++)
            {
                if (Input.GetButtonDown(GameManager.Instance.settingsData.cardInputKeys[i]))
                {
                    playerMoveInput = Vector2.zero;
                    //PlayCard(i);
                }
            }
        }
        else if (playerMoveInput != Vector2.zero && playerData.canPlayerInput)
        {
            playerData.animState = PlayerData.AnimState.MOVE;
        }
        else if (playerMoveInput == Vector2.zero && playerData.canPlayerInput)
        {
            playerData.animState = PlayerData.AnimState.IDLE;
        }

        StateMachine(false);
    }

    private void FixedUpdate() => StateMachine(true);

    private void LateUpdate()
    {
        //Some playerController properties are being animated, we can only override those properties in LateUpdate()
        if (playerData.animState == PlayerData.AnimState.BOW_ATK || playerData.animState == PlayerData.AnimState.STAFF_ATK)
        {
            //Follow Mouse Cursor
            float maxDistance = 0.15f;
            Vector3 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDistance = Vector2.ClampMagnitude(mouseDirection - centerPosition.transform.position, maxDistance);
            equipPosition.transform.position = mouseDistance + centerPosition.transform.position;

            //Rotate to Mouse Cursor
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(centerPosition.transform.position);
            mousePos.x -= objectPos.x;
            mousePos.y -= objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            equipPosition.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    #region TODO: Transfer to PlayerManager because this functions can be called in buttons
    public void PlayCard(int i)
    {
        //playerData.canPlayerInput = false;

        /*
        if (playerData.activeDeck[i].GetStat(CARDSTATS.Mana).value >= playerData.activeDeck[i].GetStat(CARDSTATS.Mana).maxValue
            && playerData.activeDeck[i].GetStat(CARDSTATS.Cooldown).value <= 0)
        {
            if (playerData.activeDeck[i].GetProp(CARDPROPS.MainType).value == GameManager.Instance.card.cardCategoryDict[Card.CARDCATEGORIES.CREATURE])
            {       
                
                CardObject creatureObject = Instantiate(playerData.activeDeck[i]);
                creatureObject.transform.parent = transform.parent.Find("Allies");
                creatureObject.transform.position = centerPosition.position;
                playerData.activeCreatureObjects.Add(creatureObject);
                //playerData.activeDeck[i].UseCard();
            }
            else if (playerData.activeDeck[i].GetProp(CARDPROPS.MainType).value == GameManager.Instance.card.cardCategoryDict[Card.CARDCATEGORIES.SPELL])
            {
                CardObject spellObject = Instantiate(playerData.activeDeck[i]);
                spellObject.transform.parent = transform.parent.Find("Spells");
                spellObject.transform.position = frontHand.transform.position;
                playerData.activeSpellObjects.Add(spellObject);
                //playerData.activeDeck[i].UseCard();
            }
            else if (playerData.activeDeck[i].GetProp(CARDPROPS.MainType).value == GameManager.Instance.card.cardCategoryDict[Card.CARDCATEGORIES.WEAPON])
            {
                if (playerData.activeWeaponObject)
                {
                    Destroy(equipPosition.GetChild(0).gameObject);
                    playerData.activeWeaponObject = null;
                }

                playerData.activeWeaponObject = Instantiate(playerData.activeDeck[i]);
                playerData.activeWeaponObject.transform.parent = equipPosition.transform;
                playerData.activeWeaponObject.transform.position = equipPosition.transform.position;
                playerData.activeWeaponObject.animator.SetFloat("Blend", playerData.GetStat(PLAYERSTATS.PlayerDirection).value);
                playerData.activeWeaponObject.animState = CardObject.ANIMSTATES.Idle;
                //playerData.activeDeck[i].UseCard();
            }

            playerData.activeDeck[i].GetStat(CARDSTATS.Mana).value = 0;
            playerData.activeDeck[i].GetStat(CARDSTATS.Cooldown).value = playerData.activeDeck[i].GetStat(CARDSTATS.Cooldown).maxValue;
            playerData.activeDeck[i].GetStat(CARDSTATS.Duration).value = playerData.activeDeck[i].GetStat(CARDSTATS.Duration).maxValue;
        }
        else if (playerData.activeDeck[i].GetStat(CARDSTATS.Mana).value < playerData.activeDeck[i].GetStat(CARDSTATS.Mana).maxValue)
        {
            Debug.Log("Mana is not enough");
        }
        else if (playerData.activeDeck[i].GetStat(CARDSTATS.Cooldown).value > 0)
        {
            Debug.Log(playerData.activeDeck[i] + " is on cooldown");
        }
        */
    }

    public void PlayerAttack()
    {
        playerData.canPlayerInput = false;

        if (!playerData.activeWeaponObject)
        {
            playerData.canPlayerInput = true;
        }
        else if (playerData.activeWeaponObject.weaponData.GetProp(CARDPROPS.SubType).value 
            == GameManager.Instance.card.weaponTypeDict[Card.WEAPONTYPES.SWORD])
        {
            playerData.animState = PlayerData.AnimState.SWORD_ATK;
            playerData.activeWeaponObject.animState = CardObject.ANIMSTATES.Attack;
        }
        else if (playerData.activeWeaponObject.weaponData.GetProp(CARDPROPS.SubType).value 
            == GameManager.Instance.card.weaponTypeDict[Card.WEAPONTYPES.POLEARM])
        {

        }
        else if (playerData.activeWeaponObject.weaponData.GetProp(CARDPROPS.SubType).value 
            == GameManager.Instance.card.weaponTypeDict[Card.WEAPONTYPES.HEAVY])
        {

        }
        else if (playerData.activeWeaponObject.weaponData.GetProp(CARDPROPS.SubType).value 
            == GameManager.Instance.card.weaponTypeDict[Card.WEAPONTYPES.BOW])
        {
            playerData.activeWeaponObject.transform.rotation = Quaternion.Euler(0, 0, 135f); //Rotates activeWeapon to properly look at target
            playerData.animState = PlayerData.AnimState.BOW_ATK;
            playerData.activeWeaponObject.animState = CardObject.ANIMSTATES.Attack;
        }
        else if (playerData.activeWeaponObject.weaponData.GetProp(CARDPROPS.SubType).value 
            == GameManager.Instance.card.weaponTypeDict[Card.WEAPONTYPES.STAFF])
        {
            playerData.activeWeaponObject.transform.rotation = Quaternion.Euler(0, 0, 135f); //Rotates activeWeapon to properly look at target
        }
    }
    #endregion
    
    private void StateMachine(bool isUsingPhysics)
    {
        switch (playerData.animState)
        {
            case PlayerData.AnimState.IDLE:
                IdleState(isUsingPhysics);
                break;
            case PlayerData.AnimState.MOVE:
                MoveState(isUsingPhysics);
                break;
            case PlayerData.AnimState.CAST_SPELL:
                CastSpell(isUsingPhysics);
                break;
            case PlayerData.AnimState.SUMMON_CREATURE:
                SummonCreature(isUsingPhysics);
                break;
            case PlayerData.AnimState.SWORD_ATK:
                SwordAttack(isUsingPhysics);
                break;
            case PlayerData.AnimState.POLEARM_ATK:
                PolearmAttack(isUsingPhysics);
                break;
            case PlayerData.AnimState.HEAVY_ATK:
                HeavyAttack(isUsingPhysics);
                break;
            case PlayerData.AnimState.BOW_ATK:
                BowAttack(isUsingPhysics);
                break;
            case PlayerData.AnimState.STAFF_ATK:
                StaffAttack(isUsingPhysics);
                break;
        }
    }

    public void AnimEventResetState() //Called as an event in animation
    {
        playerData.animState = PlayerData.AnimState.IDLE;
        equipPosition.rotation = Quaternion.identity; //For Bow-type & Staff-type Weapons
        playerData.canPlayerInput = true;
    }

    private void IdleState(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {
            animator.Play("IdleState");
        }
    }

    private void MoveState(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = playerData.GetStat(PLAYERSTATS.MovementSpeed).value * Time.fixedDeltaTime * playerMoveInput;
        }
        else //Called in Update()
        {
            //If moving in y-axis only, playerDirection will save the last x-axis so it cannot become 0
            if (playerMoveInput.x != 0)
            {
                playerData.GetStat(PLAYERSTATS.PlayerDirection).value = playerMoveInput.x;
                animator.SetFloat("Blend", playerData.GetStat(PLAYERSTATS.PlayerDirection).value);

                if (playerData.activeWeaponObject)
                {
                    playerData.activeWeaponObject.animator.SetFloat("Blend", playerData.GetStat(PLAYERSTATS.PlayerDirection).value);
                }
            }

            animator.Play("MoveState");
        }
    }

    private void SummonCreature(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

    private void CastSpell(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

    private void SwordAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {
            animator.Play("SwordATKState");
        }
    }

    private void PolearmAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

    private void HeavyAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }

    private void BowAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {
            animator.Play("BowATKState");
        }
    }

    private void StaffAttack(bool isUsingPhysics)
    {
        if (isUsingPhysics) //Called in FixedUpdate()
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else //Called in Update()
        {

        }
    }
}