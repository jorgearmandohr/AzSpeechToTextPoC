using System;
using System.Text;
using System.Windows.Forms;

namespace SpeechDemo
{
    public partial class Form1 : Form
    {
        private readonly Transcript _transcriptService;
        private StringBuilder _transcriptText;
        public Form1()
        {
            InitializeComponent();
            _transcriptService = new Transcript();
        }

        //private static object lockThread = new object();
        //internal event PrintText PrintResult;
        //internal delegate void PrintText(string interpretedText, bool isLog);

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txbPath.Text = openFileDialog1.FileName;

                //PrintResult += AppendText;
                _transcriptText = _transcriptService.AzSpeechtoText(openFileDialog1.FileName).GetAwaiter().GetResult();
                rtxbTranscript.Text = _transcriptText.ToString();
            }
        }

        private void rtxbTranscript_TextChanged(object sender, EventArgs e)
        {
            string language = "en";
            if (rdbtnEs.Checked)
            {
                language = "es";
            }

            rtxbTranslate.Text = _transcriptService.AzTranslate(_transcriptText.ToString(), language).GetAwaiter().GetResult();
        }

        //private void AppendText(string interpretedText, bool isLog)
        //{
        //    if (InvokeRequired)
        //    {
        //        if (isLog)
        //        {
        //            this.Invoke(new Action(() => ltxbConsole.Items.Add(interpretedText)));
        //        }
        //        else
        //        {
        //            this.Invoke(new Action(() => rtxbTranscript.AppendText(interpretedText + Environment.NewLine)));
        //        }
        //    }
        //}
    }
}
