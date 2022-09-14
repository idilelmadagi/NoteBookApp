using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Reflection;

namespace NoteBook
{
    [Serializable]
    public class NoteModel
    {
        public NoteModel()
        {

        }

        private List<NoteItem> notes = new List<NoteItem>();

        [XmlArray]
        public List<NoteItem> Notes { set { notes = value; } get { return notes; } }

        private List<AlarmItem> alarms = new List<AlarmItem>();

        [XmlArray]
        public List<AlarmItem> Alarms { set { alarms = value; } get { return alarms; } }


        public class NoteItem : ICloneable
        {
            public NoteItem() { }


            public Object Clone()
            {
                return this.MemberwiseClone() as NoteItem;
            }

            private string _dateTime;
            [XmlAttribute]
            [DisplayName("DateTime")]
            public string DateTime { set { _dateTime = value; }  get { return _dateTime; } }


            private string _note;
            [XmlAttribute]
            [DisplayName("Note")]
            public string Note { set { _note = value; } get { return _note; } }


            private string _status;
            [XmlAttribute]
            [DisplayName("Status")]
            public string Status { set { _status = value; } get { return _status; } }

        }
        public class AlarmItem : ICloneable
        {
            public AlarmItem() { }


            public Object Clone()
            {
                return this.MemberwiseClone() as NoteItem;
            }

            private string _dateTime;
            [XmlAttribute]
            [DisplayName("DateTime")]
            public string DateTime { set { _dateTime = value; } get { return _dateTime; } }

            private string _alarmDate;
            [XmlAttribute]
            [DisplayName("AlarmDate")]
            public string AlarmDateTime { set { _alarmDate = value; } get { return _alarmDate; } }

            private bool _alarm;
            [XmlAttribute]
            [DisplayName("Alarm")]
            [Browsable(false)]
            public bool Alarm { set { _alarm = value; } get { return _alarm; } }


            private string _note;
            [XmlAttribute]
            [DisplayName("Note")]
            public string Note { set { _note = value; } get { return _note; } }

            private string _status;
            [XmlAttribute]
            [DisplayName("Status")]

            public string Status { set { _status = value; } get { return _status; } }



        }
    }
}
