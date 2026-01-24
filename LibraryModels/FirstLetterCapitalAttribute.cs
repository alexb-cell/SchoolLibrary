using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels
{
    public class FirstLetterCapitalAttribute:ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
           string word = value.ToString();
           char firstLetter = word[0];
           char last = '1';
           
            if (firstLetter < 'A' || firstLetter > 'Z')
                return false;

            for (int i = 1; i < word.Length; i++)
            {
                if (last != ' ')
                {
                    char x = word[i];
                    if ((x < 'a' || x > 'z') && word[i]!=' ')
                        return false;
                }
                else
                {
                    if (word[i] < 'a' || word[i] > 'z'|| word[i] < 'A' || word[i] > 'Z')
                        return false;
                } 
                last = word[i];
               
            }
            return true;

        }
    }
}
