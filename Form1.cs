using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public struct socnames
    {
        public Socket socket;
        public string username;

    }
    public partial class Form1 : Form
    {


        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> Onlines = new List<string>();
        bool terminating = false;
        bool listening = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;


                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();
                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }









        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Byte[] bufferrequest = new byte[128];
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    Byte[] sendingfriends = new byte[128];
                    Byte[] sendingreject = new byte[128];
                    sendingfriends = Encoding.Default.GetBytes(txt_friend.Text);
                    bufferrequest = Encoding.Default.GetBytes(txt_Request.Text);
                    sendingreject = Encoding.Default.GetBytes(ss.Text);
                    Byte[] initializereq = Encoding.Default.GetBytes("-->");
                    try
                    {
                        foreach (Socket client in clientSockets)
                        {
                            
                            if (txt_Request.Text != "")
                            {
                                client.Send(bufferrequest);
                                
                            }
                            else
                                client.Send(initializereq);
                        }
                    }
                    catch
                    {
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;

                        textBox_port.Enabled = true;
                        button_listen.Enabled = true;
                        serverSocket.Close();
                    }
                    Thread.Sleep(100);
                    try
                    {
                        foreach (Socket client in clientSockets)
                        {
                            client.Send(sendingfriends);
                        }
                    }
                    catch
                    {
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;

                        textBox_port.Enabled = true;
                        button_listen.Enabled = true;
                        serverSocket.Close();
                    }
                    try
                    {
                        foreach (Socket client in clientSockets)
                        {
                            client.Send(sendingreject);
                        }
                    }
                    catch
                    {
                        logs.AppendText("There is a problem! Check the connection...\n");
                        terminating = true;

                        textBox_port.Enabled = true;
                        button_listen.Enabled = true;
                        serverSocket.Close();
                    }
                    Thread.Sleep(100);
                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                }
                   
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }
        string username;
        private void Receive()
        {

            socnames thisClient = new socnames();

            thisClient.socket = clientSockets[clientSockets.Count() - 1];



            bool connected = false;
            string line;
            bool indb = false;
            bool added = false;
            try
            {
                Byte[] buffer = new Byte[128];
                thisClient.socket.Receive(buffer);


                username = Encoding.Default.GetString(buffer);
                username = username.Substring(0, username.IndexOf("\0"));
                StreamReader sr = new StreamReader("user_db.txt");


                line = sr.ReadLine();
                while (line != null)
                {

                    if (line == username)
                    {
                        indb = true;
                        if (!Onlines.Contains(username))
                        {
                            added = true;
                            Onlines.Add(username);
                            thisClient.username = username;
                            connected = true;
                        }
                    }


                    line = sr.ReadLine();


                }
            }
            catch
            {
                if (!terminating)
                {

                    thisClient.socket.Close();
                    clientSockets.Remove(thisClient.socket);
                    connected = false;
                }
            }
            if (connected == false)
            {
                string disconnect = "";
                if (!indb)
                {
                    logs.AppendText(username + " is not in database, disconnecting\n");
                    disconnect = "You are not in database, disconnecting";
                }
                else if (indb && !added)
                {
                    logs.AppendText(username + " is already connected\n");
                    disconnect = "You are already connected";
                }
                Byte[] buffer = Encoding.Default.GetBytes(disconnect);
                thisClient.socket.Send(buffer);
                if (!indb)
                {

                    thisClient.socket.Close();
                    clientSockets.Remove(thisClient.socket);
                    if (Onlines.Contains(username))
                    {
                        Onlines.Remove(thisClient.username);
                    }
                }
            }
            else
            {
                logs.AppendText(username + " logged succesfully\n");

            }

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    thisClient.socket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (incomingMessage.Contains("Add Friend"))
                    {
                        incomingMessage = incomingMessage.Substring(0, incomingMessage.Length - 10);
                        if (incomingMessage == thisClient.username)
                        {
                            Byte[] collisionreject = Encoding.Default.GetBytes("You cannot add yourself");
                            thisClient.socket.Send(collisionreject);
                        }
                        else
                        {
                            StreamReader reader = new StreamReader("user_db.txt");
                            Byte[] bufferrequest = new byte[128];

                            string lines = reader.ReadLine();
                            int counter = 0;
                            while (lines != null)
                            {
                                if (incomingMessage == lines)
                                {
                                    counter++;
                                    if (!txt_Request.Text.Contains(thisClient.username + "-->" + incomingMessage)&& !txt_Request.Text.Contains(incomingMessage + "-->" + thisClient.username)&& !txt_friend.Text.Contains(thisClient.username + "%" + incomingMessage) && !txt_friend.Text.Contains(incomingMessage + "%" + thisClient.username))
                                    {
                                        txt_Request.AppendText(thisClient.username + "-->" + incomingMessage + "\n");

                                        bufferrequest = Encoding.Default.GetBytes(txt_Request.Text);
                                        Byte[] initializereq = Encoding.Default.GetBytes("-->");
                                        try
                                        {
                                            foreach (Socket client in clientSockets)
                                            {
                                                if (txt_Request.Text != "")
                                                    client.Send(bufferrequest);
                                                else
                                                    client.Send(initializereq);
                                            }
                                            Byte[] invite = Encoding.Default.GetBytes("You invited " + incomingMessage);
                                            thisClient.socket.Send(invite);
                                        }
                                        catch
                                        {
                                            logs.AppendText("There is a problem! Check the connection...\n");
                                            terminating = true;

                                            textBox_port.Enabled = true;
                                            button_listen.Enabled = true;
                                            serverSocket.Close();
                                        }


                                    }
                                    else if (txt_Request.Text.Contains(incomingMessage + "-->" + thisClient.username))
                                    {
                                        buffer = Encoding.Default.GetBytes("You already invited from " + incomingMessage + "\n");
                                        thisClient.socket.Send(buffer);
                                    }
                                    else if (txt_friend.Text.Contains(thisClient.username + "%" + incomingMessage) || txt_friend.Text.Contains(incomingMessage + "%" + thisClient.username))
                                    {
                                        buffer = Encoding.Default.GetBytes("You already friend with " + incomingMessage + "\n");
                                        thisClient.socket.Send(buffer);
                                    }
                                    else
                                    {
                                        buffer = Encoding.Default.GetBytes("You already invited " + incomingMessage + "\n");
                                        thisClient.socket.Send(buffer);
                                    }
                                }
                                lines = reader.ReadLine();
                            }
                            if (counter == 0)
                            {
                                buffer = Encoding.Default.GetBytes("Not user such that exist!");
                                thisClient.socket.Send(buffer);

                            }
                        }

                    }
                    else if (incomingMessage.Contains("++"))
                    {

                        string friendship = incomingMessage.Substring(0, incomingMessage.Length - 2) + "-->" + thisClient.username;
                        logs.AppendText("New Friendship created: " + thisClient.username + "-" + incomingMessage.Substring(0, incomingMessage.Length - 2) + "\n");
                        txt_Request.Text = txt_Request.Text.Replace(friendship, "");
                        txt_friend.AppendText(thisClient.username + "%" + incomingMessage.Substring(0, incomingMessage.Length - 2) + "\n");

                    }
                    else if (incomingMessage.Contains("--"))
                    {

                        string friendship = incomingMessage.Substring(0, incomingMessage.Length - 2) + "-->" + thisClient.username;
                        logs.AppendText(thisClient.username + " Rejected to " + incomingMessage.Substring(0, incomingMessage.Length - 2) + "\n");
                        txt_Request.Text = txt_Request.Text.Replace(friendship, "");
                        ss.AppendText(thisClient.username + "$" + incomingMessage.Substring(0, incomingMessage.Length - 2) + "\n");

                    }
                    else if (incomingMessage.Contains("~"))
                    {
                        logs.AppendText(thisClient.username + " Removes friendship with " + incomingMessage.Substring(0, (incomingMessage.Length - 1))+ "\n");
                        rich_Remove.AppendText(thisClient.username + " REMOVES " + incomingMessage.Substring(0, (incomingMessage.Length - 1))+"\n");
                        string[] liness = Regex.Split(txt_friend.Text, "\n");
                        txt_friend.Clear();
                        foreach (String l in liness)
                        {
                            if (l.Contains(thisClient.username) && l.Contains(incomingMessage.Substring(0, (incomingMessage.Length - 1))))
                            {

                            }
                            else {
                                txt_friend.AppendText(l + "\n");
                            }

                        }

                    }
                    else if (incomingMessage == "Request")
                    {
                        Byte[] sendfriends = new byte[128];
                        string accumulate = "";
                        string[] liness = Regex.Split(txt_friend.Text, "\n");
                        foreach (String l in liness)
                        {
                            if (l.Contains(thisClient.username))
                            {
                                accumulate += l.Replace("%", "**") + "\n";
                            }
                        
                        }

                        sendfriends = Encoding.Default.GetBytes(accumulate);
                        thisClient.socket.Send(sendfriends);
                    }
                    else if (incomingMessage.Contains("To my friends"))
                    {
                        Byte[] sendfriends = new byte[128];
                        string accumulate = "";
                        string[] liness = Regex.Split(txt_friend.Text, "\n");
                        foreach (String l in liness)
                        {
                            if (l.Contains(thisClient.username))
                            {
                                string hold;
                                hold = l.Replace(thisClient.username, "");
                                hold = hold.Replace("%", "Getit") + "\n";
                                accumulate += hold;
                            }

                        }

                        sendfriends = Encoding.Default.GetBytes(incomingMessage + "\n" + accumulate);
                        
                        
                        
                        bool sendrepeat = true;

                        Task f = Task.Factory.StartNew(() =>
                        {
                            int counter = 0;
                            while (sendrepeat)
                            {
                                counter++;
                                foreach (Socket client in clientSockets)
                                {
                                   
                                    try
                                    {
                                        if (thisClient.socket != client)
                                        {
                                            client.Send(sendfriends);
                                            
                                        }
                                    }
                                    catch
                                    {
                                        //logs.AppendText("There is a problem! Check the connection...\n");
                                        //terminating = true;

                                        //textBox_port.Enabled = true;
                                       // button_listen.Enabled = true;
                                       // serverSocket.Close();
                                    }
                                }
                                if (counter == 50)
                                {
                                    sendrepeat = false;
                                }
                                Thread.Sleep(1000);
                               // Application.DoEvents(); 
                            }
                        });


                    }
                    else
                    {
                        logs.AppendText(incomingMessage + "\n");
                        foreach (Socket client in clientSockets)
                        {
                            try
                            {
                                if (thisClient.socket != client)
                                {
                                    client.Send(buffer);
                                }
                            }
                            catch
                            {
                                logs.AppendText("There is a problem! Check the connection...\n");
                                terminating = true;

                                textBox_port.Enabled = true;
                                button_listen.Enabled = true;
                                serverSocket.Close();
                            }


                        }




                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText(thisClient.username + " has disconnected\n");
                    }
                    if (Onlines.Contains(thisClient.username))
                    {
                        Onlines.Remove(thisClient.username);
                    }
                    thisClient.socket.Close();
                    clientSockets.Remove(thisClient.socket);
                    connected = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txt_friend_TextChanged(object sender, EventArgs e)
        {
            Byte[] sendingfriends = new byte[128];
            sendingfriends = Encoding.Default.GetBytes(txt_friend.Text);
            foreach (Socket client in clientSockets)
            {
                try
                {

                    client.Send(sendingfriends);

                }
                catch
                {
                    logs.AppendText("There is a problem! Check the connection...\n");
                    terminating = true;

                    textBox_port.Enabled = true;
                    button_listen.Enabled = true;
                    serverSocket.Close();
                }



            }
        }

        private void ss_TextChanged(object sender, EventArgs e)
        {
            Byte[] sendingfriends = new byte[128];
            sendingfriends = Encoding.Default.GetBytes(ss.Text);
            foreach (Socket client in clientSockets)
            {
                try
                {

                    client.Send(sendingfriends);

                }
                catch
                {
                    logs.AppendText("There is a problem! Check the connection...\n");
                    terminating = true;

                    textBox_port.Enabled = true;
                    button_listen.Enabled = true;
                    serverSocket.Close();
                }



            }
        }

       private void label5_Click(object sender, EventArgs e)
        { 

        }
    }
}
