using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/ZipPointJump")]

    public class ZipPointJump : StateBase
    {
        [SerializeField] private FallState _fallState;
        [SerializeField] private float _forceValue ;
        public override void EnterState()
        {
            Debug.Log("Done Move" );

            base.EnterState();
            _blackBoard.playerMovement.AddForce( _blackBoard.playerMovement.transform.up.normalized * _forceValue, ForceMode.Impulse);
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            if (_blackBoard.playerMovement.GetVelocity().y <= -10f)
            {
                _fsm.ChangeState(_fallState);
                return StateStatus.Success;
            }
            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
        }
    }
}