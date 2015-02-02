using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace Buildstation_Server.Class
{
    class ClientTracker
    {
        TcpClient Client;

        public ClientTracker(TcpClient Client, string UUID)
        {
            this.Client = Client;
            this.UUID = UUID;
        }

        private string Data;
        private string[] DataSplit;
        public string UUID;
        public bool Connected = false;

        public void Connect()
        {
            StreamReader SR = new StreamReader(Client.GetStream());
            StreamWriter SW = new StreamWriter(Client.GetStream());
            Connected = true;

            while (true)
            {
                try
                {
                    Data = SR.ReadLine();
                }
                catch (Exception)
                {
                    Console.WriteLine("[INFO] Client " + UUID + " disconected.");
                    Connected = false;
                    break;
                }
                DataSplit = Data.Split(';');

                if (Data == "Sorter;Disconnect")    // Perhaps not nesasary, but I'm doing it anyway.
                {
                    Client.Close();
                    break;
                }

                NetworkThread.NetworkSorters[DataSplit[0]].NewTrafic(DataSplit[1], this);
            }
        }

        public void SendMessage(string Sorter, string Message)
        {
            try
            {
                StreamWriter SW = new StreamWriter(Client.GetStream());
                string MessageCompiled = Sorter + ";" + Message;
                SW.WriteLine(MessageCompiled);
                SW.Flush();
            }
            catch { }
        }

        /// <summary>
        /// Allows you to send multiple messages at once. Probably more efficent.
        /// </summary>
        /// <param name="Sorter">The sorter in the client you want to access.</param>
        /// <param name="Message">The message you want to send.</param>
        public void SendMessage(string Sorter, string[] Message)
        {
            try
            {
                StreamWriter SW = new StreamWriter(Client.GetStream());
                int Progress = 0;
                string MessageCompiled;

                while (Progress >= Message.Length)
                {
                    if (Progress == Message.Length)
                        break;
                    MessageCompiled = Sorter + ";" + Message[Progress];
                    SW.WriteLine(MessageCompiled);
                    Progress++;
                    Console.WriteLine("Sending message of " + MessageCompiled);
                }

                SW.Flush();
            }
            catch { }
        }
    }
}
