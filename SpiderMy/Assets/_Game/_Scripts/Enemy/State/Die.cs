namespace SFRemastered
{
    public class Die : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.lineRenderer.positionCount = 0;
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            _blackBoard.characterController.enabled = false;

            if (_state.NormalizedTime >= 2.5f)
            {
                _blackBoard.enemyData.ReturnToPool(_blackBoard.enemyData.id, _blackBoard.gameObject);
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