using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Xml;


namespace NoteBook
{
    public partial class Form1 : Form
    {

        NoteModel Model;
        string _fileName = "Notes.xml";
        string _directory = @"C:\NoteBook";
        System.Threading.Thread _alarmThread;
        string _pathXml = "";
        public string dateNow = "";

        public Form1()
        {
            InitializeComponent();

            SetFileDirectory();
            DoubleBuffered(noteGrid, true);
            DoubleBuffered(alarmGrid, true);

            noteGrid.DataSource = this.Model.Notes;
            alarmGrid.DataSource = this.Model.Alarms;
            NotegridRefresh();
            AlarmgridRefresh();

             _alarmThread = new System.Threading.Thread(new System.Threading.ThreadStart(Notify));
            _alarmThread.Start();

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            key.SetValue("NoteBook", "\"" + System.Windows.Forms.Application.ExecutablePath + "\"");

        }

        void SetFileDirectory()
        {
            if (!System.IO.Directory.Exists(_directory))
            {
                System.IO.Directory.CreateDirectory(_directory);
            }
            _pathXml = System.IO.Path.Combine(_directory, _fileName);

            if (!System.IO.File.Exists(@"C:\NoteBook\Notes.xml"))
            {
                System.IO.File.Copy(Application.StartupPath + "/Notes.xml", _pathXml, true);
                this.Model = new NoteModel();
            }

            if (System.IO.File.Exists(_pathXml))
            {

                using (var sw = new System.IO.StreamReader(_pathXml))
                {
                    this.Model = FSerializerLib.FSerializer.DeSerialize(sw.ReadToEnd(), typeof(NoteModel));


                    sw.Dispose();
                    sw.Close();
                }
            }

        }
        public void DoubleBuffered(DataGridView dgv, bool setting)
        {
            Type dgvtype = dgv.GetType();
            PropertyInfo pi = dgvtype.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
        void NotegridRefresh(){

            noteGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            noteGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            noteGrid.Columns[2].Visible = false;
        }
        void AlarmgridRefresh()
        {
            alarmGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            alarmGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            alarmGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            alarmGrid.Columns[3].Visible = false;
        }

        private void SaveNoteModel(NoteModel ntt)
        {

            noteGrid.Invoke(new MethodInvoker(delegate { noteGrid.DataSource = null; }));
            NoteModel.NoteItem nt = new NoteModel.NoteItem();
            nt.DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            nt.Note = noteTxt.Text;
            nt.Status = " ~ ";
            ntt.Notes.Add(nt);

            using (var sw = new System.IO.StreamWriter(_pathXml))
            {
                sw.Write(FSerializerLib.FSerializer.Serialize(ntt));
                sw.Close();

            }

            noteGrid.Invoke(new MethodInvoker(delegate { noteGrid.Refresh(); noteGrid.DataSource = this.Model.Notes; }));
            NotegridRefresh();
        }
        private void SaveAlarmModel(NoteModel ntt)
        {

            Alarm alar = new Alarm(_pathXml);
            alar._index = alarmGrid.Rows.GetLastRow(DataGridViewElementStates.Visible) + 1;
            alar.onRefresh += delegate { FetchModel(_pathXml); };
            alar.Show();

            alarmGrid.Invoke(new MethodInvoker(delegate { alarmGrid.DataSource = null; }));
            NoteModel.AlarmItem nt = new NoteModel.AlarmItem();
            nt.DateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            nt.Note = alarmTxt.Text;
            nt.Alarm = true;
            nt.AlarmDateTime = alar.alarmTime;
            nt.Status = " ~ ";
            ntt.Alarms.Add(nt);
          
            using (var sw = new System.IO.StreamWriter(_pathXml))
            {
                sw.Write(FSerializerLib.FSerializer.Serialize(ntt));
                sw.Close();

            }
         
            alarmGrid.Invoke(new MethodInvoker(delegate { alarmGrid.Refresh(); alarmGrid.DataSource = this.Model.Alarms; }));
            AlarmgridRefresh();
        }
        void FetchModel(string path)
        {
            try
            {
                using (var sw = new System.IO.StreamReader(path))
                {
                    this.Model = FSerializerLib.FSerializer.DeSerialize(sw.ReadToEnd(), typeof(NoteModel));


                    sw.Dispose();
                    sw.Close();

                }

                noteGrid.Invoke(new MethodInvoker(delegate
                {
                    noteGrid.DataSource = this.Model.Notes;
                }));

                alarmGrid.Invoke(new MethodInvoker(delegate
                {
                    alarmGrid.DataSource = this.Model.Alarms;

                }));

            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
        private void DeleteNoteRecord(object sender, EventArgs e)
        {

            if (MessageBox.Show(this, "Record will be permanently deleted. Do you confirm?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            XDocument x = XDocument.Parse(
                 System.IO.File.ReadAllText(_pathXml));

            foreach (DataGridViewRow _cell in noteGrid.SelectedRows)
            {
                noteGrid.Refresh();
                var deneme = x.Descendants("NoteItem").ToList();
                deneme[_cell.Index].Remove();


            }

            x.Save(_pathXml);
            FetchModel(_pathXml);
            noteGrid.Refresh();
        }
        private void SetCompleted(object sender, EventArgs e)
        {
            XDocument x = XDocument.Parse(
                System.IO.File.ReadAllText(_pathXml));

            foreach (DataGridViewRow _cell in noteGrid.SelectedRows)
            {
              
                var item = x.Descendants("NoteItem").ToList();
                ((XElement)item[_cell.Index]).SetAttributeValue("Status", " :) ");
                ((XElement)item[_cell.Index]).SetAttributeValue("Alarm", false);
         
            }
            x.Save(_pathXml);
            FetchModel(_pathXml);
            noteGrid.Refresh();
        }


        private void NoteTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                SaveNoteModel(this.Model);
                noteTxt.Clear();
            }
           
        }
        
        private void NoteTxt_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(noteTxt, "Please save your note by F2.");
            tt.ToolTipTitle = "Note";
        }

        private void NoteGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {

                var grid = sender as DataGridView;

                if (grid.Rows[e.RowIndex].Cells["Status"].Value.ToString() == " :) ")
                {
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(100, 255, 153);
                }
                else if (grid.Rows[e.RowIndex].Cells["Status"].Value.ToString() == " ~ ")
                {
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 102);
                }
   
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }

