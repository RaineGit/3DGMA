using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class FirstPersonExample2 : MonoBehaviour
    {
        bool binded;
        Transform myTransform;
        public Transform cameraTransform;
        CharacterController controller;
        float rotation;
        bool jump, prevGrounded, isPorjectileCube;
        float weapReadyTime;
        bool weapReady = true;
        Vector2 clickStartPosition;
        Vector2 clickStartPosition2;

        public class MovementSettings
        {
            public float ForwardSpeed = 8.0f;   // Speed when walking forward
            public float BackwardSpeed = 4.0f;  // Speed when walking backwards
            public float StrafeSpeed = 4.0f;    // Speed when walking sideways
            public float RunMultiplier = 2.0f;   // Speed when sprinting
            public KeyCode RunKey = KeyCode.LeftShift;
            public float JumpForce = 30f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 8f;

#if !MOBILE_INPUT
            private bool m_Running;
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
                if (input == Vector2.zero) return;
                if (input.x > 0 || input.x < 0)
                {
                    //strafe
                    CurrentTargetSpeed = StrafeSpeed;
                }
                if (input.y < 0)
                {
                    //backwards
                    CurrentTargetSpeed = BackwardSpeed;
                }
                if (input.y > 0)
                {
                    //forwards
                    //handled last as if strafing and moving forward at the same time forwards speed should take precedence
                    CurrentTargetSpeed = ForwardSpeed;
                }
#if !MOBILE_INPUT
                if (Input.GetKey(RunKey))
                {
                    CurrentTargetSpeed *= RunMultiplier;
                    m_Running = true;
                }
                else
                {
                    m_Running = false;
                }
#endif
            }

#if !MOBILE_INPUT
            public bool Running
            {
                get { return m_Running; }
            }
#endif
        }

        public MovementSettings movementSettings = new MovementSettings();
        private Vector3 m_GroundContactNormal;


        // Awake
        void Awake()
        {
            myTransform = transform;
            //cameraTransform = Camera.main.transform;
            //controller = GetComponent<CharacterController>();
            //controller.detectCollisions = true;
        }

        // Update
        void Update()
        {

            if(Input.GetMouseButtonDown(0)){
                clickStartPosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            }

            if(Input.GetMouseButtonDown(1)){
                clickStartPosition2 = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            }
            
            if( weapReady == false )
            {
                weapReadyTime += Time.deltaTime;
                if( weapReadyTime > .15f )
                {
                    weapReady = true;
                    weapReadyTime = 0f;
                }
            }


            if( TCKInput.GetAction( "jumpBtn", EActionEvent.Down ) )
            {
                Jumping();
            }

            if( TCKInput.GetAction( "fireBtn", EActionEvent.Press ) )
            {
                PlayerFiring();
            }

            if(((Input.GetAxis("Mouse X")!=0 || Input.GetAxis("Mouse Y")!=0) && ((Input.GetMouseButton(0) && clickStartPosition.x > Screen.width/2) || (Input.GetMouseButton(1) && clickStartPosition2.x > Screen.width/2))) || !Application.isEditor){
                Vector2 look = TCKInput.GetAxis( "Touchpad" );
                PlayerRotation( look.x, look.y );
            }
        }

        // FixedUpdate
        void FixedUpdate()
        {
            if((((Input.GetMouseButton(0) && clickStartPosition.x < Screen.width/2) || (Input.GetMouseButton(1) && clickStartPosition2.x < Screen.width/2))) || !Application.isEditor){
                /*float moveX = TCKInput.GetAxis( "Joystick", EAxisType.Horizontal );
                float moveY = TCKInput.GetAxis( "Joystick", EAxisType.Vertical );*/
                Vector2 move = TCKInput.GetAxis( "Joystick" ); // NEW func since ver 1.5.5
                PlayerMovement( move.x, move.y );
            }
        }


        // Jumping
        private void Jumping()
        {
            if( true )
                jump = true;
        }

         private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }
     
        // PlayerMovement
        private void PlayerMovement( float horizontal, float vertical )
        {
            bool grounded = true;
            //grounded=true;

            Vector3 moveDirection = new Vector3(0,0,0);
            moveDirection = (transform.forward*vertical + transform.right*horizontal)/5;

            
            //moveDirection += myTransform.right * horizontal;

            //moveDirection.y = -10f;

            if( jump )
            {
                jump = false;
                moveDirection.y = 25f;
                isPorjectileCube = !isPorjectileCube;
            }

            moveDirection *= 3f;

            if( grounded )            
                moveDirection *= 3f;
            


            if (gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude <
                    (movementSettings.CurrentTargetSpeed*movementSettings.CurrentTargetSpeed))
                {
                    gameObject.GetComponent<Rigidbody>().AddForce(moveDirection, ForceMode.Impulse);
                }
            

            if( !prevGrounded && grounded )
                moveDirection.y = 0f;

            prevGrounded = grounded;
        }

        // PlayerRotation
        public void PlayerRotation( float horizontal, float vertical )
        {
            myTransform.Rotate( 0f, horizontal * 12f, 0f );
            rotation += vertical * 12f;
            rotation = Mathf.Clamp( rotation, -60f, 60f );
            cameraTransform.localEulerAngles = new Vector3( -rotation, cameraTransform.localEulerAngles.y, 0f );
        }
        
        // PlayerFiring
        public void PlayerFiring()
        {
            if( !weapReady )
                return;

            weapReady = false;

            GameObject primitive = GameObject.CreatePrimitive( isPorjectileCube ? PrimitiveType.Cube : PrimitiveType.Sphere );
            primitive.transform.position = ( myTransform.position + myTransform.right );
            primitive.transform.localScale = Vector3.one * .2f;
            Rigidbody rBody = primitive.AddComponent<Rigidbody>();
            Transform camTransform = Camera.main.transform;
            rBody.AddForce( myTransform.forward * Random.Range( 25f, 35f ) + myTransform.right * Random.Range( -2f, 2f ) + myTransform.up * Random.Range( -2f, 2f ), ForceMode.Impulse );
            Destroy( primitive, 3.5f );
        }

        // PlayerClicked
        public void PlayerClicked()
        {
            //Debug.Log( "PlayerClicked" );
        }
    }
}