using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    private Vector3 direction;
    private float rotationAngle;
    public float speed;
    private bool previousIsRunning;
    private bool isRunning;
    private Animator animator;

    private const string IsWalkingAnim = "IsWalking";
    private const string IsRunningAnim = "IsRunning";
    private const string IsSittingAnim = "IsSitting";
            RaycastHit hit;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();

        if (direction != Vector3.zero)
        {
            Rotate();
            Move();
        }
        else
        {
            isRunning = false;
            animator.SetBool(IsRunningAnim, false);

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                animator.SetBool(IsSittingAnim, true);
            }
        }

        hit = GetRaycast();
    }

    public RaycastHit GetRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            return hit;

        return hit; 
    }

    private void HandleInput()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (direction != Vector3.zero)
        {
            direction = Camera.main.transform.TransformDirection(direction);
            direction.y = 0;
            direction.Normalize();

            rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            animator.SetBool(IsSittingAnim, false);
        }
    }

    private void ChangeState()
    {
        animator.SetBool(isRunning ? IsRunningAnim : IsWalkingAnim, true);
        animator.SetBool(!isRunning ? IsRunningAnim : IsWalkingAnim, false);
    }

    private void Move()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        ChangeState();

        float targetSpeed = isRunning ? 5.5f : 1.5f;

        speed = Mathf.MoveTowards(speed, targetSpeed, Time.deltaTime * 10f);

        RaycastHit hit;
        //Vector3 moveDirection = direction;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            direction = Vector3.ProjectOnPlane(direction, hit.normal).normalized;
        }

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        previousIsRunning = isRunning;
    }

    private void Rotate()
    {
        if (direction != Vector3.zero)
        {
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            //{
                //Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, hit.normal).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction, hit.normal);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, hit.normal), Time.deltaTime * 15f);
            //}
        }
    }

    private void Jump()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f);
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}