using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PMoveController : MonoBehaviour
{
    //Run
    public float movementSpeed;
    public float boostCoefficient;
    //Jump
    public float gConstant = 10;
    public float fallDownCoefficient;
    public float jumpHeigth;
    public float jumpTimeCoefficient;  
    public float jumpForce;
    public float jumpChargeTime;
    public float jumpDecreaseCoefficent;
    public bool isJumpChargable = false;

    public float distanceToGround;
    //Mouse
    public bool invertMouse;
    public float mouseSensitivity;



    public KeyCode jumpButton;
    //public KeyCode WASD;
    public KeyCode dashButton;
    public KeyCode shootButton;

    public UnityEvent jump;
    public UnityEvent move;
    public UnityEvent shoot;
    public UnityEvent dash;


    public GameObject cameraObject;
    public GameObject feet;


    private Vector3 velocity;
    private Vector3 jumpVelocity;

    float _jumpForce;
    float jumpLength;
    float _gconst;
    float jumpBufferTime;
    bool isGrounded = false;
    private Rigidbody rb;
    
    void Awake()
    {
        move.AddListener(MoveFixed);
        jump.AddListener(Jump);
        dash.AddListener(InstantSpeedUp);
        rb = gameObject.GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
        if (!GroundCheck()) _gconst = gConstant * fallDownCoefficient;
        else _gconst = gConstant;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Input.ResetInputAxes();
        jumpVelocity = new Vector3(0, 0, 0);
    }
    
    
    // Update is called once per frame
    void Update()
    {

        if(isJumpChargable)ChargeJump();
        CameraRotate();
        if (Input.GetKeyUp(dashButton) && dash != null) InstantSlowDown();
        if (Input.GetKeyDown(dashButton) && dash != null) dash.Invoke();


        if (isJumpChargable) { if (Input.GetKeyDown(jumpButton) && jump != null) isJumpChargeBegan = true; }
        if (isJumpChargable) { if (Input.GetKeyUp(jumpButton) && jump != null) jump.Invoke(); }

        if(isJumpChargable == false )if (Input.GetKeyDown(jumpButton) && jump != null) jump.Invoke();
        if (isJumpChargable == false) if (Input.GetKeyUp(jumpButton) && jump != null) jumpLength = 0 ;

        if (Input.GetKeyDown(shootButton) && shoot != null) shoot.Invoke();
        isGrounded = GroundCheck();
        rb.velocity = velocity;
    }

    private void FixedUpdate()
    {
        if (move != null) move.Invoke();
    }
     
    void Move()
    {

    velocity.x = ((Vector3.up*jumpLength+gameObject.transform.forward*Input.GetAxis("Vertical") + gameObject.transform.right*Input.GetAxis("Horizontal"))*movementSpeed*Time.deltaTime+Vector3.down*gConstant*Time.deltaTime).x;
    velocity.z = ((Vector3.up*jumpLength+gameObject.transform.forward*Input.GetAxis("Vertical") + gameObject.transform.right*Input.GetAxis("Horizontal"))*movementSpeed*Time.deltaTime+Vector3.down*gConstant*Time.deltaTime).z;
        velocity.y += jumpLength;
        if (jumpLength > 0) jumpLength = jumpLength - jumpLength * Time.deltaTime * 100f;
        else jumpLength = 0;
    }

    void MoveFixed()
    {

      // velocity =(gameObject.transform.forward * Input.GetAxis("Vertical") + gameObject.transform.right * Input.GetAxis("Horizontal")) * movementSpeed + Vector3.down*gConstant;
        velocity.x = ((Vector3.up * jumpLength + gameObject.transform.forward * Input.GetAxis("Vertical") + gameObject.transform.right * Input.GetAxis("Horizontal")) * movementSpeed + Vector3.down * _gconst ).x;
        velocity.z = ((Vector3.up * jumpLength + gameObject.transform.forward * Input.GetAxis("Vertical") + gameObject.transform.right * Input.GetAxis("Horizontal")) * movementSpeed + Vector3.down * _gconst ).z;
        velocity.y = _jumpForce + (Vector3.down * _gconst).y;

        if (jumpLength > 0)
        {
            _jumpForce = jumpForce;
            jumpLength = jumpLength + (Vector3.down * gConstant * jumpDecreaseCoefficent).y;
            _gconst = gConstant;
        }
        else
        {
            _jumpForce = 0;
            if (!GroundCheck()) _gconst = gConstant * fallDownCoefficient;
            else _gconst = gConstant / fallDownCoefficient;
            jumpLength = 0; }


    }




    void CameraRotation()
    {
        var deltaMouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        Vector2 deltaRotation = deltaMouse * mouseSensitivity;
        deltaRotation.y *= invertMouse ? 1.0f : -1.0f;

        float pitchAngle = cameraObject.transform.localEulerAngles.x;

        // turns 270 deg into -90, etc
        if (pitchAngle > 180)
            pitchAngle -= 360;

        pitchAngle = Mathf.Clamp(pitchAngle + deltaRotation.y, -90.0f, 90.0f);

        transform.Rotate(Vector3.up, deltaRotation.x);
        cameraObject.transform.localRotation = Quaternion.Euler(pitchAngle, 0.0f, 0.0f);
    }

    void CameraRotate()
    {
        CameraRotation();
    }
    bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,distanceToGround+ 0.1f) && hit.transform.tag == "Ground")
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    bool isJumpChargeBegan;
    void ChargeJump()
    {
        if(isJumpChargeBegan)
        if (jumpChargeTime > jumpBufferTime)
        {
            jumpBufferTime += Time.deltaTime;
        }
        else
        {
            jumpBufferTime = jumpChargeTime;
                isJumpChargeBegan = false;
        }

        
    }
    void Jump()
    {

        if (GroundCheck())
        {
            if (isJumpChargable) jumpLength = (jumpHeigth / jumpTimeCoefficient) * (jumpBufferTime / jumpChargeTime);
            else jumpLength = jumpHeigth / jumpTimeCoefficient;
        }
        jumpBufferTime = 0;
    }
    void LinearJump()
    {
        
    }
    void InstantSpeedUp()
    {
        movementSpeed += boostCoefficient;
        
    }
    void InstantSlowDown()
    {
        movementSpeed -= boostCoefficient;
    }
    float _currStep = 0;
    float LinearSpeedUp(float _maxVal, float _step)
    {if (_currStep! > _maxVal) _currStep += _step;
        return _currStep;
    }
    
}
