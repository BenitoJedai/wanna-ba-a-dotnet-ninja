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
        public static GameController Game { get; set; }
        public static gfx.ImageService Images { get; set; }

        static ServiceLocator()
        {
            Game = new GameController(ConfigurationSettings.AppSettings["questionsDatabase"]);
            Images = new gfx.ImageService();
        }
    }
}
