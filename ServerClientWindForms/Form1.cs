using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerClientWindForms
{
    public partial class TCPServer : Form
    {
        public TCPServer()
        {
            InitializeComponent();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
        
        SimpleTcpClient client;
        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client.Connect();
                btnSend.Enabled = true;
                btnConnect.Enabled = false;  
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
        }
       
        

        private void TCPServer_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient(txtIP.Text);
            client.Events.Connected += Events_Connected;
            client.Events.DataReceived += Events_DataReceived;
            client.Events.Disconnected += Events_Disconnected;
            btnSend.Enabled = false;
        }


        private void Events_Disconnected(object sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                textInfo.Text += $"Server disconnected.{Environment.NewLine}";
            });
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                textInfo.Text += $"Server: {Encoding.UTF8.GetString(e.Data.ToArray())}{Environment.NewLine}";
            });
            }

        private void Events_Connected(object sender, ConnectionEventArgs e)
        {
                    this.Invoke((MethodInvoker)delegate
                    {
                        textInfo.Text += $"Server connected.{Environment.NewLine}"; ;
                    });
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (client.IsConnected) 
            {
              if(!string.IsNullOrEmpty(textMessage.Text)) 
                {
                    client.Send(textMessage.Text);
                    textInfo.Text += $"Me:{textMessage.Text}{Environment.NewLine}";
                    textMessage.Text = string.Empty;


                }
            
            
            }
        }
    }
}
