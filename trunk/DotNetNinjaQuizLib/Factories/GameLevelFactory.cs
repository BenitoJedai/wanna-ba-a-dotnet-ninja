using System;
using DotNetNinjaQuizLib.Domain;
using System.Collections.Generic;

namespace DotNetNinjaQuizLib.Factories
{
    /// <summary>
    /// This is basically a hard-coded configuration class,
    /// deciding the steps / difficulty levels in the game.
    /// </summary>
    internal static class GameLevelFactory
    {
        internal static SortedList<int, GameLevel> CreateDefaultGameLevels()
        {
            SortedList<int, GameLevel> list = new SortedList<int, GameLevel>();

            #region levels 1 to 5
            list.Add(
                1, new GameLevel()
            {
                Label = "$100",
                DifficultySelector = new DifficultyDecider_Easy(),
            });
            list.Add(
                2, new GameLevel()
                {
                    Label = "$200",
                    DifficultySelector = new DifficultyDecider_Easy(),
                });
            list.Add(
                3, new GameLevel()
                {
                    Label = "$300",
                    DifficultySelector = new DifficultyDecider_EasyToMedium(),
                });
            list.Add(
                4, new GameLevel()
                {
                    Label = "$500",
                    DifficultySelector = new DifficultyDecider_EasyToMedium(),
                });
            list.Add(
                5, new GameLevel()
                {
                    Label = "$1,000",
                    DifficultySelector = new DifficultyDecider_EasyToMedium(),
                });
            #endregion
            #region levels 6 to 10
            list.Add(
                6, new GameLevel()
                {
                    Label = "$2,000",
                    DifficultySelector = new DifficultyDecider_Medium(),
                });
            list.Add(
                7, new GameLevel()
                {
                    Label = "$4,000",
                    DifficultySelector = new DifficultyDecider_Medium(),
                });
            list.Add(
                8, new GameLevel()
                {
                    Label = "$8,000",
                    DifficultySelector = new DifficultyDecider_Medium(),
                });
            list.Add(
                9, new GameLevel()
                {
                    Label = "$16,000",
                    DifficultySelector = new DifficultyDecider_MediumToHard(),
                });
            list.Add(
                10, new GameLevel()
                {
                    Label = "$32,000",
                    DifficultySelector = new DifficultyDecider_MediumToHard(),
                });
            #endregion
            #region levels 11 to 15
            list.Add(
                11, new GameLevel()
                {
                    Label = "$64,000",
                    DifficultySelector = new DifficultyDecider_MediumToHard(),
                });
            list.Add(
                12, new GameLevel()
                {
                    Label = "$125,000",
                    DifficultySelector = new DifficultyDecider_MediumToHard(),
                });
            list.Add(
                13, new GameLevel()
                {
                    Label = "$250,000",
                    DifficultySelector = new DifficultyDecider_Difficult(),
                });
            list.Add(
                14, new GameLevel()
                {
                    Label = "$500,000",
                    DifficultySelector = new DifficultyDecider_Difficult(),
                });
            list.Add(
                15, new GameLevel()
                {
                    Label = "$1 MILLION",
                    DifficultySelector = new DifficultyDecider_Difficult(),
                });
            #endregion

            return list;
        }
    }
}
