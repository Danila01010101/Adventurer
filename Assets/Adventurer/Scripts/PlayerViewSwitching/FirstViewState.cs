using GenshinImpactMovementSystem;
using UnityEngine;

namespace Adventurer
{
    public class FirstViewState : IState
    {
        private PlayerViewStateMachine stateMachine;

        public FirstViewState(PlayerViewStateMachine stateMachine) 
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }

        public void HandleInput()
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationEnterEvent()
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationExitEvent()
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationTransitionEvent()
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerEnter(Collider collider)
        {
            throw new System.NotImplementedException();
        }

        public void OnTriggerExit(Collider collider)
        {
            throw new System.NotImplementedException();
        }

        public void PhysicsUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}