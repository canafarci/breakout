using UnityEngine;

using Breakout.Gameplay.Data;
using Breakout.Gameplay.Enums;
using Breakout.Gameplay.State;

namespace Breakout.Gameplay
{
    internal class Paddle : MonoBehaviour
    {
        [SerializeField] private Ball Ball;
        [SerializeField] private GameplayConfigSO GameplayConfigSO;
        [SerializeField] private RectTransform BallRectTransform;
        [SerializeField] private RectTransform GameplayBoundary;
        [SerializeField] private RectTransform PaddleRectTransform;
        [SerializeField] private RectTransform BallStartPositionParent;
        [SerializeField] private BoxCollider2D BoxCollider2D;
        
        private float _boundaryWidth;
        private PaddleState _paddleState;
        
        private void Start()
        {
            _boundaryWidth = GameplayBoundary.rect.width;
            _paddleState = PaddleState.NotFiredBall;
            
            Ball.OnBallReset.AddListener(BallResetHandler);
        }
        
        //move the paddle in fixed update because ball bounces use physics
        private void FixedUpdate()
        {
            if (GameplayStateManager.instance.currentGameState == GameplayState.Active)
            {
                MovePaddle();
            }
        }
        
        //this method is physics independent and input for it a keyDown input, 
        //so make it work in update
        //we can simulate GetKeyDown in FixedUpdate as well using bit flags
        //but that is beyond scope. for reference, in my repo Forsaken Graves
        //there is an implementation of it at https://github.com/canafarci/ForsakenGraves/blob/119e64fa9f4d716576f4072cd5712c5e1bd3c239/Forsaken%20Graves/Assets/Scripts/Gameplay/Inputs/InputPoller.cs#L25
        private void Update()
        {
            if (GameplayStateManager.instance.currentGameState == GameplayState.Active)
            {
                CheckAndFireBall();
            }
        }
        
        private void MovePaddle()
        {
            if (Mathf.Approximately(InputPoller.instance.horizontalInput, 0f)) return;
            
            float clampedPositionX = ClampMovePosition();

            PaddleRectTransform.localPosition = new Vector2(clampedPositionX, PaddleRectTransform.localPosition.y);
        }
        
        //clamp the moved paddle relative to the canvas boundary
        private float ClampMovePosition()
        {
            float newPaddleXPosition = PaddleRectTransform.localPosition.x +
                                       InputPoller.instance.horizontalInput * 
                                       GameplayConfigSO.paddleSpeed * 
                                       Time.fixedDeltaTime;
            
            float paddleWidth = PaddleRectTransform.rect.width;

            float clampedPositionX = Mathf.Clamp(newPaddleXPosition, 
                                                 -_boundaryWidth / 2 + paddleWidth /2,
                                                 _boundaryWidth / 2 - paddleWidth /2);
            return clampedPositionX;
        }
        
        private void CheckAndFireBall()
        {
            if (_paddleState == PaddleState.FiredBall) return;
            
            if (InputPoller.instance.fireBallInput)
            {
                Ball.Fire();
                _paddleState = PaddleState.FiredBall;
            }
        }
        
        //this method is called from an event from Ball,
        //to prevent circular dependency between classes
        private void BallResetHandler()
        {
            BallRectTransform.SetParent(BallStartPositionParent);
            BallRectTransform.localPosition = Vector3.zero;
            
            _paddleState = PaddleState.NotFiredBall;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            //in physics settings 2D, paddle can only collide with the ball,
            //so there is no need to check for layers or tags
            
            //get direction vector from center of paddle to the ball's contact point to
            //manipulate bounce angle of the ball
            Vector2 contactPoint = other.GetContact(0).point;
            Vector2 directionFromCenterOfPaddle = contactPoint - new Vector2(BoxCollider2D.transform.position.x, BoxCollider2D.transform.position.y);
            
            Ball.ChangeBallVelocity(directionFromCenterOfPaddle.normalized);
        }
        
        private enum PaddleState
        {
            FiredBall,
            NotFiredBall
        }
    }
}