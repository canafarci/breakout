using UnityEngine;
using UnityEngine.Events;

using Breakout.CrossScene.Audio;
using Breakout.CrossScene.Enums;
using Breakout.Gameplay.Data;
using Breakout.Gameplay.Enums;
using Breakout.Gameplay.State;

namespace Breakout.Gameplay
{
    //we could use an interface to define the contract further to better encapsulate the ball's interaction with paddle,
    //however base unity does not support serialized interfaces without odin
    internal class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D Rigidbody2D;
        [SerializeField] private GameplayConfigSO GameplayConfigSO;
        [SerializeField] private RectTransform GameplayBallHolder;
        [SerializeField] private RectTransform BallRectTransform;
        
        private float _ballInitialVelocityMagnitude;
        private Vector2 _velocityBeforePause;
        
        private const int LOSE_LIFE_ZONE_LAYER = 9;

        public UnityEvent OnBallReset = new UnityEvent();
        
        private void Start()
        {
            _ballInitialVelocityMagnitude = GameplayConfigSO.initialBallVelocity.magnitude;
            GameplayStateManager.instance.OnGameplayStateChanged.AddListener(GameplayStateChangedHandler);
        }
        
        //freeze the ball when game state changes to any inactive state and cache the previous speed to
        //continue simulation when game gets unpaused
        private void GameplayStateChangedHandler(GameplayState currentState, GameplayState lastState)
        {
            if (currentState is GameplayState.Paused or GameplayState.GameWon or GameplayState.GameLost)
            {
                _velocityBeforePause = Rigidbody2D.velocity;
                Rigidbody2D.velocity = Vector2.zero;
            }
            else if (currentState is GameplayState.Active && lastState is not GameplayState.WaitingToStart or GameplayState.Active)
            {
                Rigidbody2D.velocity = _velocityBeforePause;
            }
        }

        internal void Fire()
        {
            BallRectTransform.SetParent(GameplayBallHolder);
            Rigidbody2D.AddForce(GameplayConfigSO.initialBallVelocity, ForceMode2D.Impulse);        
        }
        
        //if we just rely on physics for bouncing the ball, gameplay starts to feel dull
        //because player's agency becomes so little.
        //Therefore, we calculate the normalized direction from the center of the paddle
        //and use it to blend with normal physics bounce velocity
        //to give more control to the player
        internal void ChangeBallVelocity(Vector2 normalizedDirectionFromCenterOfPaddle)
        {
            Vector2 weightedPreviousSpeed =
                Rigidbody2D.velocity.normalized * (1f - GameplayConfigSO.ballDirectionChangeBounceNormalWeight);
            
            Vector2 weightedDirectionChangeFromCenterOfPaddle = normalizedDirectionFromCenterOfPaddle *
                                                                GameplayConfigSO.ballDirectionChangeBounceNormalWeight;
            
            Vector2 weightedBounceDirection = weightedDirectionChangeFromCenterOfPaddle + weightedPreviousSpeed;
            
            //reset the speed and give the newly calculated bounce direction based speed, at the same magnitude as the
            //starting ball speed
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.AddForce(weightedBounceDirection.normalized * _ballInitialVelocityMagnitude, ForceMode2D.Impulse);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            //comparing ints are much cheaper than .CompareTag() method
            if (other.gameObject.layer == LOSE_LIFE_ZONE_LAYER)
            {
                //decrease player health
                PlayerManager.instance.ChangePlayerHealth(-1);

                Rigidbody2D.velocity = Vector2.zero;
                //paddle listens to this event
                OnBallReset?.Invoke();
            }
            
            //ball can only collide with boundary, lose life zone and the paddle,
            //as set in the collision matrix of Physics2D settings, and we want to play a sfx
            //each time that occurs
            AudioPlayer.instance.PlayOneShotAudio(AudioClipID.BallBounce);
        }
        
        private void OnDestroy()
        {
            GameplayStateManager.instance?.OnGameplayStateChanged.RemoveListener(GameplayStateChangedHandler);
        }

    }
}