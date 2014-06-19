﻿using Engine.Client;
using Engine.Server;
using Engine.Utility;
using System;
using System.Collections.Generic;
namespace Engine.Effect
{
    public class CrystalEffect
    {
        /// <summary>
        /// 获得法力水晶
        /// </summary>
        public String 获得法力水晶 = String.Empty;
        /// <summary>
        /// 获得空法力水晶
        /// </summary>
        public String 获得空法力水晶 = String.Empty;
        /// <summary>
        /// 对法力水晶的法术实施
        /// </summary>
        /// <param name="role"></param>
        /// <param name="Ability"></param>
        public List<string> RunEffect(GameManager game, Utility.CardUtility.TargetSelectDirectEnum Direct)
        {
            List<string> Result = new List<string>();

            switch (Direct)
            {
                case CardUtility.TargetSelectDirectEnum.本方:
                    game.MyInfo.crystal.CurrentRemainPoint = ExpressHandler.PointProcess(game.MyInfo.crystal.CurrentRemainPoint, 获得法力水晶);
                    game.MyInfo.crystal.CurrentFullPoint = ExpressHandler.PointProcess(game.MyInfo.crystal.CurrentFullPoint, 获得空法力水晶);
                    break;
                case CardUtility.TargetSelectDirectEnum.对方:
                    game.YourInfo.crystal.CurrentRemainPoint = ExpressHandler.PointProcess(game.YourInfo.crystal.CurrentRemainPoint, 获得法力水晶);
                    game.YourInfo.crystal.CurrentFullPoint = ExpressHandler.PointProcess(game.YourInfo.crystal.CurrentFullPoint, 获得空法力水晶);
                    break;
                case CardUtility.TargetSelectDirectEnum.双方:
                    game.MyInfo.crystal.CurrentRemainPoint = ExpressHandler.PointProcess(game.MyInfo.crystal.CurrentRemainPoint, 获得法力水晶);
                    game.MyInfo.crystal.CurrentFullPoint = ExpressHandler.PointProcess(game.MyInfo.crystal.CurrentFullPoint, 获得空法力水晶);
                    game.YourInfo.crystal.CurrentRemainPoint = ExpressHandler.PointProcess(game.YourInfo.crystal.CurrentRemainPoint, 获得法力水晶);
                    game.YourInfo.crystal.CurrentFullPoint = ExpressHandler.PointProcess(game.YourInfo.crystal.CurrentFullPoint, 获得空法力水晶);
                    break;
                default:
                    break;
            }
            //Crystal#ME#4#4
            if (Direct == CardUtility.TargetSelectDirectEnum.本方)
            {
                Result.Add(ActionCode.strCrystal + CardUtility.strSplitMark + CardUtility.strMe + CardUtility.strSplitMark +
                    game.MyInfo.crystal.CurrentRemainPoint + CardUtility.strSplitMark + game.MyInfo.crystal.CurrentFullPoint);
            }
            else
            {
                Result.Add(ActionCode.strCrystal + CardUtility.strSplitMark + CardUtility.strYou + CardUtility.strSplitMark +
                    game.YourInfo.crystal.CurrentRemainPoint + CardUtility.strSplitMark + game.YourInfo.crystal.CurrentFullPoint);
            }
            return Result;
        }
        /// <summary>
        /// 对方复原操作
        /// </summary>
        /// <param name="game"></param>
        /// <param name="actField"></param>
        public static void ReRunEffect(GameManager game, String[] actField)
        {
            //Crystal#ME#4#4
            //Me代表对方 YOU代表自己，必须反过来
            if (actField[1] == CardUtility.strMe)
            {
                game.YourInfo.crystal.CurrentRemainPoint = int.Parse(actField[2]);
                game.YourInfo.crystal.CurrentFullPoint = int.Parse(actField[3]);
            }
            else
            {
                game.MyInfo.crystal.CurrentRemainPoint = int.Parse(actField[2]);
                game.MyInfo.crystal.CurrentFullPoint = int.Parse(actField[3]);
            }
        }
        /// <summary>
        /// 获得效果信息
        /// </summary>
        /// <param name="InfoArray"></param>
        public void GetField(List<string> InfoArray)
        {
            获得法力水晶 = InfoArray[0].Split("/".ToCharArray())[0];
            获得空法力水晶 = InfoArray[0].Split("/".ToCharArray())[1];
        }
    }
}