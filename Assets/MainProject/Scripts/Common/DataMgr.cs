using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;


namespace Sinabro
{
    

    //
    // my info
    //
    [System.Serializable]
    public class MyInfo
    {
        //
        public int          accountLevel_;
        public int          currentAccountExp_;

        //
        public int[]        myStatusLevels_ = new int[(int)MyStatus.MAX];

        //
        public DateTime     saveDate;

        //
        public List<MyShipData> myShipList_ = new List<MyShipData>();

        public MyInfo()
        {
            accountLevel_ = 1;
            currentAccountExp_ = 0;
        }

        //
        public void MakePlayerAbility()
        {
            //playerTrainingInfo_ = DataMgr.Instance.GetPlayerTrainingInfo(1);
            //if (playerTrainingInfo_ != null)
            //{
            //    //
            //    for (int i = 0; i < (int)SoldierType.MAX; ++i)
            //    {
            //        soldierDatas_[i] = (float)soldierTraining_[i] / 0.1f;
            //    }

            //    //
            //    float overheat = playerTrainingInfo_.Overheat - playerTrainingInfo_.OverheatMax;
            //    float finalOverheat = playerTrainingInfo_.Overheat - (overheat * DataMgr.Instance.myInfo_g.soldierDatas_[(int)SoldierType.Overheat] * 0.01f);
            //    fleetStatusDatas_[(int)FleetStatus.Overheat] = finalOverheat;

            //    //
            //    float outlook = playerTrainingInfo_.Outlook - playerTrainingInfo_.OutlookMax;
            //    float finalOutlook = playerTrainingInfo_.Outlook - (outlook * DataMgr.Instance.myInfo_g.soldierDatas_[(int)SoldierType.Outlook] * 0.01f);
            //    fleetStatusDatas_[(int)FleetStatus.Outlook] = finalOutlook;
            //}
        }

        //
        public void AddExp(long exp)
        {

        }

    }

    //
    // MyAchieve
    //
    [System.Serializable]
    public class MyAchieve
    {
        public List<MyQuestData> myDayQuestList_ = new List<MyQuestData>();
        public List<MyQuestData> myRepeatQuestList_ = new List<MyQuestData>();
        public List<MyQuestData> myAchieveQuestList_ = new List<MyQuestData>();
    }



    [System.Serializable]
    public class MyShipData
    {
        //
        public ShipDataEntity   shipInfo_;

        public int[]            defenseDatas_ = new int[(int)DefenseType.MAX];
        public double[]         shipStatusDatas_ = new double[(int)ShipStatus.MAX];


        public MyShipData()
        {
            for (int i = 0; i < (int)DefenseType.MAX; ++i)
            {
                defenseDatas_[i] = 0;
            }
            for (int i = 0; i < (int)ShipStatus.MAX; ++i)
            {
                shipStatusDatas_[i] = 0;
            }
        }

        //
        public void MakeShipAbility()
        {
            if (shipInfo_ != null)
            {
                //
                shipStatusDatas_[(int)ShipStatus.HP] = shipInfo_.Hp;
                shipStatusDatas_[(int)ShipStatus.AP] = shipInfo_.Ap;
                shipStatusDatas_[(int)ShipStatus.Aim] = shipInfo_.StartAim + DataMgr.Instance.myInfo_g.myStatusLevels_[(int)MyStatus.Aim];
                shipStatusDatas_[(int)ShipStatus.Range] = shipInfo_.Range;
                shipStatusDatas_[(int)ShipStatus.MoveSpeed] = shipInfo_.MoveSpeed;
                shipStatusDatas_[(int)ShipStatus.Fuel] = shipInfo_.Fuel;
                shipStatusDatas_[(int)ShipStatus.Shell] = shipInfo_.ShellCnt;

                float coolTime = shipInfo_.StartFireCool - shipInfo_.MaxFireCool;
                float finalCoolTime = shipInfo_.StartFireCool - (coolTime * (float)DataMgr.Instance.myInfo_g.myStatusLevels_[(int)MyStatus.CoolTime] * 0.01f);
                shipStatusDatas_[(int)ShipStatus.CoolTime] = finalCoolTime;

                shipStatusDatas_[(int)ShipStatus.DefenseSide] = defenseDatas_[(int)ShipStatus.DefenseSide];
                shipStatusDatas_[(int)ShipStatus.DefenseTop] = defenseDatas_[(int)ShipStatus.DefenseTop];
                shipStatusDatas_[(int)ShipStatus.DefenseTorpedo] = defenseDatas_[(int)ShipStatus.DefenseTorpedo];
            }
        }

    }

