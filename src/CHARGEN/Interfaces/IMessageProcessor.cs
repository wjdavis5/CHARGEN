using System;
using System.Security.Cryptography;
using System.Text;

namespace CHARGEN.Interfaces
{
    public interface IMessageProcessor
    {
        void ProcessMessage();
    }

    public interface IChargenService
    {
        int Port { get; set; }
        void Listen();
        void Stop();
    }

    public interface ICharacterGenerator
    {
        ICharacterGenerator Create();
        string GetCharacter();
    }

    
}