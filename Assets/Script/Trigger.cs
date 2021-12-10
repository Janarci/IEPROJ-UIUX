using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //OnCollision col;
    [SerializeField] private float min = -3;
    [SerializeField] private float max = 3;
    [SerializeField] private float jumpHeight = 2;
    private float ticks = 0.0f;
    [SerializeField] private float JUMP_INTERVAL = 0.15f;
    private bool isInAir;

    [SerializeField] private List<Transform> planes; 
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animation Player;

    // Start is called before the first frame update
    void Start()
    {
        //JUMP_INTERVAL = playerAnimator.GetCurrentAnimatorStateInfo(2).length;
        isInAir = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && GetComponent<OnCollision>().GetHPPoints == 3)
            {
                //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    playerAnimator.SetBool("Left", true);

                if (transform.position.z > planes[0].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[0].position.z);
                    //transform.position = transform.position + new Vector3(0, 0, min);
                }
            }
            else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && (GetComponent<OnCollision>().GetHPPoints == 3 || GetComponent<OnCollision>().GetHPPoints == 2))
            {
                //if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    playerAnimator.SetBool("Right", true);

                if (transform.position.z < planes[2].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y ,planes[2].position.z);
                    //transform.position = transform.position + new Vector3(0, 0, max);
                }
                    
            }
            else
            {
                playerAnimator.SetBool("Left", false);
                playerAnimator.SetBool("Right", false);
                transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[1].position.z);
                //transform.position = planes[1].position;
#if false
                if (transform.position.z >= max)
                {
                    
                    //transform.position = transform.position + new Vector3(0, 0, -max);
                    //playerAnimator.SetBool("Left", false);
                    //playerAnimator.SetBool("Right", true);
                }

                else if (transform.position.z <= min)
                {
                    transform.position = transform.position + new Vector3(0, 0, -min);
                }
#endif
            }

            jumpAndFalling();
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isInAir)
            {
                transform.position = transform.position + new Vector3(0, jumpHeight, 0);
                isInAir = true;
            }
            /*
               if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.position.z > min)
               {
                   transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + min);
               }
               else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.position.z < max)
               {
                   transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + max);
               }

            */
        }
    }

    void jumpAndFalling()
    {
        if (isInAir == true)
        {
            playerAnimator.SetBool("Jump", true);
            this.ticks += Time.deltaTime;
        }
        if (this.ticks >= JUMP_INTERVAL)
        {
            playerAnimator.SetBool("Jump", false);
            this.ticks = 0.0f;
            isInAir = false;
            transform.position = transform.position + new Vector3(0, -jumpHeight, 0);
        }
    }

    public List<Transform> GetPlanes { get { return planes; } }
}
