using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.ELE.Repertory
{
    public class ParticlesGraphicalElementData : EleGraphicalData
    {
        

        public override EleGraphicalElementTypes Type
        {
            get
            {
                return EleGraphicalElementTypes.ANIMATED;
            }
        }

        private int _ScriptId;

        public int ScriptId
        {
            get
            {
                return this._ScriptId;
            }

            set
            {
                if (this._ScriptId == value)
                {
                    return;
                }
                this._ScriptId = value;
            }
        }

        public ParticlesGraphicalElementData(Elements instance, int id)
            : base(instance, id)
        {
        }

        public static ParticlesGraphicalElementData ReadFromStream(Elements instance, int id, BigEndianReader reader)
        {
            return new ParticlesGraphicalElementData(instance, id)
            {
                ScriptId = (int)reader.ReadShort()
            };
        }
    }
}