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
        
        public bool hasCompletedControlsTutorial
        {
            get => GetHasCompletedControlsTutorial();
            set => SetBoolPlayerPrefsValue(PersistentDataKeys.HasCompletedFirstGameplayTutorial.ToString(), value);
        }
        
        private bool GetHasCompletedControlsTutorial()
        {
            string hasCompletedControlsTutorialKey = PersistentDataKeys.HasCompletedFirstGameplayTutorial.ToString();

            return GetPlayerPrefValue(hasCompletedControlsTutorialKey, defaultValue: 0) != 0;
        }
        
        private int GetLastCompletedLevel()
        {
            string lastCompletedLevelKey = PersistentDataKeys.LastCompletedLevel.ToString();
            
            return GetPlayerPrefValue(lastCompletedLevelKey, defaultValue: 0);
        }
        
        private int GetPlayerPrefValue(string playerPrefKey, int defaultValue)
        {
            if (!PlayerPrefs.HasKey(playerPrefKey))
            {
                PlayerPrefs.SetInt(playerPrefKey, defaultValue);
            }

            return PlayerPrefs.GetInt(playerPrefKey);
        }

        private void SetIntPlayerPrefsValue(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        
        private void SetBoolPlayerPrefsValue(string key, bool value)
        {
            int valueToSet = value ? 1 : 0;
            PlayerPrefs.SetInt(key, valueToSet);
        }
    }
}