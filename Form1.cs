using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MicLocalizationSystem;
using System.Globalization;

namespace MicSubGeneration
{
    public partial class Form1 : Form
    {
        string Filename = "";
        string Filenamerash = "";
        string Filenameput = "";
        Title titles = new Title();

        Language lg = new Language();
        LangParam lng;
        public Form1()
        {
            InitializeComponent();
            lg = LocalizationSettings.GetLangSystem();
            CultureInfo culture = CultureInfo.CurrentCulture;
            Console.WriteLine(culture.Name);
            added();
            lang();
        }

        void lang()
        {
            lng = LocalizationSettings.LoadLangParamFromFile(lg, Environment.CurrentDirectory + "\\Localization\\");         
            button6.Text = lng.GetLangText("Load_Video");           
            button4.Text = lng.GetLangText("Save_File_Sub");
            button3.Text = lng.GetLangText("Added_Sub");
            button5.Text = lng.GetLangText("Create_Video");

            label1.Text = lng.GetLangText("Start_Time_Text");
            label2.Text = lng.GetLangText("End_Time_Text");
            label3.Text = lng.GetLangText("Text");

            toolStripStatusLabel1.Text = lng.GetLangText("Status");
        }

        private void Chengelang(object sender, EventArgs e)
        {
            string name = (sender as ToolStripMenuItem).Text;
            lg = LocalizationSettings.GetLangName(name, Environment.CurrentDirectory + "\\Localization\\");
            lang();
        }

        void added()
        {
            Language[] lng = LocalizationSettings.GetFromFolder(Environment.CurrentDirectory + "\\Localization\\");
            foreach (var item in lng)
            {
                ToolStripMenuItem tmp = new ToolStripMenuItem();
                tmp.Text = item.GetName();
                tmp.Click += Chengelang;
                языкToolStripMenuItem.DropDownItems.Add(tmp);
            }           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog opg = new OpenFileDialog();
            opg.Filter = "mp4|*.mp4|avi|*.avi|ts|*.ts|mpg|*.mpg|wmv|*.wmv";
            opg.Title = "";
            if(opg.ShowDialog() == DialogResult.OK)
            {
                Filename = opg.SafeFileName.Split('.')[0];
                Filenamerash = opg.SafeFileName;
                Filenameput = opg.FileName;
                axWindowsMediaPlayer1.URL = opg.FileName;
                toolStripStatusLabel1.Text = lng.GetLangText("Video_Uploading");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Text = Validater.TimeValid(axWindowsMediaPlayer1.Ctlcontrols.currentPositionString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            maskedTextBox2.Text = Validater.TimeValid(axWindowsMediaPlayer1.Ctlcontrols.currentPositionString);
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            svf.FileName = Filename;
            svf.Filter = "Файл субтитров (SRT)|*.srt";
            if(svf.ShowDialog() == DialogResult.OK)
            {
                titles.SaveToFile(svf.FileName);
                toolStripStatusLabel1.Text = lng.GetLangText("Save_File");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            titles.Add(Validater.TitleValid(maskedTextBox1.Text, maskedTextBox2.Text, richTextBox1.Text));
            maskedTextBox1.Text = ""; maskedTextBox2.Text = ""; richTextBox1.Text = "";
            toolStripStatusLabel1.Text = lng.GetLangText("Sub_Added");
        }

        private void exited(object sender, EventArgs e)
        {
            File.Delete(Environment.CurrentDirectory + "\\" + Filename + ".srt");
            toolStripStatusLabel1.Text = lng.GetLangText("Creating_Video");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(File.Exists(Filenameput.Split('.')[0] + ".srt") & Filename != "" & Filenamerash != "")
            {
                toolStripStatusLabel1.Text = lng.GetLangText("Creating_Video_Being");
                if(File.Exists(Environment.CurrentDirectory + "\\" + Filename + ".srt")) { File.Delete(Environment.CurrentDirectory + "\\" + Filename + ".srt"); }
                File.Copy(Filenameput.Split('.')[0] + ".srt", Environment.CurrentDirectory + "\\" + Filename + ".srt");
                Console.WriteLine("-i \"" + Filenameput + "\" -vf \"subtitles='" + Filename + ".srt" + "':force_style='Fontsize=24,PrimaryColour=&H0000ff&'\" -c:a copy \"" + Filenameput.Split('.')[0] + "AddedSub." + Filenameput.Split('.')[1] + "\"");
                Process p = Process.Start(Environment.CurrentDirectory + "\\ffmpeg.exe", "-i \""+Filenameput+"\" -vf \"subtitles='"+ Filename + ".srt" + "':force_style='Fontsize=24,PrimaryColour=&H0000ff&'\" -c:a copy \""+Filenameput.Split('.')[0] + "AddedSub." + Filenameput.Split('.')[1] + "\"");
                p.Exited += exited;                
            }
            else
            {
                MessageBox.Show(lng.GetLangText("Error1"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (File.Exists(Filenameput.Split('.')[0] + ".srt"))
            {
                Process.Start("notepad", Filenameput.Split('.')[0] + ".srt");
            }
        }

        private void языкToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
