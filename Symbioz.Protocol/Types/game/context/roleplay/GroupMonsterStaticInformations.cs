


















// Generated on 06/04/2015 18:45:27
using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;

namespace Symbioz.Protocol.Types
{

    public class GroupMonsterStaticInformations
    {

        public const short Id = 140;
        public virtual short TypeId
        {
            get { return Id; }
        }

        public MonsterInGroupLightInformations mainCreatureLightInfos;
        public IEnumerable<MonsterInGroupInformations> underlings;


        public GroupMonsterStaticInformations()
        {
        }

        public GroupMonsterStaticInformations(MonsterInGroupLightInformations mainCreatureLightInfos, IEnumerable<MonsterInGroupInformations> underlings)
        {
            this.mainCreatureLightInfos = mainCreatureLightInfos;
            this.underlings = underlings;
        }


        public virtual void Serialize(ICustomDataOutput writer)
        {

            mainCreatureLightInfos.Serialize(writer);
            writer.WriteUShort((ushort)underlings.Count());
            foreach (var entry in underlings)
            {
                entry.Serialize(writer);
            }


        }

        public virtual void Deserialize(ICustomDataInput reader)
        {

            mainCreatureLightInfos = new MonsterInGroupLightInformations();
            mainCreatureLightInfos.Deserialize(reader);
            var limit = reader.ReadUShort();
            underlings = new MonsterInGroupInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                (underlings as MonsterInGroupInformations[])[i] = new MonsterInGroupInformations();
                (underlings as MonsterInGroupInformations[])[i].Deserialize(reader);
            }


        }


    }


}