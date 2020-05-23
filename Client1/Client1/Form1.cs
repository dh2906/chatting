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
using System.Threading;
using System.Windows.Input;

namespace Client1
{
    public partial class Main : Form
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream stream = default(NetworkStream);
        string message = string.Empty;

        public Main()
        {
            InitializeComponent();
            StartChatting();
        }

        private void StartChatting()
        {
            btn_Send.BackColor = SystemColors.GradientActiveCaption;
            btn_Send.ForeColor = SystemColors.GradientActiveCaption;

            clientSocket.Connect(IP.IPAddress, IP.Port);
            stream = clientSocket.GetStream();

            byte[] buffer = Encoding.Unicode.GetBytes(IP.IPAddress + "$");
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();

            Thread t_handler = new Thread(GetMessage);
            t_handler.IsBackground = true;
            t_handler.Start();
        }

        private void GetMessage()
        {
            while (true)
            {
                stream = clientSocket.GetStream();
                int BUFFERSIZE = clientSocket.ReceiveBufferSize;
                byte[] buffer = new byte[BUFFERSIZE];
                int bytes = stream.Read(buffer, 0, buffer.Length);

                string message = Encoding.Unicode.GetString(buffer, 0, bytes);
                DisplayText(message, false);
            }
        }
        private void Send()
        {
            if (txt_Wrt.Text != string.Empty)
            {
                byte[] buffer = Encoding.Unicode.GetBytes(txt_Wrt.Text + "$");
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();

                DisplayText("나" + " : " + txt_Wrt.Text, true);
                txt_Wrt.ResetText();
                txt_Wrt.Focus();
                
            }
        }

        // flag -> true = 자신, false = 상대방
        private void DisplayText(string text, bool flag)
        {
            if (txt_Show.InvokeRequired)
            {
                txt_Show.BeginInvoke(new MethodInvoker(delegate
                {
                    if(flag == false)
                        txt_Show.SelectionAlignment = HorizontalAlignment.Left;

                    else
                        txt_Show.SelectionAlignment = HorizontalAlignment.Right;

                    txt_Show.AppendText(text + Environment.NewLine);
                    
                }));
            }

            else
            {
                if (flag == false)
                    txt_Show.SelectionAlignment = HorizontalAlignment.Left;

                else
                    txt_Show.SelectionAlignment = HorizontalAlignment.Right;

                txt_Show.AppendText(text + Environment.NewLine);
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            Send();
        }

        private void txt_Wrt_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter==e.KeyCode) Send();
            if (e.KeyCode == Keys.Back && txt_Wrt.Text == string.Empty) btn_Send.Enabled = false;
            else btn_Send.Enabled = btn_Send.Text == string.Empty ? false : true;

            if(txt_Wrt.Text == string.Empty)
            {
                btn_Send.BackColor = SystemColors.GradientActiveCaption;
                btn_Send.ForeColor = SystemColors.GradientActiveCaption;
            }

            else
            {
                btn_Send.BackColor = color;
                btn_Send.ForeColor = SystemColors.GradientActiveCaption;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }


    class IP
    {
        // LocalHost IPAddress,Port 설정
        public static string IPAddress
        {
            get
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (var item in host.AddressList)
                    if (item.AddressFamily == AddressFamily.InterNetwork) return item.ToString();
                return "127.0.0.1";
            }
        }
        public static int Port
        {
            get { return 13000; }
        }
    }
}
