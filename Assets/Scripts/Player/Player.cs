using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;

    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(-.1f, 0);
    public float speed = 10;
    public float speedRun = 20;
    public float forceJump = 15;

    [Header("Animation Setup")]
    public float jumpScaleY = 1.1f;
    public float jumpScaleX = 1f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;

    private void Update()
    {
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

            myRigidbody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ? -speedRun : -speed, myRigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 scale = myRigidbody.transform.localScale;
            scale.x = 1;
            myRigidbody.transform.localScale = scale;

            myRigidbody.velocity = new Vector2(Input.GetKey(KeyCode.LeftShift) ? speedRun : speed, myRigidbody.velocity.y);
        }

        if (myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += friction;
        }
        else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * forceJump;

            Vector3 currentScale = myRigidbody.transform.localScale;
            myRigidbody.transform.localScale = new Vector3(currentScale.x, 1, 1);

            DOTween.Kill(myRigidbody.transform);

            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        float currentScaleX = myRigidbody.transform.localScale.x;

        myRigidbody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleX(jumpScaleX * Mathf.Sign(currentScaleX), animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}