        private void SetWaiting(object sender, EventArgs e)
        {
            XDocument x = XDocument.Parse(
                 System.IO.File.ReadAllText(_pathXml));

            foreach (DataGridViewRow _cell in noteGrid.SelectedRows)
            {
             
                var item = x.Descendants("NoteItem").ToList();
                ((XElement)item[_cell.Index]).SetAttributeValue("Status", " ~ ");
       
            }
            x.Save(_pathXml);
            FetchModel(_pathXml);
            noteGrid.Refresh();
        }

        private void UpdateNote(object sender, EventArgs e)
        {
            string _answer = string.Empty;
            if (SingleQuestionn.SingleQuestionn.ShowDialog("Note:", ref _answer) == System.Windows.Forms.DialogResult.OK)
            {
                XDocument x = XDocument.Parse(
                System.IO.File.ReadAllText(_pathXml));

                foreach (DataGridViewRow _cell in noteGrid.SelectedRows)
                {

                    var item = x.Descendants("NoteItem").ToList();
                    ((XElement)item[_cell.Index]).SetAttributeValue("Note", _answer);


                }
                x.Save(_pathXml);
                FetchModel(_pathXml);
                noteGrid.Refresh();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            noteTxt.Focus();
            alarmTxt.Focus();
         
        }

        private void NoteGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == noteGrid.Columns["Note"].Index)
            {
                string note = noteGrid.Rows[e.RowIndex].Cells["Note"].Value.ToString();

                MessageBox.Show(string.Format("Note:\n\r\n{0}", note));

            }
        }

