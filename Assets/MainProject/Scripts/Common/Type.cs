using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sinabro
{
    //
    public enum ShipStatus
    {
        HP = 0,
        AP = 1,
        Aim = 2,
        CoolTime = 3,
        Range = 4,
        DefenseSide = 5,
        DefenseTop = 6,
        DefenseTorpedo = 7,
        MoveSpeed = 8,
        Fuel = 9,
        Shell = 10,
        MAX
    }

    //
    public enum MyStatus
    {
        Aim = 0,
        CoolTime = 1,
        MAX
    }

    //
    public enum DefenseType
    {
        Side = 0,
        Top = 1,
        Torpedo = 2,
        MAX
    }

    //
    public enum AttackType
    {
        Shell = 0,
        Torpedo = 1,
        AirBomb = 2,
        MAX
    }

    public enum LanguageType
    {
        English = 0,
        Korean = 1,
        Japanese = 2,
        Cn = 3,
        Tw = 4,
        Spanish = 5,
        Portuguese = 6,
        Indonesian = 7,
        Thai = 8,
        Vietnamese = 9,
        MAX = 10
    }

    public enum ServerType
    {
        Dev = 0,
        Test = 1,
        QA = 2,
        Review = 3,
        Live = 4,
        Dev2 = 5
    }

    //
    public enum PlatformType
    {
        Guest = 1,
        Google = 2,
        Apple = 3,
        Facebook = 4
    }

    //
    public enum StoreType
    {
        Google = 1,
        Apple = 2
    }



    public enum SoundType
    {
        Fx = 0,
        Bgm = 1
    }


    //
    public static class Utils
    {
        public static string GetLanguageName(LanguageType type)
        {
            switch (type)
            {
                case LanguageType.English:
                    return "English";

                case LanguageType.Korean:
                    return "Korean";

                case LanguageType.Japanese:
                    return "Japan";

                case LanguageType.Cn:
                    return "China";

                case LanguageType.Tw:
                    return "Taiwan";

                default:
                    break;
            }

            return "English";
        }
    }
}