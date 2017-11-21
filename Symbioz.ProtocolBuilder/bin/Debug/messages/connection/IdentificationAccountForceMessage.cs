


















using System;
using System.Collections.Generic;
using System.Linq;
using Symbioz.Protocol.Types;
using SSync.IO;
using SSync.Messages;

namespace Symbioz.Protocol.Messages
{

public class IdentificationAccountForceMessage : IdentificationMessage
{

public const ushort Id = 6119;
public override ushort MessageId
{
    get { return Id; }
}

public string forcedAccountLogin;
        

public IdentificationAccountForceMessage()
{
}

public IdentificationAccountForceMessage(bool autoconnect, bool useCertificate, bool useLoginToken, Types.VersionExtended version, string lang, sbyte[] credentials, short serverId, long sessionOptionalSalt, ushort[] failedAttempts, string forcedAccountLogin)
         : base(autoconnect, useCertificate, useLoginToken, version, lang, credentials, serverId, sessionOptionalSalt, failedAttempts)
        {
            this.forcedAccountLogin = forcedAccountLogin;
        }
        

public override void Serialize(ICustomDataOutput writer)
{

base.Serialize(writer);
            writer.WriteUTF(forcedAccountLogin);
            

}

public override void Deserialize(ICustomDataInput reader)
{

base.Deserialize(reader);
            forcedAccountLogin = reader.ReadUTF();
            

}


}


}