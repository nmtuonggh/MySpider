using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Swing")]
    public class SwingState : AirBoneState
    {
        [SerializeField] private LandRollState _landRollState;
        [SerializeField] private JumpFromSwingLow _jumpFromSwingLow;
        [SerializeField] private JumpFromSwing _jumpFromSwing;
        [SerializeField] protected Vector3 currentSwingPoint;
        [SerializeField] protected float startSwingVelocity;
        [SerializeField] protected float speedWhenSwing;
        [SerializeField] private ClipTransition[] _ListAnim;
        [SerializeField] private LayerMask layerMask;

        [FormerlySerializedAs("_swingAnimCount")] [SerializeField]
        private int swingAnimCount;

        private SpringJoint _springJoint;
        private Bounds _ropeHolderBounds;
        private Vector3 _randomRopePosition;
        private float angle;
        private int animIndex;
        
        private float originalMaxDistance;
        private float originalMinDistance;
        
        private LayerMask originalCollisionLayers;

        
        public override void EnterState()
        {
            _blackBoard.readyToSwing = false;
            _blackBoard.playerController.StartSwingCoroutine();
            currentSwingPoint = _blackBoard.swingPoint.position;
            animIndex = Random.Range(0, swingAnimCount);
            base.EnterState();
            SetupEnterState();
            RandomRopeShotPosition();
            Swinging();
            
            originalMaxDistance = _springJoint.maxDistance;
            originalMinDistance = _springJoint.minDistance;
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            _blackBoard.rigidbody.AddForce(_blackBoard.moveDirection.normalized * speedWhenSwing);

            if (_blackBoard.rigidbody.velocity.magnitude > 15)
            {
                _blackBoard.windEffect.Play();
            }
            else
            {
                DOVirtual.DelayedCall(0.2f, () => _blackBoard.windEffect.Stop());
            }
            
            SwingAssist();

            if (!_blackBoard.swing && elapsedTime >=0.3f)
            {
                if (_blackBoard.rigidbody.velocity.y <= 15)
                {
                    _fsm.ChangeState(_jumpFromSwingLow);
                    return StateStatus.Success;
                }

                if (_blackBoard.rigidbody.velocity.y > 15)
                {
                    _fsm.ChangeState(_jumpFromSwing);
                    return StateStatus.Success;
                }

                if (_blackBoard.foundWallBot)
                {
                    _fsm.ChangeState(_blackBoard.stateReference.JumpToSwing);
                    return StateStatus.Success;
                }
            }
            
            if (_blackBoard.playerMovement.transform.position.y >= currentSwingPoint.y - 2f)
            {
                _fsm.ChangeState(_jumpFromSwing);
                return StateStatus.Success;
            }


            return StateStatus.Running;
        }

        #region SwingAssist

        private void SwingAssist()
        {
            float groundDistance = GroundCheck();
            if (groundDistance is > 2f and < 3.5f)
            {
                _springJoint.maxDistance = Mathf.Max(_springJoint.maxDistance - 8f, 0.5f); 
                _springJoint.minDistance = Mathf.Max(_springJoint.minDistance - 4f, 0.25f);
            }
            else if (groundDistance is > 0.5f and < 2f)
            {
                _springJoint.maxDistance = Mathf.Max(_springJoint.maxDistance - 10f, 0.5f); 
                _springJoint.minDistance = Mathf.Max(_springJoint.minDistance - 5f, 0.25f); 
            }
            else if (groundDistance is > 0 and < 0.5f)
            {
                _springJoint.maxDistance = Mathf.Max(_springJoint.maxDistance - 15f, 0.5f); 
                _springJoint.minDistance = Mathf.Max(_springJoint.minDistance - 7.5f, 0.25f); 
            }
            
            float wallDistance = WallCheck();
            if (wallDistance is > 6f and <= 10f)
            {
                _springJoint.maxDistance = Mathf.Max(_springJoint.maxDistance - 8f, 0.5f); 
                _springJoint.minDistance = Mathf.Max(_springJoint.minDistance - 4f, 0.25f);
            }
            else if (wallDistance is > 3f and <= 6f)
            {
                _springJoint.maxDistance = Mathf.Max(_springJoint.maxDistance - 10f, 0.5f); 
                _springJoint.minDistance = Mathf.Max(_springJoint.minDistance - 5f, 0.25f); 
            }
            else if (wallDistance is > 0 and <= 3f)
            {
                _springJoint.maxDistance = Mathf.Max(_springJoint.maxDistance - 15f, 0.5f); 
                _springJoint.minDistance = Mathf.Max(_springJoint.minDistance - 7.5f, 0.25f); 
            }
        }


        #endregion

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            RotateVisual();
        }

        public override void LateUpdateState()
        {
            base.LateUpdateState();
            DrawLine();

        }

        public override void ExitState()
        {
            base.ExitState();
            SetupExitState();

            _blackBoard.lr.positionCount = 0;
            Destroy(_springJoint);
            _blackBoard.swing = false;
            
            _springJoint.maxDistance = originalMaxDistance; // Store this value when entering the state
            _springJoint.minDistance = originalMinDistance;
            
            _blackBoard.windEffect.Stop();
        }

        private void SwitchAnim()
        {
            if (_blackBoard.rigidbody.velocity.magnitude < 5f)
            {
                _state = _blackBoard.animancer.Play(_mainAnimation);
            }
            else
            {
                _state = _blackBoard.animancer.Play(_ListAnim[animIndex]);
            }
        }

        private void RotateVisual()
        {
            Vector3 ropeDirection = (_randomRopePosition - _blackBoard.startrope.position).normalized;
            Vector3 velocityDirection = _blackBoard.rigidbody.velocity.normalized;
            
            if (velocityDirection != Vector3.zero && ropeDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocityDirection, ropeDirection);
                
                _blackBoard.characterVisual.transform.rotation = Quaternion.Slerp(_blackBoard.characterVisual.transform.rotation,
                    targetRotation,
                    Time.fixedDeltaTime * 5f
                );
            }
        }

        private void SetupEnterState()
        {
            /*_blackBoard.normalCam.Follow = _blackBoard.characterVisual.transform;
            _blackBoard.normalCam.LookAt = _blackBoard.characterVisual.transform;*/
            //_blackBoard.normalCam.dam
            //not take layer 3
            originalCollisionLayers = _blackBoard.characterMovement.collisionLayers;
            _blackBoard.characterMovement.collisionLayers &= ~(1 << 11)| (1 << 13)  ;
            var direction = -(_blackBoard.playerMovement.transform.position - currentSwingPoint).normalized;
            _blackBoard.playerMovement.transform.rotation = Quaternion.LookRotation(direction.onlyXZ(), Vector3.up);
            var velocity = _blackBoard.playerMovement.GetVelocity();
            _blackBoard.playerMovement.rotationRate = 240f;
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = true;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints =
                RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _blackBoard.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            if (velocity.magnitude < 25)
            {
                _blackBoard.rigidbody.velocity = (velocity.normalized * startSwingVelocity);
            }
            else
            {
                _blackBoard.rigidbody.velocity = velocity;
            }
        }

        private void Swinging()
        {
            _springJoint = _blackBoard.gameObject.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = currentSwingPoint;

            var distanceFromPoint = Vector3.Distance(_blackBoard.playerSwingPos.position, currentSwingPoint);

            _springJoint.maxDistance = distanceFromPoint * 0.5f;
            _springJoint.minDistance = distanceFromPoint * 0.25f;

            _springJoint.spring = 4.5f;
            _springJoint.damper = 7f;
            _springJoint.massScale = 1f;
        }

        private void SetupExitState()
        {
            //_blackBoard.normalCam.Follow = _blackBoard.target.transform;
            //_blackBoard.normalCam.LookAt = _blackBoard.target.transform;
            _blackBoard.characterMovement.collisionLayers = originalCollisionLayers;
            Vector3 velocity = _blackBoard.rigidbody.velocity;
            _fsm.transform.DORotate(
                Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles,
                0.2f);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
            _blackBoard.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _blackBoard.playerMovement.rotationRate = 540f;
            _blackBoard.playerMovement.SetVelocity(velocity.normalized * startSwingVelocity);
        }

        private void RandomRopeShotPosition()
        {
            randomBound(_blackBoard.ropHolder);
        }

        private void randomBound(GameObject gameObject)
        {
            _ropeHolderBounds = gameObject.GetComponent<Renderer>().bounds;
            _randomRopePosition = new Vector3(
                Random.Range(_ropeHolderBounds.min.x, _ropeHolderBounds.max.x),
                Random.Range(_ropeHolderBounds.min.y, _ropeHolderBounds.max.y),
                Random.Range(_ropeHolderBounds.min.z, _ropeHolderBounds.max.z)
            );
        }

        private void DrawLine()
        {
            // Set the LineRenderer's positions
            _blackBoard.lr.positionCount = 3;
            _blackBoard.lr.SetPosition(2, _randomRopePosition);
            _blackBoard.lr.SetPosition(1, _blackBoard.startrope.position);
            _blackBoard.lr.SetPosition(0, _blackBoard.startrope.position + Vector3.down * .5f);
        }

        private float GroundCheck()
        {
            RaycastHit hit;
            if (Physics.Raycast(_fsm.transform.position, Vector3.down, out hit, 4f, layerMask))
            {
                return hit.distance;
            }
            else
            {
                return float.MaxValue;
            }
        }
        
        private float WallCheck()
        {
            RaycastHit hit;
            if (Physics.Raycast(_fsm.transform.position, Vector3.forward, out hit, 10f, layerMask))
            {
                return hit.distance;
            }
            else
            {
                return float.MaxValue;
            }
        }
        
    }
}