using SSync.IO;
using Symbioz.Tools.ELE.Repertory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Tools.ELE
{
    /// <summary>
    /// Stump
    /// </summary>
    public abstract class EleGraphicalData
    {
        private Elements _Instance;

        public Elements Instance
        {
            get
            {
                return this._Instance;
            }

            set
            {
                if (this._Instance == value)
                {
                    return;
                }
                this._Instance = value;
            }
        }



        public int Id { get; set; }

        public abstract EleGraphicalElementTypes Type
        {
            get;
        }

        public EleGraphicalData(Elements instance, int id)
        {
            this.Instance = instance;
            this.Id = id;
        }

        public static EleGraphicalData readElement(Elements instance, BigEndianReader reader, int id)
        {
            EleGraphicalElementTypes type = (EleGraphicalElementTypes)reader.ReadByte();
            EleGraphicalData result;
            switch (type)
            {
                case EleGraphicalElementTypes.NORMAL:
                    result = NormalGraphicalElementData.ReadFromStream(instance, id, reader);
                    break;

                case EleGraphicalElementTypes.BOUNDING_BOX:
                    result = BoundingBoxGraphicalElementData.ReadFromStream(instance, id, reader);
                    break;

                case EleGraphicalElementTypes.ANIMATED:
                    result = AnimatedGraphicalElementData.ReadFromStream(instance, id, reader);
                    break;

                case EleGraphicalElementTypes.ENTITY:
                    result = EntityGraphicalElementData.ReadFromStream(instance, id, reader);
                    break;

                case EleGraphicalElementTypes.PARTICLES:
                    result = ParticlesGraphicalElementData.ReadFromStream(instance, id, reader);
                    break;

                case EleGraphicalElementTypes.BLENDED:
                    result = BlendedGraphicalElementData.ReadFromStream(instance, id, reader);
                    break;

                default:
                    throw new Exception("Unknown graphical data of type " + type);
            }
            return result;
        }

    }
}
