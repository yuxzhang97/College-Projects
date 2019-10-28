using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs498
{
    public class ModelController : Character
    {

        Animator playerAni;

        //Cam Follow
        float CamToModel = 0.1f;
        GameObject ovrCam;

        //IK
        public bool IKActive = false;
        public Transform HeadToCam;
        public Transform leftHandAnchor;
        public Transform rightHandAnchor;

        public Transform leftEyeAnchor;
        public Transform rightEyeAnchor;

        public GameObject playerOVR;

        Collider ovrCollider;

        protected override void die()
        {
            // GetComponent<cs498.PlayerController>().model.SetActive(false);
            // gameObject.SetActive(false);
        }

        // Use this for initialization
        void Start()
        {
            init();
            playerAni = GetComponentInChildren<Animator>();
            ovrCam = GameObject.Find("CenterEyeAnchor");
            ovrCollider = playerOVR.GetComponent<Collider>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(ovrCam.transform.position.x, ovrCam.transform.position.y-1.5f, ovrCam.transform.position.z);
            transform.rotation = Quaternion.Euler(0, ovrCam.transform.rotation.eulerAngles.y, 0);
            transform.Translate(transform.forward * -0.1f, Space.World);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            Debug.Log("ON");
            if(IKActive)
            {
                Quaternion leftAdjust = Quaternion.Euler(0, 0, 90);
                Quaternion rightAdjust = Quaternion.Euler(0, 0, -90);
                //Rotate Model along with Cam
                // transform.position = new Vector3(ovrCam.transform.position.x, ovrCam.transform.position.y-1.5f, ovrCam.transform.position.z);
                // transform.rotation = Quaternion.Euler(0, ovrCam.transform.rotation.eulerAngles.y, 0);
                // transform.Translate(transform.forward * -0.1f, Space.World);

                //Rotate (only) Model Head to look to Cam
                playerAni.SetLookAtWeight(1);
                playerAni.SetLookAtPosition(HeadToCam.forward*1f+HeadToCam.position);

                //LeftHand IK Tracking Follow
                playerAni.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                playerAni.SetIKPosition(AvatarIKGoal.LeftHand, leftHandAnchor.position);
                playerAni.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                playerAni.SetIKRotation(AvatarIKGoal.LeftHand, leftHandAnchor.rotation * leftAdjust);

                //RightHand IK Tracking Follow
                playerAni.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                playerAni.SetIKPosition(AvatarIKGoal.RightHand, rightHandAnchor.position);
                playerAni.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                playerAni.SetIKRotation(AvatarIKGoal.RightHand, rightHandAnchor.rotation * rightAdjust);

                float floor_y = ovrCollider.bounds.min.y + 0.1f;
                Vector3 leftFootPos = new Vector3(leftEyeAnchor.position.x, floor_y, leftEyeAnchor.position.z);
                Vector3 leftEyeRot = leftEyeAnchor.rotation.eulerAngles;
                Quaternion leftFootRot = Quaternion.Euler(0, leftEyeRot.y, 0);
                Vector3 rightFootPos = new Vector3(rightEyeAnchor.position.x, floor_y, rightEyeAnchor.position.z);
                Vector3 rightEyeRot = rightEyeAnchor.rotation.eulerAngles;
                Quaternion rightFootRot = Quaternion.Euler(0, rightEyeRot.y, 0);

                // LeftFoot IK Tracking Follow
                playerAni.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                playerAni.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);
                playerAni.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                playerAni.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);

                // RightFoot IK Tracking Follow
                playerAni.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                playerAni.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);
                playerAni.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                playerAni.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);

                // Lefthand Grab
                if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
                {
                    
                }
                else
                {

                }

                // Righthand Grab
                if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
                {

                }
                else
                {

                }
            }
            else
            {
                //Clear IK

                playerAni.SetLookAtWeight(0);

                playerAni.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                playerAni.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);

                playerAni.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                playerAni.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

                playerAni.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
                playerAni.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);

                playerAni.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
                playerAni.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
            }
        }

    }
}
