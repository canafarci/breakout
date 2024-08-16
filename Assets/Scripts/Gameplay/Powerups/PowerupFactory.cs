using UnityEngine;

using Breakout.Gameplay.Data;
using Breakout.Infrastructure;

namespace Breakout.Gameplay.Powerups
{
    internal class PowerupFactory : UnitySingleton<PowerupFactory>
    {
        [SerializeField] private PowerupDataSO PowerupDataSO;
        [SerializeField] private RectTransform BrickHolderRectTransform;

        internal void SpawnPowerup(Vector3 brickLocalPosition)
        {
            int randInt = Random.Range(0, PowerupDataSO.powerupData.Count);
            PowerupDatum powerupDatum = PowerupDataSO.powerupData[randInt];
            
            Powerup powerup = Instantiate(PowerupDataSO.powerupPrefab, BrickHolderRectTransform, true);
            
            SetUpPowerup(brickLocalPosition, powerup, powerupDatum);
        }

        private void SetUpPowerup(Vector3 brickLocalPosition, Powerup powerup, PowerupDatum powerupDatum)
        {
            powerup.iconImage.sprite = powerupDatum.PowerupSprite;
            powerup.backgroundImage.color = powerupDatum.PowerupBackgroundColor;
            powerup.transform.localPosition = brickLocalPosition;
            powerup.rigidbody2D.AddForce(PowerupDataSO.initialImpulse, ForceMode2D.Impulse);
            powerup.PowerupID = powerupDatum.PowerupID;
        }
    }
}