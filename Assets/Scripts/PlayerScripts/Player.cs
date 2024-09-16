using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayableCharacter
{ 
    REIMU, // Yukari
    MARISA, // Okina
    SUMIREKO, // Kasen
    RIN, // Hidden
}


public class Player : MonoBehaviour
{
    public static Player Instance;
    public PlayableCharacter Character;
    private InputSchema InInstance;
    private GameManager GameInstance;

    public event Action<Vector2> MoveAction = new Action<Vector2>((data) => { });
    public event Action<bool> SlowAction = new Action<bool>((data) => { });
    public event Action FireAction = new Action(() => { });
    public event Action BoomAction = new Action(() => { });

    [Header("Player Status")]
    [SerializeField] float PlayerFastSpeed;
    [SerializeField] float PlayerSlowSpeed;

    [SerializeField] bool PlayerBoomUse;
    [SerializeField] bool PlayerInvinsible;
    [SerializeField] float PlayerInvinsibleTime;
    [SerializeField] float PlayerInvinsibleCurTime;

    [SerializeField] int PlayerProjectile;
    [SerializeField] float PlayerProjectileCooltime;
    [SerializeField] float PlayerProjectileCurCooltime;

    [SerializeField] int PlayerSubProjectile;
    [SerializeField] float PlayerSubProjectileCooltime;
    [SerializeField] float PlayerSubProjectileCurCooltime;

    [Header("Player Physics")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] float InputSpeed;
    [SerializeField] Vector2 InputVector;

    [SerializeField] SpriteRenderer spr;
    //[SerializeField] float PlayerSpeed;
    //[SerializeField] float PlayerSpeed;
    //[SerializeField] float PlayerSpeed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { 
            Destroy(Instance);
        }
    }

    private void Start()
    {
        InputSpeed = PlayerFastSpeed;
        ActionInitialize();
        CharacterInit();
    }

    private void CharacterInit()
    {
        GameInstance = GameManager.Instance;

        Debug.LogWarning("Player :: Directed Access To InputControl, Under Script will be deprecated. In Future.");
        GameInstance.InputContol(InputSchemaMap.PLAYER);



    }

    private void Update()
    {
        PlayerMoveRestrict();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMoveRestrict()
    {
        Vector3 CurPos = transform.position;
        if (transform.position.x < GameInstance.LEFT_SCREEN)
        {
            CurPos.x = GameInstance.LEFT_SCREEN;
        }
        if (transform.position.x > GameInstance.RIGHT_SCREEN)
        {
            CurPos.x = GameInstance.RIGHT_SCREEN;
        }
        if (transform.position.y < GameInstance.BOTTOM_SCREEN)
        {
            CurPos.y = GameInstance.BOTTOM_SCREEN;
        }
        if (transform.position.y > GameInstance.TOP_SCREEN)
        {
            CurPos.y = GameInstance.TOP_SCREEN;
        }
        transform.position = CurPos;
    }

    private void PlayerMove()
    {
        //if (InputVector == Vector2.zero)
        //{
        //    rigid.velocity = Vector2.zero;
        //    return;
        //}




        Vector2 Force = InputVector * InputSpeed;
        rigid.velocity = Force;
    }

    #region Action Mapping
    // this function will be used in Started Selected in Game.
    public void ActionInitialize()
    {
        InInstance = GameManager.Instance.GetSchema();
        MoveAction = new Action<Vector2>((data) => Move(data));
        SlowAction = new Action<bool>((data) => Slow(data));
        FireAction = new Action(() => Fire());
        BoomAction = new Action(() => Boom());

        InInstance.MoveAction += MoveAction;
        InInstance.SlowAction += SlowAction;
        InInstance.BoomAction += BoomAction;
        InInstance.FireAction += FireAction;
    }

    #region External System Access Point
    public void BoomActionRegistry(Action _boom)
    {
        InInstance.BoomAction -= BoomAction;
        BoomAction += _boom;
        InInstance.BoomAction += BoomAction;
    }

    public void FireActionRegistry(Action _fire)
    {
        InInstance.FireAction -= FireAction;
        FireAction += _fire;
        InInstance.FireAction += FireAction;
    }
    #endregion


    public void Move(Vector2 _vector)
    {
        InputVector = _vector;
    }

    public void Fire()
    {

    }

    public void Slow(bool _press)
    {
        if (_press)
        {
            InputSpeed = PlayerSlowSpeed;
        }
        else
        {
            InputSpeed = PlayerFastSpeed;
        }
    }

    public void Boom()
    {

    }
    #endregion

}
