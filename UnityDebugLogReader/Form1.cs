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

namespace UnityDebugLogReader
{
    public partial class Form1 : Form
    {
        Thread CheckLogThread;

        FileStream Log;

        private List<string> OutputLines;
        int NumLinesOutput = 0;

        OpenFileDialog fDialog;
        string dir;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OutputLines = new List<string>();

            dir = "";

            fDialog = new OpenFileDialog();
            fDialog.Title = "Open";
            fDialog.Filter = "TextFiles|*.txt";
            fDialog.InitialDirectory = Directory.GetCurrentDirectory();
            fDialog.FileOk += FileDirectorySelected;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reset();
        }

        private void CloseStreamButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void OpenStreamButton_Click(object sender, EventArgs e)
        {
            fDialog.ShowDialog();
        }

        private void FileDirectorySelected(object sender, CancelEventArgs e)
        {
            Reset();
            dir = fDialog.FileName;
            MessageBox.Show(e.ToString());
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
            }
        }

        private void CheckDebugLogFile()
        {
            long prevSize = 0;
            
            while (true)
            {
                if (prevSize < Log.Length)
                {
                    byte[] buffer = new byte[Log.Length];
                    Log.Read(buffer, 0, (int)Log.Length);

                    string str = System.Text.Encoding.ASCII.GetString(buffer);

                    string[] stringArray = str.Split('\n');

                    for (int i = 0; i < stringArray.Length; ++i)
                    {
                        OutputLines.Add(stringArray[i]);
                    }

                    PrintOutputToWindow();
                    prevSize = Log.Length;
                }
                else if (prevSize > Log.Length)
                {
                    Log.Position = 0;
                    OutputLines.Clear();
                    NumLinesOutput = 0;
                    prevSize = 0;
                }
                Thread.Yield();
            }
        }

        private void PrintOutputToWindow()
        {
            for (int i = NumLinesOutput; i < OutputLines.Count; i++)
            {
                Output.AppendText(OutputLines[i]);
                NumLinesOutput++;
            }
        }

        private void Reset()
        {
            OutputLines.Clear();
            NumLinesOutput = 0;
            if (Log != null)
                Log.Close();

            if (CheckLogThread != null && CheckLogThread.IsAlive)
                CheckLogThread.Abort();
        }


    }
}