        private void AlarmGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {

                var grid = sender as DataGridView;  
                if (grid.Rows[e.RowIndex].Cells["Status"].Value.ToString() == " :) ")
                {
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(100, 255, 153); 
                }
                else if (grid.Rows[e.RowIndex].Cells["Status"].Value.ToString() == " ~ ")
                {
                    grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255,255,102); 
                }

            }
            catch (Exception m)
            {
               MessageBox.Show(m.Message);
            }
        }

        private void AlarmTxt_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(alarmTxt, "Please save your note by F2.");
            tt.ToolTipTitle = "Note";

        }

        private void AlarmTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                SaveAlarmModel(this.Model);
                alarmTxt.Clear();
            }

        }

        void Notify()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                dateNow = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

                using (var sw = new System.IO.StreamReader(_pathXml))
                {
                    this.Model = FSerializerLib.FSerializer.DeSerialize(sw.ReadToEnd(), typeof(NoteModel));

                    sw.Dispose();
                    sw.Close();

                }

                foreach (string now in Model.Alarms.Where(x => x.Alarm == true).Select(x => x.AlarmDateTime).ToList())
                {
                    if (dateNow == now)
                    {
                        List<string> alarm_list = new List<string>();
                        alarm_list.AddRange(Model.Alarms.Where(x => x.AlarmDateTime == dateNow).Select(x => x.Note).ToList());

                        foreach (string alarm in alarm_list)
                        {
                            notifyIcon1.Icon = SystemIcons.Warning;
                            notifyIcon1.ShowBalloonTip(3000, "Remember", alarm, ToolTipIcon.Warning);
                            System.Threading.Thread.Sleep(3000);
                        }
                    
                    }
                }
            }
        }

        private void DeleteAlarmRecord(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Record will be permanently deleted. Do you confirm?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            XDocument x = XDocument.Parse(System.IO.File.ReadAllText(_pathXml));

            foreach (DataGridViewRow _cell in alarmGrid.SelectedRows)
            {
                noteGrid.Refresh();
                var item = x.Descendants("AlarmItem").ToList();
                item[_cell.Index].Remove();

            }

            x.Save(_pathXml);
            FetchModel(_pathXml);
            noteGrid.Refresh();
        }

        private void SetWaitingForAlarm(object sender, EventArgs e)
        {
            XDocument x = XDocument.Parse(
               System.IO.File.ReadAllText(_pathXml));

            foreach (DataGridViewRow _cell in alarmGrid.SelectedRows)
            {

                var item = x.Descendants("AlarmItem").ToList();
                ((XElement)item[_cell.Index]).SetAttributeValue("Status", " ~ ");

            }
            x.Save(_pathXml);
            FetchModel(_pathXml);
            noteGrid.Refresh();
        }

        private void SetCompletedForAlarm(object sender, EventArgs e)
        {
            XDocument x = XDocument.Parse(System.IO.File.ReadAllText(_pathXml));

            foreach (DataGridViewRow _cell in alarmGrid.SelectedRows)
            {

                var item = x.Descendants("AlarmItem").ToList();
                ((XElement)item[_cell.Index]).SetAttributeValue("Status", " :) ");
                ((XElement)item[_cell.Index]).SetAttributeValue("Alarm", false);

            }
            x.Save(_pathXml);
            FetchModel(_pathXml);
            noteGrid.Refresh();
        }
    
        private void UpdateAlarm(object sender, EventArgs e)
        {
            string _answer = string.Empty;
            if (SingleQuestionn.SingleQuestionn.ShowDialog("Note:", ref _answer) == System.Windows.Forms.DialogResult.OK)
            {
                XDocument x = XDocument.Parse(System.IO.File.ReadAllText(_pathXml));

                foreach (DataGridViewRow _cell in alarmGrid.SelectedRows)
                {
                    var item = x.Descendants("AlarmItem").ToList();
                    ((XElement)item[_cell.Index]).SetAttributeValue("Note", _answer);
                }
                x.Save(_pathXml);
                FetchModel(_pathXml);
                noteGrid.Refresh();
            }
        }

        private void AlarmGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == alarmGrid.Columns["Note"].Index)
            {
                string note = alarmGrid.Rows[e.RowIndex].Cells["Note"].Value.ToString();

                MessageBox.Show(string.Format("Note:\n\r\n{0}", note));

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _alarmThread.Abort();
        }

        private void UpdateAlarmDateTime(object sender, EventArgs e)
        {
          
            foreach (DataGridViewRow _cell in alarmGrid.SelectedRows)
            {
                Alarm alar = new Alarm(_pathXml);
                alar._index = _cell.Index;
                alar.onRefresh += delegate { FetchModel(_pathXml); };
                alar.Show();

            }

            FetchModel(_pathXml);
            noteGrid.Refresh();
        }

      
    }
}
