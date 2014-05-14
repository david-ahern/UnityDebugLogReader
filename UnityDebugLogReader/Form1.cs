using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Timers;

namespace UnityDebugLogReader
{
    public partial class Form1 : Form
    {
        Thread CheckLogThread;
        Thread OutputLogThread;
        readonly object locker = new object();

        FileStream Log;

        private List<string> OutputLines;
        int NumLinesOutput = 0;

        OpenFileDialog fDialog;
        string dir;

        bool _streamStopped = true;

        System.Windows.Forms.Timer timer;
        int nDots;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OutputLines = new List<string>();

            dir = "";

            fDialog = new OpenFileDialog();
            fDialog.Title = "Open Stream";
            fDialog.Filter = "TextFiles|*.txt";
            fDialog.InitialDirectory = Directory.GetCurrentDirectory();
            fDialog.FileOk += FileDirectorySelected;

            timer = new System.Windows.Forms.Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 333;
            nDots = 0;
            timer.Start();

            StatusLabel.Text = "Stopped";
            StatusLabel.ForeColor = Color.Red;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!_streamStopped)
            {
                StatusLabel.ForeColor = Color.Green;
                StatusLabel.Text = "Streaming";

                for (int i = 0; i < nDots; ++i)
                    StatusLabel.Text += " .";

                if (nDots >= 3)
                    nDots = 0;
                else
                    nDots++;
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reset(true);
        }

        private void CloseStreamButton_Click(object sender, EventArgs e)
        {
            RequestStop();
            DirectoryLabel.Text = "";
        }

        private void OpenStreamButton_Click(object sender, EventArgs e)
        {
            fDialog.ShowDialog();
        }

        private void FileDirectorySelected(object sender, CancelEventArgs e)
        {
            Reset(true);
            DirectoryLabel.Text = dir = fDialog.FileName;
            try
            {
                Log = new FileStream(dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            catch (IOException ex) 
            {
                MessageBox.Show("Could not open file\n" + ex.Message);
            }

            if (Log != null)
            {
                CheckLogThread = new Thread(CheckDebugLogFile);
                CheckLogThread.Start();

                OutputLogThread = new Thread(PrintOutputToWindow);
                OutputLogThread.Start();
            }
        }

        private void CheckDebugLogFile()
        {
            long prevSize = 0;
            _streamStopped = false;

            Console.WriteLine("Starting CheckDebugLogFile");

            while (!_streamStopped)
            {
                if (prevSize < Log.Length)
                {
                    byte[] buffer = new byte[Log.Length];
                    Log.Read(buffer, 0, (int)(Log.Length - Log.Position));

                    string str = System.Text.Encoding.ASCII.GetString(buffer);

                    string[] stringArray = str.Split('\n');

                    for (int i = 0; i < stringArray.Length; ++i)
                    {
                        lock (locker)
                        {
                            OutputLines.Add(stringArray[i]);
                        }
                    }

                    prevSize = Log.Length;
                }
                else if (prevSize > Log.Length)
                {
                    lock (locker)
                    {
                        Console.WriteLine("Resetting output");
                        Log.Position = 0;
                        OutputLines.Clear();
                        MethodInvoker mi = delegate() { OutputTextbox.Text = ""; OutputTextbox.ScrollToCaret(); };
                        this.Invoke(mi);
                        NumLinesOutput = 0;
                        prevSize = 0;
                    }
                }
                string status = "Streaming";               

                Thread.Yield();
            }
        }

        private void PrintOutputToWindow()
        {
            Console.WriteLine("Start PrintOutputToWindow");

            while (!_streamStopped)
            {
                if (OutputLines.Count > NumLinesOutput)
                {
                    for (int i = NumLinesOutput; i < OutputLines.Count; i++)
                    {
                        lock (locker)
                        {
                            MethodInvoker mi = delegate() { OutputTextbox.AppendText(OutputLines[i]); OutputTextbox.ScrollToCaret(); };
                            this.Invoke(mi);
                            NumLinesOutput++;
                        }
                    }
                }
                Thread.Yield();
            }
        }

        private void RequestStop()
        {
            _streamStopped = true;
        }

        private void Reset(bool clearText)
        {
            _streamStopped = true;
            OutputLines.Clear();
            if (clearText)
                OutputTextbox.Text = "";
            NumLinesOutput = 0;
            if (Log != null)
                Log.Close();

            if (CheckLogThread != null && CheckLogThread.IsAlive)
                CheckLogThread.Abort();
            if (OutputLogThread != null && OutputLogThread.IsAlive)
                OutputLogThread.Abort();
        }


    }
}
