using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Handframe.Persistence
{
    public class Fields
    {
        public string name { get; set; }
        private string value_;
        public byte[] bytes_ { get; set; }
        public bool identity_ { get; set; }
        public bool key_ { get; set; }
        public bool integer_ { get; set; }
        public bool float_ { get; set; }
        public bool number_ { get; set; }
        public bool logic_ { get; set; }
        public bool text_ { get; set; }
        public bool ntext_ { get; set; }
        public bool date_ { get; set; }
        public bool time_ { get; set; }
        public bool dateTime_ { get; set; }
        public bool dateAlteration_ { get; set; }
        public bool archive_ { get; set; }
        public bool image_ { get; set; }
        public bool null_ { get; set; }
        public int size_ { get; set; }
        public int sizeDecimal_ { get; set; }
        public bool encrypted_ { get; set; }
        public bool header_ { get; set; }
        public bool foreignKey { get; set; }
        public string tableForeignKey { get; set; }
        public string fieldFK2 { get; set; }
        public DeleteUpdate toDelete { get; set; }
        public DeleteUpdate toUpdate { get; set; }
    }

    public enum DeleteUpdate
    {
        NOACTION = 1,
        CASCADE = 2,
        SETNULL = 3,
    }
}
