using System;
using System.Collections.Generic;

namespace SharpGenerator.Helpers
{
    public static class StringHelper
    {
        public static string ToUcWords(this string str)
        {
            string[] splittedStr = ToWords(str).Split(' ');
            var words = new List<string>();
            foreach (var s in splittedStr) {
                string t = s; // s.ToLower();
                if (t.Length > 1) {
                    char firstChar = t[0];
                    words.Add(firstChar.ToString().ToUpper() + t.Substring(1));
                } else {
                    words.Add(t.ToUpper());
                }
            }
            return string.Join(" ", words);
        }
        
        public static string ToCamelCase(this string str)
        {
            string s = ToPascalCase(str);
            if (s.Length > 1) {
                char firstChar = s[0];
                return firstChar.ToString().ToLower() + s.Substring(1);
            } else {
                return s.ToLower();
            }
        }
        
        public static string ToPascalCase(this string str)
        {
            string[] splittedStr = str.ToUcWords().Split(' ');
            return string.Join("", splittedStr);
        }
        
        public static string ToWords(this string str)
        {
            str = str.Replace('_', ' ');
            string[] splittedStr = str.Split(' ');
            return string.Join(" ", splittedStr);
        }
    }
}
