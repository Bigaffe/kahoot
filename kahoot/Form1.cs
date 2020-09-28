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
using System.Threading;
using System.Diagnostics;

namespace kahoot
{
    public partial class Form1 : Form
    {
        TcpListener lyssanre;

        TcpClient klienten = new TcpClient();
        List<TcpClient> klienter = new List<TcpClient>();

        //List<Klienter> allaKlienter = new List<Klienter>();
        /// <summary>
        /// Porten till servern
        /// </summary>
        int port = 12345;
        bool stänger = false;
        bool ansluten = false;

      
        public Form1()
        {
            InitializeComponent();
            //klienten.NoDelay = true;
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                
                lyssanre = new TcpListener(IPAddress.Any, port);
                lyssanre.Start();
            }
            catch(Exception error) { lbxLista.Items.Add(error.Message + "Hejsan numme 1" );return; }
            lbxLista.Items.Add("Knapp klar");
            btnStart.Enabled = false;
            StartaMottagning();
        }
        /// <summary>
        /// Tar emot ny klient
        /// </summary>
        public async void StartaMottagning()
        {
            try
            {

                TcpClient k = await lyssanre.AcceptTcpClientAsync();
                klienter.Add(k);
                StartaLäsning(k);
                broadcast(((IPEndPoint)k.Client.RemoteEndPoint).Address + ":" + ((IPEndPoint)k.Client.RemoteEndPoint).Port + "> User connected " + "\r\n");
                lbxLista.Items.Add("Mottagning klar");
                lbxLista.Items.Add(((IPEndPoint)k.Client.RemoteEndPoint).Port.ToString());
                StartaMottagning();

            }
            catch (Exception error) { lbxLista.Items.Add(error.Message + "Hejsan numme 2"); return; }
            
            
        }
        /// <summary>
        /// Läser in datan som klienter skickar in
        /// </summary>
        public async void StartaLäsning(TcpClient k)
        {
            if(stänger == false)
            {
                byte[] buffer = new byte[1028];
                int l = 0;
                try
                {
                    l = await k.GetStream().ReadAsync(buffer, 0, buffer.Length);
                }
                catch(Exception error) { lbxLista.Items.Add(error.Message); }
                string msg = Encoding.Unicode.GetString(buffer, 0, l);
                if (msg != "/CLOST CONNECTION")
                {
                    Debug.WriteLine(((IPEndPoint)k.Client.RemoteEndPoint).Address + ":" + ((IPEndPoint)k.Client.RemoteEndPoint).Port + "> \"" + msg + "\"" + "\r\n");
                }
                broadcast(((IPEndPoint)k.Client.RemoteEndPoint).Address + ":" + ((IPEndPoint)k.Client.RemoteEndPoint).Port + "> \"" + msg + "\"" + "\r\n");
                StartaLäsning(k);
            }
            else
            {
                if(stänger == false)
                {
                lbxLista.Items.Add(((IPEndPoint)k.Client.RemoteEndPoint).Address);
                }
                broadcast(((IPEndPoint)k.Client.RemoteEndPoint).Address + ":" + ((IPEndPoint)k.Client.RemoteEndPoint).Port + "> " + "User disconnected");
                klienter.Remove(k);
            }
        }
        private async void broadcast(string msg)
        {
            foreach(TcpClient client in klienter)
            {
                startTest(client, msg);
            }
        }
        private async void startTest(TcpClient c, string msg)
        {
            byte[] utData = Encoding.Unicode.GetBytes(msg);

            try
            {
                await c.GetStream().WriteAsync(utData, 0, utData.Length);
            }
            catch (Exception error) { MessageBox.Show("stream" + error.Message, Text); }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stänger = true;
            broadcast("/CLOSE CONNECTION");
            Application.Exit();
        }


        private void btnAnslut_Click(object sender, EventArgs e)
        {
            if(!klienten.Connected)StartaAnslutning();
            lbxLista.Items.Add("Knapp klar");
        }
        /// <summary>
        /// Ansluter till en server
        /// </summary>
        public async void StartaAnslutning()
        {
            try
            {   
                
                IPAddress adress = IPAddress.Parse(tbxAnslut.Text);
                await klienten.ConnectAsync(adress, port);
            }
            catch(Exception error) { lbxLista.Items.Add(error.Message);return; }
            if (klienten.Connected)
            {
                btnAnslut.Enabled = false;
                btnSend.Enabled = true;
                ansluten = true;
                lyssnaKlient(klienten);
            }
        }

        private async void lyssnaKlient(TcpClient c)
        {
            Debug.WriteLine(c.Connected.ToString() + " Read");
            if(ansluten == true)
            {
                byte[] buffer = new byte[1028];
                int l = 0;
                try
                {
                    if(ansluten == true)
                    {
                        l = await c.GetStream().ReadAsync(buffer, 0, buffer.Length);
                    }
                }
                catch(Exception error) { lbxLista.Items.Add(error.Message); }

                string msg = Encoding.Unicode.GetString(buffer, 0, l);
                Debug.WriteLine(msg);
                if(msg != "/CLOSE CONNECTION")
                {
                    if(ansluten == true)
                    {
                        lbxInput.Items.Add(msg);
                    }
                    lyssnaKlient(c);
                }
                else
                {
                    klienten.Close();
                    ansluten = false;
                    klienten = new TcpClient();
                    if(ansluten == true)
                    {
                        lbxLista.Items.Add("Error");
                    }
                    btnAnslut.Enabled = true;
                    btnSend.Enabled = false;
                }
            }

        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            StartaSändning(tbxSend.Text);
        }
        /// <summary>
        /// Skickar data till servern.
        /// </summary>
        public async void StartaSändning(string message)
        {
            byte[] utdata = Encoding.Unicode.GetBytes(message);
            try
            {
                await klienten.GetStream().WriteAsync(utdata, 0, utdata.Length);
            }
            catch (Exception error) { lbxLista.Items.Add(error.Message); return; }
            lbxLista.Items.Add("Sändning klar");
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            ansluten = false;
            StartaSändning("/CLOSE CONNECTION");
        }
    }
}
