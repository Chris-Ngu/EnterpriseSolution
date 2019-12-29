using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace EnterpriseSolution
{
    [Serializable()]
    public class Notes : ISerializable
    {
        public string header { get; set; }
        public string noteToSave { get; set; }

        private Notes()
        {
            this.header = "NO_PARAM_HEADER";
            this.noteToSave = "NO_PARAM_NOTE";
        }
        public Notes(string noteToSave)
        {
            this.header = "Notes\n-----";
            this.noteToSave = noteToSave;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("header", header);
            info.AddValue("noteToSave", noteToSave);
        }
        
        public Notes(SerializationInfo info,StreamingContext context)
        {
            header = (string)info.GetValue("header", typeof(string));
            noteToSave = (string)info.GetValue("noteToSave", typeof(string));
        }

        public override string ToString()
        {
            return (this.header + "\n" + this.noteToSave);
        }


    }
}
