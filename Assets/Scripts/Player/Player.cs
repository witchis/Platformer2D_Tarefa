using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public HealthBase healthBase;

    [Header("Setup")]
    public SOPlayerSetup SOPlayerSetup;

    [Header("Jump Collision Check")]
    public float distToGround;
    public float spaceToGround = .1f;
    private new Collider2D collider2D;

    private Animator _currentPlayer;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerDeath;
        }

        _currentPlayer = GetComponent<Animator>();

        if (collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

    private void OnPlayerDeath()
    {
        healthBase.OnKill -= OnPlayerDeath;
        _currentPlayer.SetTrigger(SOPlayerSetup.triggerDeath);
    }

    private void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 scale = myRigidbody.transform.localScale;
            scale.x = -1;
            myRigidbody.transform.localScale = scale;

            myRigidbody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ? -SOPlayerSetup.speedRun : -SOPlayerSetup.speed, myRigidbody.velocity.y);
            _currentPlayer.SetBool(SOPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 scale = myRigidbody.transform.localScale;
            scale.x = 1;
            myRigidbody.transform.localScale = scale;

            myRigidbody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ? SOPlayerSetup.speedRun : SOPlayerSetup.speed, myRigidbody.velocity.y);
            _currentPlayer.SetBool(SOPlayerSetup.boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(SOPlayerSetup.boolRun, false);
        }

        if (Input.GetKey(KeyCode.LeftShift)) { _currentPlayer.speed = 1.2f; }
        else { _currentPlayer.speed = 1f; }

        if (myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += SOPlayerSetup.friction;
        }
        else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= SOPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidbody.velocity = Vector2.up * SOPlayerSetup.forceJump;

            Vector3 currentScale = myRigidbody.transform.localScale;
            myRigidbody.transform.localScale = new Vector3(currentScale.x, 1, 1);

            DOTween.Kill(myRigidbody.transform);

            HandleScaleJump();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
    }

    private void HandleScaleJump()
    {
        float currentScaleX = myRigidbody.transform.localScale.x;

        myRigidbody.transform.DOScaleY(SOPlayerSetup.jumpScaleY, SOPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(SOPlayerSetup.ease);
        myRigidbody.transform.DOScaleX(SOPlayerSetup.jumpScaleX * Mathf.Sign(currentScaleX), SOPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(SOPlayerSetup.ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}

