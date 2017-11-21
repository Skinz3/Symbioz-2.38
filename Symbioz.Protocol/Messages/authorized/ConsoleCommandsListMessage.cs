


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class ConsoleCommandsListMessage : Message
{

public const ushort Id = 6127;
public override ushort MessageId
{
    get { return Id; }
}

public string[] aliases;
        public string[] args;
        public string[] descriptions;
        

public ConsoleCommandsListMessage()
{
}

public ConsoleCommandsListMessage(string[] aliases, string[] args, string[] descriptions)
        {
            this.aliases = aliases;
            this.args = args;
            this.descriptions = descriptions;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

writer.WriteUShort((ushort)aliases.Length);
            foreach (var entry in aliases)
            {
                 writer.WriteUTF(entry);
            }
            writer.WriteUShort((ushort)args.Length);
            foreach (var entry in args)
            {
                 writer.WriteUTF(entry);
            }
            writer.WriteUShort((ushort)descriptions.Length);
            foreach (var entry in descriptions)
            {
                 writer.WriteUTF(entry);
            }
            

}

public override void Deserialize(ICustomDataInput reader)
{

var limit = reader.ReadUShort();
            aliases = new string[limit];
            for (int i = 0; i < limit; i++)
            {
                 aliases[i] = reader.ReadUTF();
            }
            limit = reader.ReadUShort();
            args = new string[limit];
            for (int i = 0; i < limit; i++)
            {
                 args[i] = reader.ReadUTF();
            }
            limit = reader.ReadUShort();
            descriptions = new string[limit];
            for (int i = 0; i < limit; i++)
            {
                 descriptions[i] = reader.ReadUTF();
            }
            

}


}


}