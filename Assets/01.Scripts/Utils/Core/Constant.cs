using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant 
{
    #region EVENT CODE
    public const short START_PLAY_EVENT = 100;
    public const short GET_STAR_EVENT = 200;
    public const short RESET_GAME_EVENT = 300;
    public const short CLEAR_STAGE_EVENT = 500;

    public const short CLICK_PLAYER_EVENT = 400;
    public const short SELECT_VERB_WORD_EVENT = 600;

    public const short GAME_START_EVENT = 1;
    public const short POOL_EVENT = -1;
    #endregion

    #region TAG
    public const string PLAYER_TAG = "Player";
    public const string PLATFORM_TAG = "Platform";
    #endregion

    #region CONSTANT STRING
    public const string PLAYER_NAME = "민영";
    #endregion

    #region CONSTANT LEVEL
    public const float DEAD_LINE_Y = -10f;
    public const float SPAWN_CHARACTER_Y = 5f;
    #endregion

    public static readonly string[] UNITS_NAME = { "조금", "보통", "엄청" };
}
