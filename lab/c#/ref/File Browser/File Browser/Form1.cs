using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace File_Browser
{
    public partial class Form1 : Form
    {
        private string currentFolderPath;
       private string sampledriveletter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rbBrowse.Checked = true;
            LoadDrive();
            label10.Visible =false;
            textBoxInput.Visible = false;

        }

        protected void LoadDrive()
        {
           DirectoryInfo driveletter;
           foreach (char c in sampledriveletter)
            {
                string drive = c + ":\\";
                driveletter = new DirectoryInfo(drive);
                if (driveletter.Exists == true)
                {
                   DriveCombo.Items.Add(driveletter.FullName);
                }
            }
        }
        protected void ClearAllFields()
        {
            listBoxFolders.Items.Clear();
            listBoxFiles.Items.Clear();
            textBoxFolder.Text = "";
            textBoxFileName.Text = "";
            textBoxCreationTime.Text = "";
            textBoxLastAccessTime.Text = "";
            textBoxLastWriteTime.Text = "";
            textBoxFileSize.Text = "";
        }

        protected void DisplayFileInfo(string fileFullName)
        {
            FileInfo theFile = new FileInfo(fileFullName);
            if (!theFile.Exists)
            {
                throw new FileNotFoundException("File not found: " + fileFullName);
            }
            textBoxFileName.Text = theFile.Name;
            textBoxCreationTime.Text = theFile.CreationTime.ToLongTimeString();
            textBoxLastAccessTime.Text = theFile.LastAccessTime.ToLongDateString();
            textBoxLastWriteTime.Text = theFile.LastWriteTime.ToLongDateString();
            textBoxFileSize.Text = theFile.Length.ToString() + " bytes";
        }
        protected void DisplayFolderList(string folderFullName)
        {
            DirectoryInfo theFolder = new DirectoryInfo(folderFullName);
            if (!theFolder.Exists)
            {
                throw new DirectoryNotFoundException("Folder not found: " + folderFullName);
            }
            ClearAllFields();
            textBoxFolder.Text = theFolder.FullName;
            currentFolderPath = theFolder.FullName;
            // list all subfolders in folder
            foreach (DirectoryInfo nextFolder in theFolder.GetDirectories())
                listBoxFolders.Items.Add(nextFolder.Name);
            // list all files in folder
            foreach (FileInfo nextFile in theFolder.GetFiles())
                listBoxFiles.Items.Add(nextFile.Name);
        }

        private void buttonDisplay_Click(object sender, EventArgs e)
        {
            if (rbSearch.Checked == true)
            {
                DirectoryInfo driveletter;
                foreach (char c in sampledriveletter)
                {
                    string drive = c + ":\\";
                    driveletter = new DirectoryInfo(drive);
                    if (driveletter.Exists == true)
                    {
                        DirectoryInfo Directoryinf = new DirectoryInfo(drive);
                        DirectoryInfo[] Dirs = Directoryinf.GetDirectories();
                        //string[] res;

                        string temp1 = "System Volume Information";
                        //string aa = "c"+ ":\";
                        for (int i = 0; i < Dirs.Length; i++)
                        {
                            if (Convert.ToBoolean(Dirs[i].Name.Equals(temp1))) //|| Directoryinf.Name.Equals(aa)))
                            {
                                continue;
                            }
                            object[] temp = Dirs[i].GetFiles();

                            for (int j = 0; j < temp.Length; j++)
                            {
                                if (Convert.ToString(temp[j]).EndsWith(textBoxInput.Text))
                                {
                                    listBoxFiles.Items.Add(Convert.ToString(temp[j]));
                                    //FileInfo currentFile = new FileInfo(temp[j].ToString());
                                    //String path = currentFile.FullName;
                                    //listBox2.Items.Add(path);
                                }

                            }

                        }
                    }

                }
            }
            else
            {
                try
                {
                    string folderPath = DriveCombo.SelectedItem.ToString();
                    DirectoryInfo theFolder = new DirectoryInfo(folderPath);
                    if (theFolder.Exists)
                    {
                        DisplayFolderList(theFolder.FullName);
                        return;
                    }
                    FileInfo theFile = new FileInfo(folderPath);
                    if (theFile.Exists)
                    {
                        DisplayFolderList(theFile.Directory.FullName);
                        int index = listBoxFiles.Items.IndexOf(theFile.Name);
                        listBoxFiles.SetSelected(index, true);
                        return;
                    }
                    throw new FileNotFoundException("There is no file or folder with "
                    + "this name: " + textBoxInput.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedString = listBoxFiles.SelectedItem.ToString();
                string fullFileName = Path.Combine(currentFolderPath, selectedString);
                DisplayFileInfo(fullFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedString = listBoxFolders.SelectedItem.ToString();
                string fullPathName = Path.Combine(currentFolderPath, selectedString);
                DisplayFolderList(fullPathName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = new FileInfo(currentFolderPath).DirectoryName;
                DisplayFolderList(folderPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void rbSearch_CheckedChanged(object sender, EventArgs e)
        {
            label10.Visible = true;
            textBoxInput.Visible = true;
            DriveCombo.Enabled = false;
        }
    }
}