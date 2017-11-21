using SSync.Messages;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Maps;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities
{
    public abstract class Entity
    {
        public abstract long Id
        {
            get;
        }

        public abstract string Name
        {
            get;
        }
        public MapPoint Point
        {
            get
            {
                return new MapPoint((short)CellId);
            }
        }
        public abstract ushort CellId
        {
            get;
            set;
        }
        public MapRecord Map
        {
            get;
            set;
        }

        public abstract DirectionsEnum Direction { get; set; }

        public abstract ContextActorLook Look { get; set; }

        public abstract GameRolePlayActorInformations GetActorInformations();

        public void SendMap(Message message)
        {
            if (Map != null && Map.Instance != null)
                Map.Instance.Send(message);
        }

        public void Say(string msg)
        {
            SendMap(new EntityTalkMessage(Id, 4, new string[] { msg }));
        }
        public void Say(Character character, string msg)
        {
            character.Client.Send(new EntityTalkMessage(Id, 4, new string[] { msg }));
        }

        /// <summary>
        /// override for Character.cs AccountId ? important?
        /// </summary>
        /// <param name="smileyid"></param>
        public virtual void DisplaySmiley(ushort smileyid)
        {
            SendMap(new ChatSmileyMessage(Id, smileyid, 0));
        }
        public void DisplaySmiley(Character character, ushort id)
        {
            character.Client.Send(new ChatSmileyMessage(Id, id, 0));
        }
        public void SpellAnim(Character character, ushort id)
        {
            character.Client.Send(new GameRolePlaySpellAnimMessage((ulong)Id, CellId, id, 1));
        }
        public void SpellAnim(ushort id)
        {
 
            SendMap(new GameRolePlaySpellAnimMessage((ulong)Id, CellId, id, 1));
        }

    }
}
