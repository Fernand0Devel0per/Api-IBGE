using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Domain.Helpers
{
    public static class StringFormater
    {
        public static string FormatName(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;

            name = name.ToLowerInvariant();

            string[] words = name.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    words[i] = char.ToUpperInvariant(words[i][0]) + words[i].Substring(1);
                }
            }

            return string.Join(' ', words);
        }
    }
}
