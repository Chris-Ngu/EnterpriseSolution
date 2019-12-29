using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace EnterpriseSolution
{
    [Serializable()]
    public class Notes : ISerializable
    {
        private string header = "Notes\n-----";
        private string noteToSave;

        public Notes()
        {
            this.noteToSave = null;
        }
        public Notes(string noteToSave)
        {
            this.noteToSave = noteToSave;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Header", header);
            info.AddValue("Note", noteToSave);
        }
        
        public Notes(SerializationInfo info,StreamingContext context)
        {
            header = (string)info.GetValue("Header", typeof(string));
            noteToSave = (string)info.GetValue("Note", typeof(string));
        }
    }
}
