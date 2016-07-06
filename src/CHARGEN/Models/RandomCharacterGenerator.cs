using System;
using System.Security.Cryptography;
using System.Text;
using CHARGEN.Interfaces;

namespace CHARGEN.Models
{
    class RandomCharacterGenerator : ICharacterGenerator
    {
        private static RandomNumberGenerator Rand { get; set; }
        private static int MinValue = 32;
        private static int MaxValue = 126;
        public RandomCharacterGenerator()
        {
            Rand = RandomNumberGenerator.Create();
        }

        public ICharacterGenerator Create()
        {
            return this;
        }

        public string GetCharacter()
        {
            var bytes = new byte[2];
            while (true)
            {
                Rand.GetBytes(bytes);
                var value = BitConverter.ToInt16(bytes, 0);
                if (value >= MinValue && value <= MaxValue)
                    return Encoding.ASCII.GetString(bytes);
            }
        }
    }
}