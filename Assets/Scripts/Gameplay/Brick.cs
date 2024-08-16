using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System.Linq;

using Breakout.CrossScene.Audio;
using Breakout.CrossScene.Enums;
using Breakout.Gameplay.Data;

namespace Breakout.Gameplay
{
    internal class Brick : MonoBehaviour
    {
        [SerializeField] private Animator Animator;
        [SerializeField] private BrickConfigSO BrickConfigSO;
        [SerializeField] private int BrickHealth;
        [SerializeField] private Image BrickImage;
        [SerializeField] private BoxCollider2D BoxCollider2D;

        private int _brickDestroyScore;
        
        //cache animation keys, as it is cheaper to calculate them only one
        private readonly int _ballHitHash = Animator.StringToHash("BallHit");
        private readonly int _brickDestroyHash = Animator.StringToHash("BrickDestroy");
        
        internal static UnityAction<Brick> OnBrickDestroyed;
        internal int brickDestroyScore => _brickDestroyScore;

        private void Awake()
        {
            SetBrickColor();
            SetBrickScore();
        }
        
        private void SetBrickColor()
        {
            Color32 brickColor = BrickConfigSO.brickData.Find(x => x.BrickHealth == BrickHealth)
                                               .BrickColor;

            BrickImage.color = brickColor;
        }

        private void SetBrickScore()
        {
            int brickScore = BrickConfigSO.brickData.FirstOrDefault(x => x.BrickHealth == BrickHealth)
                                               .BrickScore;

            _brickDestroyScore = brickScore;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            BrickHealth--;
            SetBrickColor();

            if (BrickHealth == 0)
            {
                Animator.SetTrigger(_brickDestroyHash);
                //no need to destroy the object, animation scales it down to zero
                //reason for this is to trigger garbage collector as few times as possible
                BoxCollider2D.enabled = false;
                //inform game manager
                OnBrickDestroyed?.Invoke(this);
                //play sfx
                AudioPlayer.instance.PlayOneShotAudio(AudioClipID.BrickBreak);
            }
            else
            {
                Animator.SetTrigger(_ballHitHash);
            }
        }
        
#if UNITY_EDITOR
        //as I've preferred to use the editor to place the bricks,
        //this helps to visualize things in the editor
        private void OnValidate()
        {
            SetBrickColor();
        }
#endif       
    }
}