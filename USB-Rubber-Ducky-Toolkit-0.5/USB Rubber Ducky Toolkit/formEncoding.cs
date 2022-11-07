﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace USB_Rubber_Ducky_Toolkit
{
    public partial class formEncoding : Form
    {
        public formEncoding()
        {
            InitializeComponent();
        }
        //Varibles
        string FilePath = "";
        string outPutFilePath = "";
        string outputName = "inject.bin";
        //BUTTONS
        private void btnOutputButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath = folderBrowserDialog1.SelectedPath;
                Console.WriteLine(FilePath);
                btnEncode.Enabled = true;
            }
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            EncodeToBin();
        }
        //END OF BUTTONS
        private void EncodeToBin()
        {
            string outputfilename = txtboxFileName.Text;
            //start cmd and run java file passing duckyscript to it
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            outputName = txtboxFileName.Text;
            Console.WriteLine(FilePath + outputName);
            outPutFilePath = Path.Combine(FilePath, outputName);
            cmd.StandardInput.WriteLine("java -jar duckencode.jar -i \"" + "script.txt" + "\" -o \""+ outPutFilePath + "\"");
            
            
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            Console.Out.WriteLine("Output: {cmd.StandardOutput.ReadToEnd()}");
            Console.Out.WriteLine("Error: {cmd.StandardError.ReadToEnd()}");
            Console.WriteLine(outPutFilePath);
            MessageBox.Show(File.Exists(outPutFilePath) ? "Bin file created sucessfully." : "Error creating file. Possible file permissions problem. Try running program in admin privleges");
            outPutFilePath = "";
        }
        

        private void lblPath_Click(object sender, EventArgs e)
        {

        }
    }
}
