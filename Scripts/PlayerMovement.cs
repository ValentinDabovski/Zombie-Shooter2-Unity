using UnityEngine;
namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public float Speed = 5f;
        public float RotationSpeed = 5f;
        public GameObject MainCamera;


        private Rigidbody rb;
        private Vector3 movingVectorVertical;
        private Vector3 movingVectorHorizontal;
        private bool isInAir = false;

      

        // Use this for initialization
        void Start()
        {
            this.rb = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
          
        }

        void FixedUpdate()
        {
            MovePlayer();
            RotatePlayerWithMouse();
        }

        #region Player move controller
        private void MovePlayer()
        {
            if (Input.GetKey("w") || Input.GetKey("up"))
            {
                movingVectorVertical = transform.forward * this.Speed;
            }

            else if (Input.GetKey("s") || Input.GetKey("down"))
            {
                movingVectorVertical = -transform.forward * this.Speed;
            }


            if (Input.GetKey("a") || Input.GetKey("left"))
            {
                movingVectorHorizontal = -transform.right * this.Speed;
            }

            else if (Input.GetKey("d") || Input.GetKey("right"))
            {
                movingVectorHorizontal = transform.right * this.Speed;
            }


            if (!(Input.GetKey("up") || Input.GetKey("w")) && !(Input.GetKey("down") || Input.GetKey("s")))
            {
                movingVectorVertical = Vector3.zero;
            }

            if (!(Input.GetKey("left") || Input.GetKey("a")) && !(Input.GetKey("right") || Input.GetKey("d")))
            {
                movingVectorHorizontal = Vector3.zero;
            }

            rb.velocity = movingVectorVertical + movingVectorHorizontal;
        }

        #endregion


        #region Player mouse rotation
        private void RotatePlayerWithMouse()
        {
            rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0, Input.GetAxis("Mouse X"), 0) * this.RotationSpeed);

            this.MainCamera.transform.Rotate(Input.GetAxis("Mouse Y") * -1 * this.RotationSpeed, 0, 0);
        }

        #endregion

    }
}
