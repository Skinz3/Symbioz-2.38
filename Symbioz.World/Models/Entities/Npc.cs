using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Providers.Maps.Cinematics;
using Symbioz.World.Providers.Maps.Npcs;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Entities
{
    /// <summary>
    /// Représente un personnage non joueur.
    /// </summary>
    public class Npc : Entity
    {
        public override string Name
        {
            get
            {
                return Template.Name;
            }
        }
        public NpcSpawnRecord SpawnRecord
        {
            get;
            set;
        }
        public NpcRecord Template
        {
            get
            {
                return SpawnRecord.Template;
            }
        }

        public List<NpcActionRecord> ActionsRecord = new List<NpcActionRecord>();

        private long m_Id;

        public override long Id
        {
            get { return m_Id; }
        }

        public override ushort CellId
        {
            get
            {
                return SpawnRecord.CellId;
            }
            set
            {
                SpawnRecord.CellId = value;
                SpawnRecord.UpdateInstantElement();
            }
        }

        public override DirectionsEnum Direction
        {
            get
            {
                return SpawnRecord.DirectionEnum;
            }
            set
            {
                SpawnRecord.DirectionEnum = value;
                SpawnRecord.UpdateInstantElement();
            }
        }

        public override ContextActorLook Look
        {
            get
            {
                return Template.Look;
            }
            set
            {
                Template.Look = value;
                Template.UpdateInstantElement();
            }
        }
        public Npc(NpcSpawnRecord spawnRecord)
        {
            this.SpawnRecord = spawnRecord;
            this.ActionsRecord = NpcActionRecord.GetActions(SpawnRecord.Id);
            this.Map = MapRecord.GetMap(spawnRecord.MapId);
            this.m_Id = this.Map.Instance.PopNextNPEntityId();
        }

        private NpcActionRecord GetAction(NpcActionTypeEnum actionType)
        {
            return ActionsRecord.Find(x => x.ActionIdEnum == actionType);
        }
        public void InteractWith(Character character, NpcActionTypeEnum actionType)
        {
            if (character.Busy)
                return;

            if (actionType == NpcActionTypeEnum.Talk && CinematicProvider.Instance.IsNpcHandled(character, SpawnRecord.Id))
            {
                var npcPoint = new Maps.MapPoint((short)this.CellId);
                character.SetDirection(character.Point.OrientationTo(npcPoint,true));
                character.RandomTalkEmote();
                CinematicProvider.Instance.TalkToNpc(character, SpawnRecord.Id);
                return;
            }

            NpcActionRecord action = GetAction(actionType);

            if (action != null)
            {
                NpcActionProvider.Handle(character, this, action);
            }
            else if (character.Client.Account.Role > ServerRoleEnum.Player)
            {
                character.Reply("No (" + actionType + ") action linked to this npc...(" + SpawnRecord.Id + ")");
            }
        }
        public override GameRolePlayActorInformations GetActorInformations()
        {
            if (SpawnRecord.Template.ActionTypesEnum.Contains(NpcActionTypeEnum.Talk) && SpawnRecord.Template.ActionTypesEnum.Count == 1)
            {
                return new GameRolePlayNpcWithQuestInformations(Id, Look.ToEntityLook(), new EntityDispositionInformations((short)SpawnRecord.CellId, SpawnRecord.Direction), Template.Id, true, 0, new GameRolePlayNpcQuestFlag(new ushort[] { 0 }, new ushort[] { 3 }));
            }

            else
            {
                return new GameRolePlayNpcInformations((double)Id, Template.Look.ToEntityLook(), new EntityDispositionInformations((short)SpawnRecord.CellId, SpawnRecord.Direction),
                     Template.Id, true, 0);
            }

        }
        public override string ToString()
        {
            return "Npc (" + Name + ") (SpawnId:" + SpawnRecord.Id + ") (CellId:" + CellId + ")";
        }
    }
}
