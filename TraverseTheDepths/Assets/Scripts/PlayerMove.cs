using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] Animator animator = null;
    [SerializeField] Transform body = null;
    float limitY = 60.0f;
    public float cameraSmooth = 60.0f;
    public float speed = 4f;
    public float strafeSpeed = 1.5f;

   

    float lookVertical = 0.0f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        lookVertical += Input.GetAxis("Mouse Y") * cameraSmooth * Time.deltaTime;
        lookVertical = Mathf.Clamp(lookVertical, -limitY, limitY);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookVertical, Vector3.right);

        rb.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraSmooth * Time.deltaTime);
        //lookHorizontal += Input.GetAxis("Mouse X") * cameraSmooth * Time.deltaTime;
        //lookHorizontal = Mathf.Clamp(lookHorizontal, -limitX, limitX);
        //playerCamera.transform.localRotation = Quaternion.AngleAxis(-lookHorizontal, Vector3.up);

        //body.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * cameraSmooth * Time.deltaTime, Space.Self);
       // body.transform.localRotation = Quaternion.AngleAxis(-lookVertical, body.transform.right);


        Vector3 rot = body.localEulerAngles + new Vector3(0, Input.GetAxis("Horizontal") * cameraSmooth  * Time.deltaTime, 0f);
        //rot.x = lookVertical;
        if (rot.y > 180)
        {
            rot.y -= 360;
        }
        rot.y = Mathf.Clamp(rot.y, -25, 25f);

       // Debug.Log(rot.y);

        body.localEulerAngles = rot;

        rb.velocity = Vector3.zero;

        Vector3 direction = body.transform.forward;
        direction.y = playerCamera.transform.forward.y;
        direction *= Input.GetAxis("Vertical");
        direction *= speed;
        if (Input.GetAxis("Vertical") < 0) direction /= 3;
        direction += (rb.transform.right * Input.GetAxis("Horizontal")) * strafeSpeed;

        rb.MovePosition(rb.transform.position + direction * Time.deltaTime);
        //rb.MovePosition(rb.transform.position += (
        //    ((playerCamera.transform.forward * Input.GetAxis("Vertical")) * speed
        //    + (rb.transform.right * Input.GetAxis("Horizontal")) * strafeSpeed
        //    ) * Time.deltaTime));
        animator.SetFloat("Speed", Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.Space))
        {
            rb.MovePosition(rb.transform.position += (Vector3.up) * strafeSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.MovePosition(rb.transform.position += (Vector3.down) * strafeSpeed * Time.deltaTime);
        }
    }

}