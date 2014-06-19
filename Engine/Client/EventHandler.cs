﻿using Engine.Utility;
using System;
using System.Collections.Generic;

namespace Engine.Client
{
    public class EventHandler
    {
        /// <summary>
        /// 事件池
        /// </summary>
        public List<CardUtility.全局事件> 事件池 = new List<CardUtility.全局事件>();
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <returns></returns>
        public List<String> 事件处理(GameManager game)
        {
            List<String> Result = new List<string>();
            for (int j = 0; j < 事件池.Count; j++)
            {
                CardUtility.全局事件 事件 = 事件池[j];
                for (int i = 0; i < game.MyInfo.BattleField.MinionCount; i++)
                {
                    if (!(事件.触发事件类型 == CardUtility.事件类型列表.召唤 && 事件.触发位置.Postion == (i + 1) && 事件.触发位置.本方对方标识))
                    {
                        Result.AddRange(game.MyInfo.BattleField.BattleMinions[i].事件处理方法(事件, game));
                    }
                }
                //转换触发方向，对方触发事件？结果是否传送？传送时候要改变strMe和strYou！
                事件.触发位置.本方对方标识 = !事件.触发位置.本方对方标识;
                for (int i = 0; i < game.YourInfo.BattleField.MinionCount; i++)
                {
                    game.YourInfo.BattleField.BattleMinions[i].事件处理方法(事件, game);
                }
            }
            return Result;
        }
    }
}