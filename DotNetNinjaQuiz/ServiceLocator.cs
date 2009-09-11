using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetNinjaQuizLib.Domain;
using System.Configuration;

namespace DotNetNinjaQuiz
{
    static class ServiceLocator
    {
        public static GameController Game { get; private set; }
        
        public static gfx.ImageService Images { get; private set; }
        
        public static gfx.SoundService Sound{ get; private set; }

        static ServiceLocator()
        {
            CreateNewGame();
            Images = new gfx.ImageService();
            Sound = new gfx.SoundService();
        }

        public static void CreateNewGame()
        {
            if (Game != null)
                Game.Dispose();
            Game = new GameController(ConfigurationSettings.AppSettings["questionsDatabase"]);
        }
    }
}
