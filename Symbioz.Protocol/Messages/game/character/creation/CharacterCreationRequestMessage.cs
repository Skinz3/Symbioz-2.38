


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

    public class CharacterCreationRequestMessage : Message
    {

        public const ushort Id = 160;
        public override ushort MessageId
        {
            get { return Id; }
        }

        public string name;
        public sbyte breed;
        public bool sex;
        public int[] colors;
        public ushort cosmeticId;


        public CharacterCreationRequestMessage()
        {
        }

        public CharacterCreationRequestMessage(string name, sbyte breed, bool sex, int[] colors, ushort cosmeticId)
        {
            this.name = name;
            this.breed = breed;
            this.sex = sex;
            this.colors = colors;
            this.cosmeticId = cosmeticId;
        }


        public override void Serialize(ICustomDataOutput writer)
        {

            writer.WriteUTF(name);
            writer.WriteSByte(breed);
            writer.WriteBoolean(sex);
            foreach (var entry in colors)
            {
                writer.WriteInt(entry);
            }
            writer.WriteVarUhShort(cosmeticId);

        }

        public override void Deserialize(ICustomDataInput reader)
        {

            name = reader.ReadUTF();
            breed = reader.ReadSByte();
            if (breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Huppermage)
                throw new Exception("Forbidden value on breed = " + breed + ", it doesn't respect the following condition : breed < (byte)Enums.PlayableBreedEnum.Feca || breed > (byte)Enums.PlayableBreedEnum.Huppermage");
            sex = reader.ReadBoolean();

            colors = new int[5];
            for (int i = 0; i < 5; i++)
            {
                colors[i] = reader.ReadInt();
            }
            cosmeticId = reader.ReadVarUhShort();
            if (cosmeticId < 0)
                throw new Exception("Forbidden value on cosmeticId = " + cosmeticId + ", it doesn't respect the following condition : cosmeticId < 0");


        }


    }


}