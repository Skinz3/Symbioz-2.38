using SSync.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.Tools.ELE
{

    public class Elements
    {
        public byte Version { get; set; }

        private Dictionary<int, EleGraphicalData> _GraphicalDatas;

        public Dictionary<int, EleGraphicalData> GraphicalDatas { get; set; }

        private Dictionary<int, bool> _GfxJpgMap { get; set; }

        public Dictionary<int, bool> GfxJpgMap { get; set; }

        public Elements()
        {
            this.GraphicalDatas = new Dictionary<int, EleGraphicalData>();
            this.GfxJpgMap = new Dictionary<int, bool>();
            Indexes = new Dictionary<int, int>();
        }

        private Dictionary<int, int> Indexes;

        private BigEndianReader Reader;

        public static Elements ReadFromStream(BigEndianReader reader)
        {
            Elements instance = new Elements();
            instance.Reader = reader;
           reader.ReadByte(); // header
            instance.Version = reader.ReadByte();
            uint count = reader.ReadUInt();
            int edId;
            ushort skypLen = 0;
            for (int i = 0; i < count; i++)
            {
                if (instance.Version >= 9)
                {
                    skypLen = reader.ReadUShort();
                }
                edId = reader.ReadInt();

                if (instance.Version <= 8)
                {
                    instance.Indexes[edId] = reader.Position;
                    instance.ReadElement(edId);
                }
                else
                {
                    instance.Indexes[edId] = reader.Position;
                    reader.Seek((skypLen - 4), SeekOrigin.Current);
                }
            }

            if (instance.Version >= 8)
            {
                int gfxCount = reader.ReadInt();
                for (int i = 0; i < gfxCount; i++)
                {
                    instance.GfxJpgMap.Add(reader.ReadInt(), true);
                }
            }
            return instance;
        }
        
        public EleGraphicalData ReadElement(int elementId)
        {
            
            Reader.Seek(this.Indexes[elementId]);
          //  var loc2 = this.Reader.ReadByte();
            var loc3 = EleGraphicalData.readElement(this, Reader, (int)elementId);
          
            return loc3;
        }
        /*
         *  this._rawData["position"] = this._elementsIndex[param1];
         var _loc2_:int = this._rawData.readByte();
         var _loc3_:GraphicalElementData = GraphicalElementFactory.getGraphicalElementData(param1,_loc2_);
         if(!_loc3_)
         {
            return null;
         }
         _loc3_.fromRaw(this._rawData,this.fileVersion);
         this._elementsMap[param1] = _loc3_;
         return _loc3_;
         */
 

    }
}
