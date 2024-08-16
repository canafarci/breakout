using UnityEngine;

using Breakout.CrossScene.Enums;
using Breakout.Infrastructure;

namespace Breakout.CrossScene.Data
{
    //Wrapper around PlayerPrefs save and load operations
    public class PersistentGameplayData : Singleton<PersistentGameplayData>
    {
        public int lastCompletedLevel
        {
            get => GetLastCompletedLevel();
            set => SetIntPlayerPrefsValue(PersistentDataKeys.LastCompletedLevel.ToString(), value);
        }
        
        private int GetLastCompletedLevel()
        {
            string lastCompletedLevelKey = PersistentDataKeys.LastCompletedLevel.ToString();
            
            return GetPlayerPrefValue(lastCompletedLevelKey, defaultValue: 0);
        }

        private void SetIntPlayerPrefsValue(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        
        private int GetPlayerPrefValue(string playerPrefKey, int defaultValue)
        {
            if (!PlayerPrefs.HasKey(playerPrefKey))
            {
                PlayerPrefs.SetInt(playerPrefKey, defaultValue);
            }

            return PlayerPrefs.GetInt(playerPrefKey);
        }
    }
}