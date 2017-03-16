using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AntiTroll
{
    public partial class Form1 : Form
    {
        ROMManager rm;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISet<int> trollObjects = new HashSet<int>();
            var rows = dataGridView1.Rows;
            foreach (DataGridViewRow row in rows)
            {
                bool detroll = (bool)row.Cells[0].Value;
                if (detroll)
                {
                    string intNumber = (string)row.Cells[1].Value;
                    int objectNumber = Int32.Parse(intNumber);
                    trollObjects.Add(objectNumber);
                }
            }
            rm.DetrollLevels(trollObjects, starRotateCheckBox.Checked, showSecretsCheckBox.Checked);
            MessageBox.Show("Your ROM is detrolled", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "ROM Files (*.z64)|*.z64";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rm = new ROMManager(openFileDialog.FileName, richTextBox, "behaviour.txt");
                    var boxObjects = rm.ReadBoxBehaviours();
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < 0x63; i++ )
                    {
                        var item = boxObjects[i];
                        if (item == null) continue;
                        string description;
                        rm.behavioursDescription.TryGetValue(item.Behaviour, out description);
                        if (description != null)
                        {
                            dataGridView1.Rows.Add(new object[] { i >= 0xF, i.ToString(), description, item.BParam1.ToString("X2") + item.BParam2.ToString("X2") });
                        }
                        else
                        {
                            dataGridView1.Rows.Add(new object[] { i >= 0xF, i.ToString(), item.Behaviour.ToString("X8"), item.BParam1.ToString("X2") + item.BParam2.ToString("X2") });
                        }
                    }
                    MessageBox.Show("Your ROM was loaded!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (IOException)
                {
                    MessageBox.Show("Failed to load!", "-_-", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
