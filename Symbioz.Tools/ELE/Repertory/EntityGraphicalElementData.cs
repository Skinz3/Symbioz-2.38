using SSync.IO;
using System.ComponentModel;

namespace Symbioz.Tools.ELE.Repertory
{
    public class EntityGraphicalElementData : EleGraphicalData
    {
        

        private string _EntityLook;

        public string EntityLook
        {
            get
            {
                return this._EntityLook;
            }

            set
            {
                if (string.Equals(this._EntityLook, value))
                {
                    return;
                }
                this._EntityLook = value;
              
            }
        }

        private bool _HorizontalSymmetry;

        public bool HorizontalSymmetry
        {
            get
            {
                return this._HorizontalSymmetry;
            }

            set
            {
                if (this._HorizontalSymmetry == value)
                {
                    return;
                }
                this._HorizontalSymmetry = value;
           
            }
        }

        private bool _PlayAnimation;

        public bool PlayAnimation
        {
            get
            {
                return this._PlayAnimation;
            }

            set
            {
                if (this._PlayAnimation == value)
                {
                    return;
                }
                this._PlayAnimation = value;
            
            }
        }

        private bool _PlayAnimStatic;

        public bool PlayAnimStatic
        {
            get
            {
                return this._PlayAnimStatic;
            }

            set
            {
                if (this._PlayAnimStatic == value)
                {
                    return;
                }
                this._PlayAnimStatic = value;
           
            }
        }

        private uint _MinDelay;

        public uint MinDelay
        {
            get
            {
                return this._MinDelay;
            }

            set
            {
                if (this._MinDelay == value)
                {
                    return;
                }
                this._MinDelay = value;
                
            }
        }

        private uint _MaxDelay;

        public uint MaxDelay
        {
            get
            {
                return this._MaxDelay;
            }

            set
            {
                if (this._MaxDelay == value)
                {
                    return;
                }
                this._MaxDelay = value;
            }
        }

        public override EleGraphicalElementTypes Type
        {
            get
            {
                return EleGraphicalElementTypes.ENTITY;
            }
        }

        public EntityGraphicalElementData(Elements instance, int id)
            : base(instance, id)
        {
        }

        public static EntityGraphicalElementData ReadFromStream(Elements instance, int id, BigEndianReader reader)
        {
            EntityGraphicalElementData data = new EntityGraphicalElementData(instance, id);
            data.EntityLook = reader.ReadUTF7BitLength();
            data.HorizontalSymmetry = reader.ReadBoolean();
            if (instance.Version >= 7)
            {
                data.PlayAnimation = reader.ReadBoolean();
            }
            if (instance.Version >= 6)
            {
                data.PlayAnimStatic = reader.ReadBoolean();
            }
            if (instance.Version >= 5)
            {
                data.MinDelay = reader.ReadUInt();
                data.MaxDelay = reader.ReadUInt();
            }
            return data;
        }
    }
}