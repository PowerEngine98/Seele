using System.Collections;
using System;
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
        public const float DASH_RANGE = 3.5F;
        public const float DASH_DURATION = 0.08F;
        public const float DASH_COOLDOWN = 0.5F;
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
        public bool inJump;
        public bool hasJumped;
        public bool inDash;
        public bool inDashCooldown;
        public float jumpSpeed = 3F;
        public float dashTime;
        public bool detached;
        public bool grounded;
        public bool pushing;
        public bool freezed;

        void Start()
        {
            from = transform.position;
            agent.speed = NOMINAL_SPEED;
            momentum = Vector3.zero;
        }

        void FixedUpdate()
        {
            grounded = isGrounded();
            //Jump
            if (grounded)
            {
                if (inJump)
                {
                    hasJumped = false;
                    inJump = false;
                    rigidbody.AddForce(new Vector3(-rigidbody.velocity.x * 0.9F, 0, -rigidbody.velocity.z * 0.9F), ForceMode.Impulse);
                    Attach();
                }
                else if (!hasJumped && Input.GetKey(KeyCode.Space))
                {
                    hasJumped = true;
                    Detach();
                    rigidbody.AddForce(new Vector3(momentum.x * 0.5F, 1F, momentum.z * 0.5F) * jumpSpeed, ForceMode.Impulse);
                    DelayedTask.Execute(0.2F, () =>
                    {
                        inJump = true;
                    });
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
            if (!inDash && !inDashCooldown && Input.GetKey(KeyCode.W))
            {
                inDash = true;
                energy--;
                if (agent.enabled)
                {
                    agent.isStopped = true;
                    agent.ResetPath();
                }
                Detach();
                Vector3 axis = transform.rotation.ToEulerAngles();
                Vector3 direction = momentum.x + momentum.z == 0 ? new Vector3((float)Math.Sin(axis.y), 0, (float)Math.Cos(axis.y)) : momentum;
                Vector3 dashVector = direction * (DASH_RANGE / DASH_DURATION);
                rigidbody.velocity = new Vector3(dashVector.x, 0, dashVector.z);
                DelayedTask.Execute(DASH_DURATION, () =>
                {
                    inDash = false;
                    rigidbody.velocity = new Vector3(0, 0, 0);
                    inDashCooldown = true;
                    DelayedTask.Execute(DASH_COOLDOWN, () =>
                    {
                        inDashCooldown = false;
                    });
                });
                Attach(DASH_DURATION + 0.5F);
            }
            //Momentum
            if (!inJump)
            {
                momentum = (transform.position - from).normalized;
            }
            from = transform.position;
            //Facing
            /*if (momentum.magnitude == 0)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit raycast;
                if (Physics.Raycast(ray, out raycast))
                {
                    Vector3 turnVector = Vector3.RotateTowards(transform.forward, raycast.point - transform.position, 0.05F, 0.0f);
                    transform.rotation = Quaternion.LookRotation(turnVector);
                }
            }
            */
            //NavMesh attachment
            if (!detached && grounded && !agent.enabled)
            {
                agent.enabled = true;
            }
            //Animations
            bool walking = momentum.magnitude > 0 && grounded && !inJump && !pushing;
            animator.speed = walking ? agent.speed : 1;
            animator.SetBool("walking", walking);
            animator.SetBool("jumping", !grounded);
            animator.SetBool("dashing", pushing);
            animator.SetBool("pushing", pushing);
        }
        private void Detach()
        {
            detached = true;
            agent.enabled = false;
        }

        private void Detach(float delay, VoidCallback callback)
        {
            Detach();
            DelayedTask.Execute(delay, callback);
        }

        private void Attach()
        {
            detached = false;
            //agent.enabled = true;
        }

        private void Attach(float delay)
        {
            DelayedTask.Execute(delay, () =>
            {
                Attach();
            });
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