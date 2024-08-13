namespace SFRemastered
{
    public class Die : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.characterController.detectCollisions = false;
            /*_state.Events.OnEnd += () =>
            {
                
            };*/
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            
            if (_state.NormalizedTime >= 2.5f)
            {
                _blackBoard.enemyData.ReturnToPool(_blackBoard.enemyData.id, _blackBoard.gameObject);
                _blackBoard.characterController.detectCollisions = true;
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