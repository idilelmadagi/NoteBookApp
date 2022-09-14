using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;

namespace NoteBook
{
    public partial class Alarm : Form
    {
        string _filePath;
        public string alarmTime;
        public int _index;

        public event EventHandler onRefresh;
        public Alarm(string file)
        {
            InitializeComponent();
            _filePath = file;
        }

        public void RefreshData()
        {

            this.onRefresh(this, null);
        }
        private void Alar_btn_Click(object sender, EventArgs e)
        {
            alarmTime = dateTimePicker1.Value.ToString("dd.MM.yyyy");

            alarmTime += " " + saat_txt.Value.ToString("00");
            alarmTime += ":" + dakika_txt.Value.ToString("00");

            XDocument x = XDocument.Parse(System.IO.File.ReadAllText(_filePath));

            var item = x.Descendants("AlarmItem").ToList();

            ((XElement)item[_index]).SetAttributeValue("AlarmDateTime", alarmTime);

            x.Save(_filePath);
            RefreshData();

            this.Close();

        }

    }
}
