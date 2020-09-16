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


namespace kahoot
{
    public partial class Form1 : Form
    {
        TcpListener lyssanre;

        TcpClient klienten = new TcpClient();

        List<TcpClient> klient = new List<TcpClient>();

        List<Klienter> allaKlienter = new List<Klienter>();
        /// <summary>
        /// Porten till servern
        /// </summary>
        int port = 12345;
        /// <summary>
        /// Klienten som gengår server process just nu
        /// </summary>
        int nuvarande = 0;

      
        public Form1()
        {
            InitializeComponent();
            klienten.NoDelay = true;
            //klient[0].NoDelay = true;
        }

        
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                
                lyssanre = new TcpListener(IPAddress.Any, port);
                lyssanre.Start();
            }
            catch(Exception error) { lbxLista.Items.Add(error.Message + "Hejsan numme 1" );return; }

            btnStart.Enabled = false;
            lbxLista.Items.Add("Knapp klar");
            StartaMottagning();
        }
        /// <summary>
        /// Tar emot ny klient
        /// </summary>
        public async void StartaMottagning()
        {
            try
            {
                //klient[nuvarande] = new TcpClient();
                //allaKlienter.Add(klient[nuvarande]);
                klienten = await lyssanre.AcceptTcpClientAsync();
                //allaKlienter.Add();
            }
            catch (Exception error) { lbxLista.Items.Add(error.Message + "Hejsan numme 2"); return; }
            lbxLista.Items.Add("Mottagning klar");
            lbxLista.Items.Add(((IPEndPoint)klienten.Client.RemoteEndPoint).Port.ToString());
            StartaLäsning(klienten);
        }
        /// <summary>
        /// Läser in datan som klienter skickar in
        /// </summary>
        public async void StartaLäsning(TcpClient k)
        {
            lbxLista.Items.Add("Läsning påbörjad");
            byte[] buffert = new byte[1024];
            int n = 0;
            try
            {
                n = await k.GetStream().ReadAsync(buffert, 0, buffert.Length);
            }
            catch(Exception error) { lbxLista.Items.Add(error.Message + "Hejsan numme 3");return;}
            lbxLista.Items.Add("Port: " + ((IPEndPoint)klienten.Client.RemoteEndPoint).Port.ToString() + " Adress: "+ ((IPEndPoint)klienten.Client.RemoteEndPoint).Address.ToString());
            lbxInput.Items.Add(Encoding.Unicode.GetString(buffert, 0, n));
            lbxLista.Items.Add("Läsning klar");
            StartaLäsning(k);
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
            lbxLista.Items.Add("Anslutning klar");
            btnSend.Enabled = true;
            btnAnslut.Enabled = false;

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
    }
}
