using SSync.IO;
using System;

namespace Symbioz.Tools.DLM
{
    public abstract class DlmBasicElement
    {
        public enum ElementTypesEnum
        {
            Graphical = 2,
            Sound = 33
        }

        public DlmCell Cell
        {
            get;
            private set;
        }

        protected DlmBasicElement(DlmCell cell)
        {
            this.Cell = cell;
        }

        public static DlmBasicElement ReadFromStream(DlmCell cell, BigEndianReader reader)
        {
            byte type = reader.ReadByte();
            DlmBasicElement.ElementTypesEnum elementTypesEnum = (DlmBasicElement.ElementTypesEnum)type;
            DlmBasicElement result;
            if (elementTypesEnum != DlmBasicElement.ElementTypesEnum.Graphical)
            {
                if (elementTypesEnum != DlmBasicElement.ElementTypesEnum.Sound)
                {
                    throw new Exception(string.Concat(new object[]
					{
						"Unknown element ID : ",
						type,
						" CellID : ",
						cell.Id
					}));
                }
                result = DlmSoundElement.ReadFromStream(cell, reader);
            }
            else
            {
                result = DlmGraphicalElement.ReadFromStream(cell, reader);
            }
            return result;
        }
    }
}