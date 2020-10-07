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
            catch (Exception error) { lbxLista.Items.Add(error.Message + "Hejsan numme 1"); return; }
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
            if (stänger == false)
            {
                byte[] buffer = new byte[1028];
                int l = 0;
                try
                {
                    l = await k.GetStream().ReadAsync(buffer, 0, buffer.Length);
                }
                catch (Exception error) { lbxLista.Items.Add(error.Message); }
                string msg = Encoding.Unicode.GetString(buffer, 0, l);
                if (msg != "/CLOSE CONNECTION")
                {
                    Debug.WriteLine(((IPEndPoint)k.Client.RemoteEndPoint).Address + ":" + ((IPEndPoint)k.Client.RemoteEndPoint).Port + "> \"" + msg + "\"" + "\r\n");
                    if (stänger == false)
                    {
                        lbxLista.Items.Add(((IPEndPoint)k.Client.RemoteEndPoint).Address + ":" + ((IPEndPoint)k.Client.RemoteEndPoint).Port + "> \"" + msg + "\"" + "\r\n");
                    }
                }
                broadcast(((IPEndPoint)k.Client.RemoteEndPoint).Address + ":" + ((IPEndPoint)k.Client.RemoteEndPoint).Port + "> \"" + msg + "\"" + "\r\n");
                StartaLäsning(k);
            }
            else
            {
                if (stänger == false)
                {
                    lbxLista.Items.Add(((IPEndPoint)k.Client.RemoteEndPoint).Address);
                }
                broadcast(((IPEndPoint)k.Client.RemoteEndPoint).Address + ":" + ((IPEndPoint)k.Client.RemoteEndPoint).Port + "> " + "User disconnected");
                klienter.Remove(k);
            }
        }
        private async void broadcast(string msg)
        {
            foreach (TcpClient client in klienter)
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
            catch (Exception error) { lbxInput.Items.Add("stream" + error.Message); }
        }


        private void disconnect(){
            if(ansluten == true)
            {
                ansluten = false;
                startaSändning("/CLOSE CONNECTION");
                klienten.Close();
            }
            else
            {

                stänger = true;
                broadcast("/CLOSE CONNECTION");
                lyssanre.Stop();
            }


        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("EXIT?!?!??!", "Exit Program", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else if (result == DialogResult.Yes)
            {
                disconnect();
            }
        }
        //---------------------------------------------------------------


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
            //Debug.WriteLine(client.Connected.ToString());
            Debug.WriteLine(c.Connected.ToString() + " Read");

            if (ansluten == true)
            {
                byte[] buffer = new byte[1028];
                int n = 0;
                try
                {
                    if (ansluten == true)
                    {
                        n = await c.GetStream().ReadAsync(buffer, 0, buffer.Length);
                    }
                }
                catch (Exception error) { MessageBox.Show("read" + error.Message, Text); }

                string msg = Encoding.Unicode.GetString(buffer, 0, n);
                Debug.WriteLine(msg);
                if (msg != "/CLOSE CONNECTION")
                {
                    if (ansluten == true)
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
                    if (ansluten == true)
                    {
                        lbxInput.Items.Add("Lost connection to server" + "\r\n");
                    }
                    btnAnslut.Enabled = true;
                    btnSend.Enabled = false;
                }
            }
        }
        private async void startaSändning(string msg)
        {
            Debug.WriteLine(klienten.Connected.ToString() + " stream");

            if (klienten.Connected)
            {
                byte[] utData = Encoding.Unicode.GetBytes(msg);
                try
                {
                    await klienten.GetStream().WriteAsync(utData, 0, utData.Length);
                }
                catch (Exception error) { lbxLista.Items.Add("stream" + error.Message); }
                if (msg == "/CLOSE CONNECTION")
                {
                    utData = Encoding.Unicode.GetBytes("/CLOSE CONNECTION");
                    try
                    {
                        await klienten.GetStream().WriteAsync(utData, 0, utData.Length);
                    }
                    catch(Exception error) { lbxLista.Items.Add("stream" + error.Message);}
                    Debug.WriteLine("closing");
                    ansluten = false;
                    klienten.Close();
                    klienten = new TcpClient();
                    btnAnslut.Enabled = true;
                    btnSend.Enabled = false;
                }
            }


        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            startaSändning(tbxSend.Text);
        }

        private void btndisconnect_Click(object sender, EventArgs e)
        {
            disconnect();
        }

        /// <summary>
        /// Skickar data till servern.
        /// </summary>



    }
}
