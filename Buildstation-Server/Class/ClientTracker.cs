﻿using System;
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

        public ClientTracker(TcpClient Client)
        {
            this.Client = Client;
        }

        string Data;
        string[] DataSplit;

        public void Connect()
        {
            StreamReader SR = new StreamReader(Client.GetStream());
            StreamWriter SW = new StreamWriter(Client.GetStream());

            while (true)
            {
                Data = SR.ReadLine();
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
            StreamReader SR = new StreamReader(Client.GetStream());
            StreamWriter SW = new StreamWriter(Client.GetStream());
            string MessageCompiled = Sorter + ";" + Message;
            SW.WriteLine(MessageCompiled);
            SW.Flush();
        }

        /// <summary>
        /// Allows you to send multiple messages at once. Probably more efficent.
        /// </summary>
        /// <param name="Sorter">The sorter in the client you want to access.</param>
        /// <param name="Message">The message you want to send.</param>
        public void SendMessage(string Sorter, string[] Message)
        {
            StreamReader SR = new StreamReader(Client.GetStream());
            StreamWriter SW = new StreamWriter(Client.GetStream());
            int Progress = 0;
            string MessageCompiled;

            while (Progress >= Message.Length)
            {
                MessageCompiled = Sorter + ";" + Message[Progress];
                SW.WriteLine(MessageCompiled);
                Progress++;
            }
            SW.Flush();
        }
    }
}
