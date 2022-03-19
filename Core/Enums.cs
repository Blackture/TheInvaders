using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle.Core
{
    public enum COLOR
    {
        WHITE,
        RED,
        BLUE,
        GREEN
    }

    public enum MULTIPLIER
    {
        ZERO,
        NONE,
        DOUBLE,
        QUADRUPLE
    }

    /// <summary>
    /// <list type="bullet">
    /// <item>
    ///     N and S are not available for hexagonal fields.
    /// </item>
    /// <item>
    ///     For the chessboard only E,N,S,W are available
    /// </item>
    /// </list>
    /// 
    /// </summary>
    public enum DIRECTION
    {
        NE,
        E,
        SE,
        SW,
        W,
        NW,
        N,
        S
    }

    public enum GAMEMODE
    {
        QUICKMATCH,
        NORMAL
    }

    public enum DIFFICULTY
    {
        TRAINING, //infinity secs per turn on battlefield
        NO_CHALLENGE_AT_ALL, //30 secs per turn on battlefield
        EASY, //15 secs per turn 
        NORMAL, //7.5 secs per turn on battlefield
        HARD, //5 secs per turn on battlefield
        SERIOUSLY_INSANE, //3.75 secs per turn on battlefield
        WHAT_THE_HELL_ARE_YOU_DOING //1.875 sec per turn on battlefield
    }
}