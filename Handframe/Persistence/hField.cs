using Handframe.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handframe.Persistence
{
    public class hField
    {
        public string name_ { get; set; }
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
        public bool foreignKey_ { get; set; }
        public string tableFK_ { get; set; }
        public string fieldFK2_ { get; set; }
        public DeleteUpdate toDelete_ { get; set; }
        public DeleteUpdate toUpdate_ { get; set; }

        public hField(string name)
        {
            this.name_ = name;
            this.value_ = "";
            this.bytes_ = new byte[1];
            this.identity_ = false;
            this.key_ = false;
            this.integer_ = false;
            this.float_ = false;
            this.number_ = false;
            this.logic_ = false;
            this.text_ = false;
            this.ntext_ = false;
            this.date_ = false;
            this.time_ = false;
            this.dateTime_ = false;
            this.archive_ = false;
            this.null_ = true;
            this.size_ = 0;
            this.sizeDecimal_ = 2;
            this.header_ = false;
            this.encrypted_ = false;
            this.foreignKey_ = false;
            this.tableFK_ = "";
            this.fieldFK2_ = "";
            this.toDelete_= DeleteUpdate.NOACTION;
            this.toUpdate_ = DeleteUpdate.NOACTION;
        }
        public string value
        {
            get
            {
                if (this.logic_)
                {
                    if (this.value_.Length == 0)
                    {
                        return "False";
                    }
                }
                return this.value_;
            }
            set
            {
                this.value_ = value;
            }
        }
        public hField Copy()
        {
            hField f = new hField(this.name_);
            f.value_ = this.value_;
            f.bytes_ = this.bytes_;
            f.identity_ = this.identity_;
            f.key_ = this.key_;
            f.integer_ = this.integer_;
            f.float_ = this.float_;
            f.number_ = this.number_;
            f.logic_ = this.logic_;
            f.text_ = this.text_;
            f.ntext_ = this.ntext_;
            f.date_ = this.date_;
            f.dateAlteration_ = this.dateAlteration_;
            f.time_ = this.time_;
            f.dateTime_ = this.dateTime_;
            f.archive_ = this.archive_;
            f.image_ = this.image_;
            f.null_ = this.null_;
            f.size_ = this.size_;
            f.sizeDecimal_ = this.sizeDecimal_;
            f.header_= this.header_;
            f.encrypted_ = this.encrypted_;
            f.foreignKey_ = this.foreignKey_;
            f.tableFK_ = this.tableFK_;
            f.fieldFK2_ = this.fieldFK2_;
            f.toDelete_ = this.toDelete_;
            f.toUpdate_ = this.toUpdate_;
            return f;
        }
        public override bool Equals(object obj)
        {
            if (obj is hField)
            {
                hField f = (hField)obj;
                return
                f.value_ == this.value_ &&
                hSystem.VectorCompare(f.bytes_, this.bytes_) &&
                f.identity_== this.identity_ &&
                f.key_== this.key_ &&
                f.integer_ == this.integer_ &&
                f.float_ == this.float_ &&
                f.number_ == this.number_ &&
                f.logic_ == this.logic_ &&
                f.text_ == this.text_ &&
                f.ntext_ == this.ntext_ &&
                f.date_ == this.date_ &&
                f.dateAlteration_== this.dateAlteration_ &&
                f.time_ == this.time_ &&
                f.dateTime_ == this.dateTime_ &&
                f.archive_ == this.archive_ &&
                f.image_== this.image_ &&
                f.null_ == this.null_ &&
                f.size_ == this.size_ &&
                f.sizeDecimal_ == this.sizeDecimal_ &&
                f.header_ == this.header_ &&
                f.encrypted_ == this.encrypted_ &&
                f.foreignKey_ == this.foreignKey_ &&
                f.tableFK_ == this.tableFK_ &&
                f.fieldFK2_ == this.fieldFK2_ &&
                f.toDelete_ == this.toDelete_ &&
                f.toUpdate_ == this.toUpdate_;
            }
            return false;
        }
        public System.Drawing.Image Imagem
        {
            get
            {
                try
                {
                    if (this.bytes_.Length > 1)
                    {
                        return System.Drawing.Image.FromStream(new MemoryStream(this.bytes_, 0, this.bytes_.Length), true);
                    }

                }
                catch (Exception ex)
                {
                }
                return null;
            }
            set
            {
                this.bytes_ = new byte[1];
                if (value != null)
                {
                    ImageConverter converter = new ImageConverter();
                    this.bytes_ = (byte[])converter.ConvertTo(value, typeof(byte[]));
                    if (Sistema.conexao.IsPostgreSQL())
                    {
                        this.valor = Sistema.ToStringBytesPostgres(this.bytes);
                    }
                }
            }
        }
    }

    public enum DeleteUpdate
    {
        NOACTION = 1,
        CASCADE = 2,
        SETNULL = 3,
    }
}
