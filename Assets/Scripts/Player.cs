using System.Threading;
using System.Xml.Serialization;
using UnityEngine;

public static class InputAxis
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
}

public class Player : Entity
{
    [Header("Move info")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float moveSpeed;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;

    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashCooldownTimer;

    [Header("Attack Info")]
    private bool isAttacking;
    private int comboCounter;
    private float comboTimeCounter;
    [SerializeField] private float comboTime = .3f;

    private float xInput;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Movement();
        FlipController();
        DashController();
        comboTimeCounter -= Time.deltaTime;
        CheckInput();
        AnimatorControllers();

    }


    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;

        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }

    private void Movement()
    {

        if (isAttacking)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(facingDir * dashSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }

    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw(InputAxis.Horizontal);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbillty();
        }

    }

    private void StartAttack()
    {
        if (dashTime >= 0) return;
        if (!isGrounded) return;
        if (comboTimeCounter < 0)
        {
            comboCounter = 0;
        }
        isAttacking = true;
        comboTimeCounter = comboTime;
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.linearVelocity.x != 0;

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDashing", dashTime > 0);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("comboCounter", comboCounter);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }

    }


    private void FlipController()
    {
        if (rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
            return;
        }
        if (rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
            return;
        }
    }

    private void DashController()
    {
        if (dashTime > 0)
        {
            dashTime -= Time.deltaTime;
        }
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void DashAbillty()
    {
        if (dashCooldownTimer <= 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }
}