    [System.Serializable]
    public class MyQuestData
    {
        //
        public int questId_;
        public int questConditionType_;
        public int goalProgress_;
        public int currentProgress_;
        public bool bGetReward_;
        public bool bComplted_;


        public MyQuestData()
        {
            questId_ = 1;
            questConditionType_ = 0;
            goalProgress_ = 0;
            currentProgress_ = 0;
            bGetReward_ = false;
            bComplted_ = false;
        }
    }

    //
    public class OptionInfo
    {
        public float bgmVolume;
        public float soundVolume;
        public int bLowMode;

        public OptionInfo()
        {
            bgmVolume = 1.0f;
            soundVolume = 1.0f;
            bLowMode = 1;
        }
    }

   
    //
    // global data magager
    //
    public class DataMgr
    {
        private static DataMgr instance = null;

        public static DataMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataMgr();
                }
                return instance;
            }
        }

        private DataMgr()
        {

        }

        //
        public Dictionary<int, string> g_localizeTextTable = new Dictionary<int, string>();


        //
        //public LocalTextExcel g_localTextExcel;
        public ShipDataExcel g_shipDataExcel;



        //
        public MyInfo myInfo_g = new MyInfo();
        public MyAchieve myAchieve_g = new MyAchieve();
        public OptionInfo optionInfo_g = new OptionInfo();


        //
        public int loadingImgIndex_g = 1;
        public int currentLanguage_g = 1;


        public bool bHaveLiveDNS_g = false;
        public string liveDNS_g;

        public int platformType_g;
        public string nickName_g;


        //-----------------------------------------------------------------------------------------------------
        // localize table util
        //

        public string GetLocalExcelText(int textId)
        {
            if (g_localizeTextTable.ContainsKey(textId))
            {
                return g_localizeTextTable[textId];
            }

            return "none";
        }


        //-----------------------------------------------
        // GetShipInfo
        //-----------------------------------------------
        public ShipDataEntity GetShipInfo(int shipId)
        {
            for (int i = 0; i < g_shipDataExcel.Sheet1.Count; ++i)
            {
                if (g_shipDataExcel.Sheet1[i].Id == shipId)
                {
                    return g_shipDataExcel.Sheet1[i];
                }
            }

            return null;
        }



        //-----------------------------------------------
        // SaveAchieve
        //-----------------------------------------------
        public void SaveAchieve()
        {
            string destination = Application.persistentDataPath + "/achieve.dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, myAchieve_g);
            file.Close();
        }

        //-----------------------------------------------
        // LoadAchieve
        //-----------------------------------------------
        public bool LoadAchieve()
        {
            string destination = Application.persistentDataPath + "/achieve.dat";
            FileStream file = null;

            if (File.Exists(destination))
            {
                file = File.OpenRead(destination);
            }
            else
            {
                return false;
            }

            BinaryFormatter bf = new BinaryFormatter();
            myAchieve_g = (MyAchieve)bf.Deserialize(file);
            file.Close();

            return true;
        }




        //
        public void LoadLocalize()
        {
            //
            g_localizeTextTable.Clear();
            //for (int i = 0; i < g_localTextExcel.Sheet1.Count; ++i)
            //{
            //    if (currentLanguage_g == (int)LanguageType.Korean)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].krString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.English)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].enString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.Japanese)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].jpString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.Cn)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].cnString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.Tw)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].twString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.Spanish)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].spanishString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.Portuguese)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].portugueseString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.Indonesian)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].indonesianString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.Thai)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].thaiString);
            //    }
            //    else if (currentLanguage_g == (int)LanguageType.Vietnamese)
            //    {
            //        g_localizeTextTable.Add(g_localTextExcel.Sheet1[i].Id, g_localTextExcel.Sheet1[i].vietnameseString);
            //    }
            //}
        }


        //-----------------------------------------------
        // GetGradeColor
        //-----------------------------------------------
        public string GetGradeColor(int grade)
        {
            if (grade == 1)
            {
                return "<color=#BDBDBD>";
            }
            else if (grade == 2)
            {
                return "<color=#9FF781>";
            }
            else if (grade == 3)
            {
                return "<color=#5882FA>";
            }
            else if (grade == 4)
            {
                return "<color=#9A2EFE>";
            }

            return "<color=#BDBDBD>";
        }

        //-----------------------------------------------
        // GetShipClassColor
        //-----------------------------------------------
        public string GetShipClassColor(string shipClass)
        {
            if (string.Equals("Ap", shipClass))
            {
                return "<color=#A9A6A1>";
            }
            else if (string.Equals("DD", shipClass))
            {
                return "<color=#FFFFFF>";
            }
            else if (string.Equals("CL", shipClass))
            {
                return "<color=#49D664>";
            }
            else if (string.Equals("CA", shipClass) || string.Equals("PS", shipClass))
            {
                return "<color=#488FD7>";
            }
            else if (string.Equals("BC", shipClass) || string.Equals("BB", shipClass))
            {
                return "<color=#995EC8>";
            }

            return "<color=#A9A6A1>";
        }

        //-----------------------------------------------
        // GetAccountRankColorStr
        //-----------------------------------------------
        public string GetAccountRankColorStr(int level)
        {
            if (level <= 60)
            {
                return "<color=#E6E6E6>";
            }
            else if (level > 60 && level <= 105)
            {
                return "<color=#FFFFFF>";
            }
            else if (level > 105)
            {
                return "<color=#FE642E>";
            }


            return "<color=#E6E6E6>";
        }

        //-----------------------------------------------
        // GetHtmlColor
        //-----------------------------------------------
        public Color GetHtmlColor(string strColor)
        {
            Color color;
            ColorUtility.TryParseHtmlString(strColor, out color);
            return color;
        }

        //-----------------------------------------------
        // GetNationNick
        //-----------------------------------------------
        public string GetNationNick(int nation)
        {
            if (nation == 0)
            {
                return "HMS";
            }
            else if (nation == 1)
            {
                return "IJN";
            }
            else if (nation == 2)
            {
                return "KMS";
            }
            else if (nation == 3)
            {
                return "USS";
            }

            return "JD";
        }


        //
        public List<int> GetIntList(string intList)
        {
            List<int> result = new List<int>();
            string[] values = null;
            values = intList.Split(',');
            if (values != null)
            {
                for (int i = 0; i < values.Length; ++i)
                {
                    if (values[i].Length != 0)
                    {
                        result.Add(int.Parse(values[i]));
                    }
                }
            }

            return result;
        }


        //
        public void ShuffleList<T>(List<T> list)
        {
            int random1;
            int random2;

            T tmp;

            for (int index = 0; index < list.Count; ++index)
            {
                random1 = UnityEngine.Random.Range(0, list.Count);
                random2 = UnityEngine.Random.Range(0, list.Count);

                tmp = list[random1];
                list[random1] = list[random2];
                list[random2] = tmp;
            }
        }

        // for local
        public const int TEXT_MAIN_TITLE = 1;

        //
        public const int MAX_SAME_PLAY_SOUND = 5;

        //
        public const int DAMAGE_DOWN_PER_INCH = 10;



        // network api
        public const int VersionApi = 1;

    }
}

