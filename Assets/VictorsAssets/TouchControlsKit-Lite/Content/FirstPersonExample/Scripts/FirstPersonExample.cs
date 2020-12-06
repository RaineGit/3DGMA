using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class FirstPersonExample : MonoBehaviour
    {
        bool binded;
        Transform myTransform, cameraTransform;
        CharacterController controller;
        float rotation;
        bool jump, prevGrounded, isPorjectileCube;
        float weapReadyTime;
        bool weapReady = true;
        Vector2 clickStartPosition;
        Vector2 clickStartPosition2;
        public float minRotationX = -60f;
        public float maxRotationX = 60f;


        // Awake
        void Awake()
        {
            myTransform = transform;
            cameraTransform = Camera.main.transform;
            controller = GetComponent<CharacterController>();
            controller.detectCollisions = false;
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
            if( controller.isGrounded )
                jump = true;
        }

                        
        // PlayerMovement
        private void PlayerMovement( float horizontal, float vertical )
        {
            bool grounded = controller.isGrounded;
            //grounded=true;

            Vector3 moveDirection = cameraTransform.forward * vertical;
            moveDirection += myTransform.right * horizontal;

            //moveDirection.y = -10f;

            if( jump )
            {
                jump = false;
                moveDirection.y = 25f;
                isPorjectileCube = !isPorjectileCube;
            }

             moveDirection *= 7f;

            if( grounded )            
                moveDirection *= 7f;
            
            transform.Translate( moveDirection * Time.fixedDeltaTime );

            if( !prevGrounded && grounded )
                moveDirection.y = 0f;

            prevGrounded = grounded;
        }

        // PlayerRotation
        public void PlayerRotation( float horizontal, float vertical )
        {
            myTransform.Rotate( 0f, horizontal * 12f, 0f );
            rotation += vertical * 12f;
            rotation = Mathf.Clamp( rotation, minRotationX, maxRotationX );
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
            rBody.AddForce( camTransform.forward * Random.Range( 25f, 35f ) + camTransform.right * Random.Range( -2f, 2f ) + camTransform.up * Random.Range( -2f, 2f ), ForceMode.Impulse );
            Destroy( primitive, 3.5f );
        }

        // PlayerClicked
        public void PlayerClicked()
        {
            //Debug.Log( "PlayerClicked" );
        }
    }
}