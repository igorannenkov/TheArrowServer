using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;

namespace TheArrowServer
{
    public partial class Form1 : Form
    {
        List<LiteEmployee> clients = new List<LiteEmployee>();
        public Form1()
        {
            InitializeComponent();
        }

        //public void SendSystemMessage(string ipAddress, string message)
        //{
        //    TcpClient client = new TcpClient(ipAddress, 8888);
        //    StreamWriter writer = new StreamWriter(client.GetStream());
        //    writer.WriteLine(message);
        //    writer.Flush();
        //}

        public void ServConnections()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 9999);
            listener.Start();
            this.Text = "Серв. запущен, ожидание подключений...";
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);

                Message inputMsg = JsonConvert.DeserializeObject<Message>(reader.ReadLine());

                switch (inputMsg.msgType)
                {
                    case MessageType.SystemMessage:
                        // Добавить в список клиента
                        // Разослать всем подключенным клиентам новый список
                        LiteEmployee liteEmp = JsonConvert.DeserializeObject<LiteEmployee>(inputMsg.msgData);
                        clients.Add(liteEmp);

                        // вот тут передаем за раз сразу массив клиентов

                        for (int i = 0; i < clients.Count; i++)
                        {
                            
                            string dataToSend = JsonConvert.SerializeObject(clients);
                            //  SendSystemMessage(clients[i].ipAddress, dataToSend);
                            Message msgToSend = new Message(MessageType.SystemMessage, dataToSend);

                            string finalDataToSend = JsonConvert.SerializeObject(msgToSend);


                            TcpClient systemClient = new TcpClient(clients[i].ipAddress, 8888);
                            NetworkStream outputStream = systemClient.GetStream();
                            StreamWriter writer = new StreamWriter(outputStream);
                            writer.WriteLine(finalDataToSend);
                            writer.Flush();
                            writer.Close();
                        }    

                        break;
                    case MessageType.TextMessage: break;
                }


               // this.Text = reader.ReadLine();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServConnections();
        }
    }
}
