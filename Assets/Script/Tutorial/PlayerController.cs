using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float JUMP_INTERVAL = 0.15f;
    [SerializeField] private List<Transform> planes;
    [SerializeField] private Animator playerAnimator;

    private bool isInAir;
    private float ticks = 0.0f;
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private Quaternion InitialRotation;
    [SerializeField] private bool IsHoldingDodge;
    void Start()
    {
        InitialRotation = new Quaternion(
            Mathf.Abs(PlayerSprite.GetComponent<Transform>().rotation.x),
            Mathf.Abs(PlayerSprite.GetComponent<Transform>().rotation.y),
            Mathf.Abs(PlayerSprite.GetComponent<Transform>().rotation.z),
            PlayerSprite.GetComponent<Transform>().rotation.w
            );
        IsHoldingDodge = false;
        isInAir = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerSprite.GetComponent<Transform>().rotation = new Quaternion(
                    InitialRotation.x,
                    -InitialRotation.y,
                    InitialRotation.z,
                    InitialRotation.w
                    );
                if (!IsHoldingDodge)
                {
                    playerAnimator.StopPlayback();
                    playerAnimator.SetBool("Dodge", true);
                    IsHoldingDodge = true;
                }
                else
                {
                    playerAnimator.SetBool("Dodge", false);
                }

                if (transform.position.z > planes[0].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[0].position.z);
                }
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                PlayerSprite.GetComponent<Transform>().rotation = new Quaternion(
                   -InitialRotation.x,
                   InitialRotation.y,
                   InitialRotation.z,
                   InitialRotation.w
                   );
                if (!IsHoldingDodge)
                {
                    playerAnimator.StopPlayback();
                    playerAnimator.SetBool("Dodge", true);
                    IsHoldingDodge = true;
                }
                else
                {
                    playerAnimator.SetBool("Dodge", false);
                }

                if (transform.position.z < planes[2].position.z)
                {
                    transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[2].position.z);
                }

            }
            else
            {
                playerAnimator.StopPlayback();
                IsHoldingDodge = false;
                playerAnimator.SetBool("Dodge", false);
                transform.position = new Vector3(this.transform.position.x, this.transform.position.y, planes[1].position.z);
            }

            Jump();
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isInAir)
            {
                transform.position = transform.position + new Vector3(0, jumpHeight, 0);
                isInAir = true;
            }
        }
    }

    void Jump()
    {
        if (isInAir == true)
        {
            playerAnimator.StopPlayback();
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
