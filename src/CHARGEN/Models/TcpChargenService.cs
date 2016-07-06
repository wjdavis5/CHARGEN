using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CHARGEN.Interfaces;

namespace CHARGEN.Models
{
    public class TcpChargenService : IChargenService
    {
        private ICharacterGenerator CharacterGenerator { get; set; }
        public int Port { get; set; }
        private bool RunService { get; set; }
        private TcpListener TcpListener { get; set; }

        public TcpChargenService(ICharacterGenerator characterGenerator,int port=19)
        {
            CharacterGenerator = characterGenerator;
            Port = port;
        }

        public void Listen()
        {
            TcpListener = new TcpListener(IPAddress.Any, Port);
            RunService = true;
            TcpListener.Start();
            AcceptClient();
        }

        private void AcceptClient()
        {
            TcpListener.AcceptTcpClientAsync().ContinueWith(SendCharactersToConnectedClient);
        }

        private void SendCharactersToConnectedClient(Task<TcpClient> t)
        {
            AcceptClient();
            var client = t.Result;
            var characterGen = CharacterGenerator.Create();
            using (var stream = client.GetStream())
            {
                while (RunService && stream.CanWrite && client.Connected)
                {
                    try
                    {
                        var ch = Encoding.ASCII.GetBytes(characterGen.GetCharacter());
                        stream.Write(ch, 0, ch.Length);
                    }
                    catch (Exception) { }
                }
            }
        }


        public void Stop()
        {
            RunService = false;
        }
    }
}