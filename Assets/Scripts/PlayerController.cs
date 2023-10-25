using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float collisionOffset;
    public float moveSpeed = 1f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    [SerializeField] private Animator anim;

    public static event Action Attack = delegate { };

    private EventSystem eventSystem;

    public static bool canMove = true;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        Shoot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.beginGame || !canMove) return;

        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0f));
            }

            if (!success)
            {
                success = TryMove(new Vector2(0f, movementInput.y));
            }

            anim.SetBool("isMoving", success);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        Turn();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.instance.beginGame && !eventSystem.IsPointerOverGameObject())
        {
            Attack?.Invoke();
        }
    }


    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void Turn()
    {
        if (movementInput.x > 0)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
        else if (movementInput.x < 0)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
    }

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
