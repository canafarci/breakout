using System.Collections.Generic;
using Breakout.Gameplay.Powerups;
using UnityEngine;

namespace Breakout.Gameplay.Data
{
    [CreateAssetMenu(fileName = "Powerup Data SO", menuName = "Breakout/Gameplay/Powerup Data SO", order = 0)]
    public class PowerupDataSO : ScriptableObject
    {
        [SerializeField] private int PowerupsPerLevel;
        [SerializeField] private  List<PowerupDatum> PowerupData;
        [SerializeField] private  Powerup PowerupPrefab;
        [SerializeField] private  Vector2 InitialImpulse;
        [SerializeField] private  Vector2 EnlargedPaddleSize;
        [SerializeField] private  Vector2 ShortenedPaddleSize;

        internal int powerupsPerLevel => PowerupsPerLevel;
        internal List<PowerupDatum> powerupData => PowerupData;
        internal Powerup powerupPrefab => PowerupPrefab;
        internal Vector2 initialImpulse => InitialImpulse;
        internal Vector2 enlargedPaddleSize => EnlargedPaddleSize;
        internal Vector2 shortenedPaddleSize => ShortenedPaddleSize;
    }
}