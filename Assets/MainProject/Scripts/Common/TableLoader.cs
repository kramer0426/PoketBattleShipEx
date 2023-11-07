using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sinabro
{

    public class TableLoader : MonoBehaviour
    {
        //[SerializeField] LocalTextExcel localTextExcel_;
        [SerializeField] ShipDataExcel      shipDataExcel_;
        [SerializeField] EnemyShipDataExcel enemyDataExcel_;


        void Start()
        {
            LoadData();
        }

        //
        public void LoadData()
        {
            //
            //DataMgr.Instance.g_localTextExcel = localTextExcel_;
            DataMgr.Instance.g_shipDataExcel = shipDataExcel_;
            DataMgr.Instance.g_enemyDataExcel = enemyDataExcel_;



            //
            int language = PlayerPrefs.GetInt("Lanuage", -1);
            if (language == -1)
            {
                //set system language
                SystemLanguage systemLanguage = Application.systemLanguage;
                if (SystemLanguage.English == systemLanguage)
                {
                    language = (int)LanguageType.English;
                }
                else if (SystemLanguage.Korean == systemLanguage)
                {
                    language = (int)LanguageType.Korean;
                }
                else if (SystemLanguage.Japanese == systemLanguage)
                {
                    language = (int)LanguageType.Japanese;

                    // test code
                    language = (int)LanguageType.English;
                }
                else if (SystemLanguage.ChineseSimplified == systemLanguage)
                {
                    language = (int)LanguageType.Cn;

                    // test code
                    language = (int)LanguageType.English;
                }
                else if (SystemLanguage.ChineseTraditional == systemLanguage)
                {
                    language = (int)LanguageType.Tw;

                    // test code
                    language = (int)LanguageType.English;
                }
                else if (SystemLanguage.Spanish == systemLanguage)
                {
                    language = (int)LanguageType.Spanish;

                    // test code
                    language = (int)LanguageType.English;
                }
                else if (SystemLanguage.Portuguese == systemLanguage)
                {
                    language = (int)LanguageType.Portuguese;

                    // test code
                    language = (int)LanguageType.English;
                }
                else if (SystemLanguage.Indonesian == systemLanguage)
                {
                    language = (int)LanguageType.Indonesian;

                    // test code
                    language = (int)LanguageType.English;
                }
                else if (SystemLanguage.Thai == systemLanguage)
                {
                    language = (int)LanguageType.Thai;

                    // test code
                    language = (int)LanguageType.English;
                }
                else if (SystemLanguage.Vietnamese == systemLanguage)
                {
                    language = (int)LanguageType.Vietnamese;

                    // test code
                    language = (int)LanguageType.English;
                }
                else
                {
                    language = (int)LanguageType.English;
                }

                PlayerPrefs.SetInt("Lanuage", language);

            }


            // english = 0, korea = 1
            DataMgr.Instance.currentLanguage_g = language;
            DataMgr.Instance.LoadLocalize();

        }

    }
}
