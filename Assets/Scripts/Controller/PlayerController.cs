using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] float bouncePower;
    [SerializeField] FixedJoystick fixedJoystick;

    public Vector2 inputVec;

    Rigidbody2D rigid;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (fixedJoystick != null)
            inputVec = new Vector2(fixedJoystick.Horizontal, fixedJoystick.Vertical);

        Move();
    }

    void OnMove(InputValue value)
    {
        if (fixedJoystick == null)
            inputVec = value.Get<Vector2>();
    }

    void Move()
    {
        rigid.linearVelocity = new Vector2(inputVec.x * moveSpeed, rigid.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckAndBounce(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        CheckAndBounce(collision);
    }

    void CheckAndBounce(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, bouncePower);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.7f)
                {
                    rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, bouncePower);
                    SoundManager.instance.PlaySFX("Bounce");
                    break;
                }
            }
        }
    }


}