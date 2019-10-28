using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs498
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject model;

        WeaponShooter leftWeapon, rightWeapon;

        Transform leftHandAnchor;
        Transform rightHandAnchor;

        bool tabletMode;
        public GameObject tablet;
        public GameObject pointLaser;

        bool death;
        public Sprite deathIndicator;

        // Use this for initialization
        void Start()
        {
            leftHandAnchor = GameObject.Find("LeftHandAnchor").transform;
            rightHandAnchor = GameObject.Find("RightHandAnchor").transform;

            model = GameObject.Find("PlayerModel");
            tabletMode = false;

            death = false;

            SwitchMode(tabletMode);
        }

        // Update is called once per frame
        void Update()
        {
            if (death)
            {
                tablet_laser_follow();
            }
            else
            {
                TabletCheck();

                if (tabletMode)
                {
                    tablet_laser_follow();
                }
                else
                {
                    weapondrop();
                    weaponfollow();
                    weaponshoot();
                }
            }
        }


        public void childTrigger(Collider other)
        {
            
            if(other.gameObject.tag == "Weapon")
            {
                float leftDist = Vector3.Distance(leftHandAnchor.position, other.transform.position);
                float rightDist = Vector3.Distance(rightHandAnchor.position, other.transform.position);
                if((!leftWeapon) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch) && leftDist < 0.3)
                {
                    if((!rightWeapon) || (other.gameObject != rightWeapon.gameObject))
                    {
                        leftWeapon = other.gameObject.GetComponent<WeaponShooter>();
                    }
                }
                else if((!rightWeapon) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch) && rightDist < 0.3)
                {
                    if((!leftWeapon) || (other.gameObject != leftWeapon.gameObject))
                    {
                        rightWeapon = other.gameObject.GetComponent<WeaponShooter>();
                    }
                }
            }
        }

        void weapondrop()
        {
            if(leftWeapon && OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch))
            {
                leftWeapon = null;
            }
            if(rightWeapon && OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch))
            {
                rightWeapon = null;
            }
        }

        void weaponfollow()
        {
            if(leftWeapon)
            {
                leftWeapon.gameObject.transform.position = leftHandAnchor.position;
                leftWeapon.gameObject.transform.rotation = leftHandAnchor.rotation;
            }
            if(rightWeapon)
            {
                rightWeapon.gameObject.transform.position = rightHandAnchor.position;
                rightWeapon.gameObject.transform.rotation = rightHandAnchor.rotation;
            }
        }

        void weaponshoot()
        {
            if((Input.GetKey(KeyCode.Space) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) && leftWeapon)
            {
                leftWeapon.Shoot();
            }
            if((Input.GetKey(KeyCode.Space) || OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) && rightWeapon)
            {
                rightWeapon.Shoot();
            }
        }

        void TabletCheck()
        {
            if(Input.GetKeyDown(KeyCode.Escape) || OVRInput.GetDown(OVRInput.Button.Three, OVRInput.Controller.Touch))
            {
                tabletMode = !tabletMode;
                SwitchMode(tabletMode);
            }
        }

        void SwitchMode(bool tabletOn)
        {
            //tablet
            if (tablet) tablet.SetActive(tabletOn);
            if (pointLaser) pointLaser.SetActive(tabletOn);

            //weapons
            if (leftWeapon) leftWeapon.gameObject.SetActive(!tabletOn);
            if (rightWeapon) rightWeapon.gameObject.SetActive(!tabletOn);
        }

        void tablet_laser_follow()
        {
            if (tablet)
            {
                tablet.transform.position = leftHandAnchor.position;
                tablet.transform.rotation = leftHandAnchor.rotation;
                tablet.transform.localPosition += leftHandAnchor.localRotation * (transform.right + transform.up + transform.forward) * 0.3f;
            }
            if (pointLaser)
            {
                pointLaser.transform.position = rightHandAnchor.position;
                pointLaser.transform.rotation = rightHandAnchor.rotation;
            }
        }


        public void PlayerDeath()
        {
            //detach model
            model.GetComponent<ModelController>().IKActive = false;
            //model.SetActive(false);

            //disable movement
            GetComponent<OVRPlayerController>().enabled = false;

            //lock in tablet mode
            death = true;
            tabletMode = true;
            GameObject arrowobj = tablet.GetComponentInChildren<guideArrow>().gameObject;
            arrowobj.GetComponent<guideArrow>().enabled = false;
            arrowobj.transform.localRotation = Quaternion.identity;
            arrowobj.GetComponent<SpriteRenderer>().sprite = deathIndicator;
            SwitchMode(tabletMode);
        }


    }
}
