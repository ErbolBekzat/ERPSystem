using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ERPSystem.Utilities
{
    public static class Translator
    {
        private static Dictionary<string, string> wordsTranslations = new Dictionary<string, string>()
        {
            { "Пользователь", "User" },
            { "Роль", "Role" },
            { "Проект", "Project" },
            { "Задача", "Task" },
            { "Подзадача", "Subtask" },
            { "Проблема", "Problem" },
            { "Материал", "Material" },
            { "Движение Товаров", "StockMovement" },
            { "Создавать", "Create" },
            { "Просматривать", "Read" },
            { "Редактировать", "Update" },
            { "Удалять", "Delete" },
        };

        public static string TranslateWord(string word)
        {
            return wordsTranslations[word];
        }

        public static ICollection<string> TranslatePhrase(string phrase)
        {
            int lastSpaceIndex = phrase.LastIndexOf(' ');

            string firstPart = phrase.Substring(0, lastSpaceIndex);
            string lastPart = phrase.Substring(lastSpaceIndex + 1);

            string[] result = new string[2] { wordsTranslations[firstPart], wordsTranslations[lastPart] };
            return result;
        }

        //public static ICollection<string> SplitAndTranslate(string phrase)
        //{
        //    string[] words = Regex.Split(phrase, @"(?<!^)(?=[A-Z])");


        //}
    }
}
