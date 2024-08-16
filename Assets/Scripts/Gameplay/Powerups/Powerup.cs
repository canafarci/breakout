using UnityEngine;
using UnityEngine.UI;

using Breakout.CrossScene.Audio;
using Breakout.CrossScene.Enums;
using Breakout.Gameplay.Enums;
using Breakout.Gameplay.Powerups.Commands;

namespace Breakout.Gameplay.Powerups
{
    internal class Powerup : MonoBehaviour
    {
        [SerializeField] private RectTransform RectTransform;
        [SerializeField] private Image BackgroundImage;
        [SerializeField] private Image IconImage;
        [SerializeField] private Rigidbody2D Rigidbody2D;

        internal RectTransform rectTransform => RectTransform;
        internal Image backgroundImage => BackgroundImage;
        internal Image iconImage => IconImage;
        internal Rigidbody2D rigidbody2D => Rigidbody2D;
        
        internal PowerupID PowerupID { get; set; }
        
        private const int PADDLE_LAYER = 6;
        private const int LOSE_LIFE_ZONE_LAYER = 9;

        
        private void OnCollisionEnter2D(Collision2D other)
        {
            //comparing ints are much cheaper than .CompareTag()
            if (other.gameObject.layer == PADDLE_LAYER)
            {
                IPowerupCommand powerupCommand =  PowerupCommandFactory.instance.CreateCommand(PowerupID);
                PowerupCommandInvoker.instance.InvokeCommand(powerupCommand);
                
                AudioPlayer.instance.PlayOneShotAudio(AudioClipID.LevelStart);
                
                gameObject.SetActive(false);
            }
            
            else if (other.gameObject.layer == LOSE_LIFE_ZONE_LAYER)
            {
                gameObject.SetActive(false);
            }
        }
    }
}