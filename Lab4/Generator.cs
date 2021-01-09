using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab4
{
    public class Generator
    {
        private readonly string[] _mostCommonPasswords;
        private readonly string[] _commonWords;

        private readonly Random _random;

        public Generator()
        {
            _mostCommonPasswords = File.ReadAllLines("./../../../PasswordLists/top1M.txt");
            _commonWords = File.ReadAllLines("./../../../PasswordLists/commonWords.txt");
            _random = new Random();
        }

        public List<string> GeneratePasswords(int totalAmount = 500_000, int top100WeakestPercent = 5,
            int top1MWeakestPercent = 85, int randomPercent = 5)
        {
            var passwords = new List<string>();
            for(var i = 0; i < totalAmount; i++)
            {
                passwords.Add(GeneratePasswordsAccordingToPercentage(top100WeakestPercent, top1MWeakestPercent, 
                    randomPercent));
            }
            return passwords;
        }

        private string GeneratePasswordsAccordingToPercentage(int top100WeakestPercent, int top1MWeakestPercent, 
            int randomPercent)
        {
            var currentPercent = _random.Next(100);
            
            var top100Measure = top100WeakestPercent;
            var top1MMeasure = top100Measure + top1MWeakestPercent;
            var randomMeasure  = top1MMeasure + randomPercent;
            
            if (currentPercent < top100Measure)
                return TakeFromTop100();
            if (currentPercent < top1MMeasure)
                return TakeFromTop1M();
            if (currentPercent < randomMeasure)
                return GenerateRandom();
            
            return GenerateHumanLikePassword();
        }
        
        private string TakeFromTop100() => 
            _mostCommonPasswords[_random.Next(0, 100)];
        
        private string TakeFromTop1M() => 
            _mostCommonPasswords[_random.Next(100, _mostCommonPasswords.Length)];
        
        private string GenerateRandom(int length = 20)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                          "!#$%&()*+,-./:;<=>?@[\\]^_`{|}1234567890";
            var sb = new StringBuilder();
            
            for(var i = 0; i < length; i++)
            {
                sb.Append(chars[_random.Next(0, chars.Length - 1)]);
            }
            return sb.ToString();
        }

        private string GenerateHumanLikePassword(int length = 20)
        {
            const string nonAlphabetic = "!#$%&()*+,-./:;<=>?@[\\]^_`{|}1234567890";
            var password = _commonWords[_random.Next(_commonWords.Length)];
            
            for (var i = password.Length; i < length; i++)
            {
                var randomSymbol = nonAlphabetic[_random.Next(nonAlphabetic.Length)];
                password = password.Insert(_random.Next(password.Length - 1), randomSymbol.ToString());
            }
            
            return password;
        }
    }
}