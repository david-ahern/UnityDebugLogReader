using System;
using System.Collections;
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
        bool OtherThreadStopped = true;

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
            DirectoryLabel.Text = dir;

            fDialog = new OpenFileDialog();
            fDialog.Title = "Open Stream";
            fDialog.Filter = "txt files (*.txt)|*.txt|All files|*.*";
            fDialog.InitialDirectory = Directory.GetCurrentDirectory();
            fDialog.FileOk += FileDirectorySelected;

            timer = new System.Windows.Forms.Timer();
            timer.Tick += timer_Tick;
            timer.Interval = 333;
            nDots = 0;

            StatusLabel.Text = "Stopped";
            StatusLabel.ForeColor = Color.Red;

            CloseStreamButton.Enabled = false;
            LoadAllButton.Enabled = false;
        }

        private void timer_Tick(object sender, EventArgs e)
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


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reset(true);
        }

        private void CloseStreamButton_Click(object sender, EventArgs e)
        {
            RequestStop();
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
                _streamStopped = false;
                OtherThreadStopped = false;
                CheckLogThread = new Thread(CheckDebugLogFile);
                CheckLogThread.Start();

                OutputLogThread = new Thread(PrintOutputToWindow);
                OutputLogThread.Start();

                CloseStreamButton.Enabled = true;
                OpenStreamButton.Enabled = false;
                LoadAllButton.Enabled = true;
                timer.Start();
            }
        }

        private void CheckDebugLogFile()
        {
            Console.WriteLine("CheckDebugLogFileThread ID: " + CheckLogThread.ManagedThreadId);
            long prevSize = 0;
            Console.WriteLine("Starting CheckDebugLogFile");

            while (!_streamStopped && !OtherThreadStopped)
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
                            if (stringArray[i].Length > 0 && (byte)stringArray[i][0] != 13)
                                OutputLines.Add(stringArray[i]);
                            else if (i < stringArray.Length - 1)
                                stringArray[i + 1] = "\n" + stringArray[i + 1];
                        }
                    }

                    prevSize = Log.Length;
                }
                else if (prevSize > Log.Length)
                {
                    lock (locker)
                    {
                        Log.Position = 0;
                        OutputLines.Clear();
                        MethodInvoker mi = delegate() { OutputTextbox.Text = ""; OutputTextbox.ScrollToCaret(); };
                        this.Invoke(mi);
                        NumLinesOutput = 0;
                        prevSize = 0;
                    }
                }

                Thread.Yield();
            }

            if (OtherThreadStopped)
                Reset(false);
            else
                OtherThreadStopped = true;
        }

        private void PrintOutputToWindow()
        {
            while (!_streamStopped && !OtherThreadStopped)
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
            if (OtherThreadStopped)
                Reset(false);
            else
                OtherThreadStopped = true;
        }

        private void RequestStop()
        {
            _streamStopped = true;

            DirectoryLabel.Text = "";
            
            timer.Stop();
            StatusLabel.Text = "Stopped";
            StatusLabel.ForeColor = Color.Red;

            CloseStreamButton.Enabled = false;
            LoadAllButton.Enabled = false;
            OpenStreamButton.Enabled = true;


        }

        private void Reset(bool clearText)
        {
            Console.WriteLine("Reset");
            _streamStopped = true;

            if (clearText)
            {
                OutputTextbox.Text = "";
                NumLinesOutput = 0;
                OutputLines.Clear();
            }
            if (Log != null)
                Log.Close();

            if (CheckLogThread != null && CheckLogThread.IsAlive)
                CheckLogThread.Abort();
            if (OutputLogThread != null && OutputLogThread.IsAlive)
                OutputLogThread.Abort();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            OutputTextbox.Text = "";
        }

        private void LoadAllButton_Click(object sender, EventArgs e)
        {
            OutputTextbox.Text = "";
            NumLinesOutput = 0;
            Log.Position = 0;
        }


    }
}
