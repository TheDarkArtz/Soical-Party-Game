using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float firectionAmount;

    [SerializeField] private float velPower;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _fallGravityMultipler;

    [SerializeField] ParticleSystem dust;
    public ContactFilter2D contactFilter2D;

    [Header("Ground")]
    [SerializeField] LayerMask isGround;

    private CircleCollider2D _collider;
    private Rigidbody2D rb;

    private float oldDirection;
    private float direction;
    private bool canJump = true;

    private GameObject spawns;

    private void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<CircleCollider2D>();
        _playerInput = gameObject.GetComponent<PlayerInput>();
    }

    private void Start() {
        spawns = GameObject.FindGameObjectWithTag("Spawn");
        Spawn();
    }

    public void Spawn()
    {
        gameObject.transform.position = spawns.transform.GetChild(_playerInput.playerIndex).transform.position;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<float>();

        if(direction != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction),1,1);

        if(direction != oldDirection && direction != 0)
            dust.Play();

        oldDirection = direction;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (canJump && isGrounded() && ctx.performed)
        {
            canJump = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            dust.Play();

            StartCoroutine(ResetJump());
        }
    }

    public void OnPickup(InputAction.CallbackContext ctx)
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.right, contactFilter2D, results, 3f);
            
    }


    private void Update() {
        Vector3 currentVelocity = new Vector2(rb.velocity.x, 0f);
        if(currentVelocity.magnitude > moveSpeed)
        {
            Vector3 targetVelocity = (currentVelocity.normalized * moveSpeed);
            //rb.velocity = new Vector2(targetVelocity.x, rb.velocity.y);
        }

        if(direction == 0)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(firectionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        if (rb.velocity.y < 0)
            rb.gravityScale = _gravityScale * _fallGravityMultipler;
        else
            rb.gravityScale = _gravityScale;
    }

    private void FixedUpdate() {
        //Vector2 velocity = new Vector2(direction * moveSpeed, 0);

        float targetSpeed = direction * moveSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);

        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, 0.2f, isGround);
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
}