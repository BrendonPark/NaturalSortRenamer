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

namespace NaturalSortRenamer
{
    public partial class MainForm : Form
    {
        // Dictates the length of the number for the output. e.g. 4 = 0001
        private readonly int NUMBER_LENGTH = 4;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            // Background Worker only takes one argument
            RenameJob job = new RenameJob();
            job.Directory = txtDirectory.Text;
            job.NewName = txtNewName.Text;

            if (!Directory.Exists(job.Directory))
                MessageBox.Show("Invalid directory");
            else
            {
                btnProcess.Enabled = false;

                // Process the rename on a background thread
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += DoWork;
                worker.RunWorkerCompleted += RunWorkerCompleted;
                worker.RunWorkerAsync(job);
            }
        }

        private void DoWork (object sender, DoWorkEventArgs e)
        {
            RenameJob job = (RenameJob)e.Argument;

            List<string> files = Directory.GetFiles(txtDirectory.Text).ToList();

            FileComparer fc = new FileComparer();
            files.Sort(fc);

            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                string number = i.ToString().PadLeft(NUMBER_LENGTH, '0');
                string name = job.NewName;
                string path = Path.GetDirectoryName(file);

                // Construct the new path
                string newFile = path + Path.DirectorySeparatorChar + name + number + Path.GetExtension(file);

                File.Move(file, newFile);
            }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnProcess.Enabled = true;
            MessageBox.Show("Done!");
        }

    }
}
