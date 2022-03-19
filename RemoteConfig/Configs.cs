using UnityEngine;
using Unity.RemoteConfig;

namespace CardBattle.RemoteConfig
{
    public class Configs : MonoBehaviour
    {
        public static Configs Instance;

        public struct userAttributes { }

        public struct appAttributes { }

        private bool fetched = false;
        public bool Fetched => fetched;

        private static int financeBox_DiceAmount = 2;
        public static int FinanceBox_DiceAmount => financeBox_DiceAmount;

        private static float debt_Interest = 1.5f;
        public static float Debt_Interest => debt_Interest;

        private static float revenue_TownFactor = 2f;
        public static float Revenue_TownFactor => revenue_TownFactor;

        private static float revenue_VillageFactor = 1f;
        public static float Revenue_VillageFactor => revenue_VillageFactor;

        private static int gameManager_InitialMoney = 5;
        public static int GameManager_InitialMoney => gameManager_InitialMoney;

        private static float playerController_Speed = 2;
        public static float PlayerController_Speed => playerController_Speed;

        private int configVersion = 1;
        public int ConfigVersion => configVersion;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this.gameObject);

            DontDestroyOnLoad(Instance);
            ConfigManager.FetchCompleted += ApplyRemoteSettings;
            ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());
        }

        void ApplyRemoteSettings(ConfigResponse configResponse)
        {
            // Conditionally update settings, depending on the response's origin:
            switch (configResponse.requestOrigin)
            {
                case ConfigOrigin.Default:
                    Debug.Log("No settings loaded this session; using default values.");
                    break;
                case ConfigOrigin.Cached:
                    Debug.Log("No settings loaded this session; using cached values from a previous session.");
                    break;
                case ConfigOrigin.Remote:
                    Debug.Log("New settings loaded this session; update values accordingly.");
                    financeBox_DiceAmount = ConfigManager.appConfig.GetInt("financeBox_DiceAmount");
                    debt_Interest = ConfigManager.appConfig.GetInt("debt_Interest");
                    revenue_TownFactor = ConfigManager.appConfig.GetFloat("revenue_TownFactor");
                    revenue_VillageFactor = ConfigManager.appConfig.GetFloat("revenue_VillageFactor");
                    gameManager_InitialMoney = ConfigManager.appConfig.GetInt("gameManager_InitialMoney");
                    playerController_Speed = ConfigManager.appConfig.GetInt("playerController_Speed");
                    configVersion = ConfigManager.appConfig.GetInt("configVersion");
                    break;
            }
            fetched = true;
        }
    }
}
