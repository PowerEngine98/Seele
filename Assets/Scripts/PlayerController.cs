using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace com.seele
{

    public class PlayerController : MonoBehaviour
    {
        public const string PLAYER_TAG = "Player";
        public const string ORB_TAG = "Orb";
        public const string PUSH_TAG = "Push";
        public const string TRIGGER_ORB_TAG = "Trigger_Orb";
        public const string BOOST_TAG = "Boost";
        public const float INITIAL_ENERGY = 0F;
        public const float ENERGY_PER_ORB = 1F;
        public const float NOMINAL_SPEED = 3F;
        public const float RAMP_SPEED = 8F;

        public Camera camera;
        public CameraController cameraController;
        public Animator animator;
        public Rigidbody rigidbody;
        public Collider collider;
        public NavMeshAgent agent;
        private Vector3 from;
        public Vector3 momentum;
        public float energy = INITIAL_ENERGY;
        public float dashSpeed = 2 * NOMINAL_SPEED;
        public bool inJump = false;
        public bool inDash = false;
        public float jumpSpeed = 3F;

        public float beginJumpTime;

        public bool hasJumped;

        public float endJumpTime;
        public bool grounded;
        public bool pushing;
        public bool freezed;

        void Start()
        {
            from = transform.position;
            agent.speed = NOMINAL_SPEED;
            momentum = transform.position;
        }

        void FixedUpdate()
        {
            grounded = isGrounded();
            //Jump
            if (grounded)
            {
                if (beginJumpTime > 0)
                {
                    beginJumpTime += Time.deltaTime;
                }
                if (inJump && beginJumpTime > 0.2)
                {
                    endJumpTime += Time.deltaTime;
                    if (endJumpTime >= 0.3)
                    {
                        beginJumpTime = 0;
                        inJump = false;
                        rigidbody.AddForce(new Vector3(-rigidbody.velocity.x * 0.9F, 0, -rigidbody.velocity.z * 0.9F), ForceMode.Impulse);
                        agent.enabled = true;
                        endJumpTime = 0;
                    }
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    agent.enabled = false;
                    inJump = true;
                    rigidbody.AddForce(new Vector3(momentum.x * 0.5F, 1F, momentum.z * 0.5F) * jumpSpeed, ForceMode.Impulse);
                    beginJumpTime += Time.deltaTime;
                }
            }
            //Movement and click interaction
            if (agent.enabled && !freezed && Input.GetMouseButton(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycast;
                if (Physics.Raycast(ray, out raycast))
                {
                    agent.SetDestination(raycast.point);
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (cameraController.focused)
                {
                    cameraController.Enlarge();
                }
                else
                {
                    cameraController.Focus();
                }
            }
            //Dash
            if (inDash)
            {
                agent.enabled = true;
                inDash = false;
            }
            else if (Input.GetKey(KeyCode.LeftShift) && energy > 0.5F * Time.deltaTime)
            {
                inDash = true;
                energy -= 0.5F * Time.deltaTime;
                if (isGrounded())
                {
                    agent.speed = dashSpeed;
                }
                else
                {
                    agent.enabled = false;
                    rigidbody.AddForce(momentum * agent.speed, ForceMode.Impulse);
                    agent.speed = NOMINAL_SPEED;
                }

            }
            if (!inJump)
            {
                momentum = (transform.position - from).normalized;
            }
            from = transform.position;
            //Animations
            bool walking = momentum.magnitude > 0 && grounded && !inJump && !pushing;
            animator.SetBool("walking", walking);
            animator.speed = walking ? agent.speed : 1;
            animator.SetBool("jumping", !grounded);
            animator.SetBool("pushing", pushing);
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case ORB_TAG:
                    energy += ENERGY_PER_ORB;
                    break;
                case BOOST_TAG:
                    agent.speed = RAMP_SPEED;
                    break;
                case PUSH_TAG:
                    pushing = true;
                    if (agent.enabled)
                    {
                        agent.SetDestination(other.transform.position);
                        agent.speed = NOMINAL_SPEED * 0.3F;
                    }
                    break;
            }
        }

        void OnTriggerExit(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case PUSH_TAG:
                    pushing = false;
                    agent.speed = NOMINAL_SPEED;
                    break;
                case BOOST_TAG:
                    agent.speed = NOMINAL_SPEED;
                    break;
            }
        }

        public bool isGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y + 0.04167F);
        }

        public void Dash()
        {
            inDash = true;
        }

        public void Jump()
        {
            inJump = true;
        }

        public void Die()
        {
            energy = 0F;
            LevelController.OnDie();
        }

    }

}