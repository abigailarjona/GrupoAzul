using UnityEngine;

namespace Enemy
{
    public abstract class State
    {
        protected readonly EnemyController enemyController;

        protected State(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }

        public virtual void OnStateEnter()
        {
        }

        public virtual void UpdateState()
        {
        }

        public virtual void OnStateExit()
        {
        }

        public virtual void OnHurt(int damageTaken)
        {
        }
    }
}