using SSync.Messages;
using Symbioz.Core;
using Symbioz.Protocol.Messages;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Items;
using Symbioz.World.Network;
using Symbioz.World.Providers.Maps.Monsters;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Interactives;
using Symbioz.World.Records.Maps;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Records.Npcs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Maps.Instances
{
    public abstract class AbstractMapInstance
    {
        public int MonsterGroupCount
        {
            get
            {
                return GetEntities<MonsterGroup>().Count();
            }
        }

        public int CharactersCount
        {
            get
            {
                return GetEntities<Character>().Count();
            }
        }

        private List<Entity> m_entities = new List<Entity>();

        private List<Fight> m_fights = new List<Fight>();

        /// <summary>
        /// private
        /// </summary>
        private List<MapInteractiveElement> m_interactiveElements = new List<MapInteractiveElement>();

        private List<DropItem> m_droppedItems = new List<DropItem>();

        private ReversedUniqueIdProvider m_npIdPopper = new ReversedUniqueIdProvider(0);

        private UniqueIdProvider m_dropItemIdPopper = new UniqueIdProvider(0);

        public int PopNextDropItemId()
        {
            return m_dropItemIdPopper.Pop();
        }
        public MapRecord Record
        {
            get;
            private set;
        }

        private ActionTimer m_monsterSpawner;

        public bool Mute = false;

        public AbstractMapInstance(MapRecord record)
        {
            this.Record = record;
            this.m_monsterSpawner = new ActionTimer(MonsterSpawnManager.MonsterSpawningPoolInterval, AddSyncMonsterGroup, true);
            this.m_interactiveElements = Record.InteractiveElements.ConvertAll(x => x.GetMapInteractiveElement(this));
        }
        public void Reload()
        {
            this.Record.InteractiveElements = InteractiveElementRecord.GetActiveElementsOnMap(Record.Id);
            this.m_interactiveElements = Record.InteractiveElements.ConvertAll(x => x.GetMapInteractiveElement(this));
            this.OnElementsUpdated();

            foreach (var character in GetEntities<Character>())
            {
                character.Client.Send(GetMapComplementaryInformationsDataMessage(character));
            }

        }
        public void RandomMonsters(byte count,ushort characterLevel)
        {
            var list = MonsterRecord.Monsters.FindAll(x => x.Grades[0].Level >= characterLevel).Random(count);
            MonsterSpawnManager.Instance.AddFixedMonsterGroup(this, list.ToArray(), false);
        }
        public void AddQuietEntity(Entity entity)
        {
            lock (this)
            {
                m_entities.Add(entity);
                OnEntitiesUpdated();
            }
        }
        public void AddEntity(Entity entity)
        {
            lock (this)
            {
                if (!m_entities.Contains(entity))
                {
                    var informations = entity.GetActorInformations();
                    Send(new GameRolePlayShowActorMessage(informations));

                    m_entities.Add(entity);
                    OnEntitiesUpdated();
                }
            }
        }
        private void OnEntitiesUpdated()
        {
            if (Record.AbleToSpawn)
            {
                if (CharactersCount == 0)
                    this.m_monsterSpawner.Pause();
                else
                    this.m_monsterSpawner.Start();
                m_monsterSpawner.Interval = MonsterSpawnManager.MonsterSpawningPoolInterval * (MonsterGroupCount + 1);
            }
        }
        public void RemoveEntity(Entity entity)
        {
            lock (this)
            {
                m_entities.Remove(entity);
                this.Send(new GameContextRemoveElementMessage(entity.Id));
                OnEntitiesUpdated();
            }
        }
        public Entity[] GetEntities()
        {
            return m_entities.ToArray();
        }
        public T[] GetEntities<T>() where T : Entity
        {
            return m_entities.OfType<T>().ToArray();
        }
        public T GetEntity<T>(long id) where T : Entity
        {
            return m_entities.Find(x => x.Id == id) as T;
        }
        public Npc GetNpc(int spawnId)
        {
            return GetEntities<Npc>().FirstOrDefault(x => x.SpawnRecord.Id == spawnId);
        }
        public Entity GetEntity(long id)
        {
            return m_entities.Find(x => x.Id == id);
        }
        public Portal GetPortal(int portalTemplateId)
        {
            return GetEntities<Portal>().FirstOrDefault(x => x.Template.Id == portalTemplateId);
        }
        public void EntityTalk(Entity entity, string message)
        {
            Send(new EntityTalkMessage((double)entity.Id, 4, new string[] { message }));
        }
        public T[] GetElements<T>() where T : MapInteractiveElement
        {
            return m_interactiveElements.OfType<T>().ToArray();
        }
        public MapInteractiveElement GetElement(uint elementId)
        {
            return m_interactiveElements.Find(x => x.Id == elementId);
        }
        public void UseInteractive(Character character, uint elementId, uint skillUId)
        {
            MapInteractiveElement element = GetElement(elementId);



            if (element != null && element.CanUse(character) && !character.Busy)
            {
                MapInteractiveElementSkill skill = element.GetSkill(skillUId);

                if (skill != null)
                {
                    skill.Use(character);
                }
                else
                {
                    character.Client.Send(new InteractiveUseErrorMessage(elementId, skillUId));
                }
            }
            else
            {
                character.Client.Send(new InteractiveUseErrorMessage(elementId, skillUId));
            }
        }
        public void MapComplementary(WorldClient client)
        {

            client.Send(GetMapComplementaryInformationsDataMessage(client.Character));

            List<ushort> cellsIds = new List<ushort>();
            List<ushort> referencesIds = new List<ushort>();
            foreach (DropItem item in m_droppedItems)
            {
                cellsIds.Add(item.CellId);
                referencesIds.Add(item.Record.GId);
            }
            client.Send(new ObjectGroundListAddedMessage(cellsIds.ToArray(), referencesIds.ToArray()));
        }
        protected FightCommonInformations[] GetFightsCommonInformations()
        {
            return m_fights.FindAll(x => !x.Started).ConvertAll<FightCommonInformations>(x => x.GetFightCommonInformations()).ToArray();
        }
        protected InteractiveElement[] GetInteractivesElements(Character character)
        {
            return Array.ConvertAll(m_interactiveElements.ToArray(), x => x.GetInteractiveElement(character));
        }
        protected GameRolePlayActorInformations[] GetGameRolePlayActorsInformations()
        {
            return m_entities.ConvertAll<GameRolePlayActorInformations>(x => x.GetActorInformations()).ToArray();
        }
        protected StatedElement[] GetStatedElements()
        {
            return Array.ConvertAll(GetElements<MapStatedElement>(), x => x.GetStatedElement());
        }
        protected MapObstacle[] GetMapObstacles()
        {
            return new MapObstacle[0];
        }
        protected HouseInformations[] GetHousesInformations()
        {
            return new HouseInformations[0];
        }
        protected bool HasAgressiveMonsters()
        {
            return false;
        }
        public abstract MapComplementaryInformationsDataMessage GetMapComplementaryInformationsDataMessage(Character character);

        public long PopNextNPEntityId()
        {
            return (long)m_npIdPopper.Pop();
        }

        public void Send(Message message)
        {
            lock (this)
            {
                foreach (var character in GetEntities<Character>())
                {
                    character.Client.Send(message);
                }
            }
        }
        public void AddSyncMonsterGroup()
        {
            if (Record.AbleToSpawn)
            {
                if (this.MonsterGroupCount < MonsterSpawnManager.MaxMonsterGroupPerMap)
                {
                    if (Record.MonsterSpawnsSubArea.Length > 0)
                        MonsterSpawnManager.Instance.AddGeneratedMonsterGroup(this, Record.MonsterSpawnsSubArea, false);
                }
            }
        }
        public void AddNpc(NpcSpawnRecord npcRecord, bool quiet)
        {
            Npc entity = new Npc(npcRecord);

            if (quiet)
                AddQuietEntity(entity);
            else
                AddEntity(entity);
        }
        public Fight GetFight(int id)
        {
            return m_fights.FirstOrDefault(x => x.Id == id);
        }
        public void AddFight(Fight fight)
        {
            m_fights.Add(fight);


            if (fight.ShowBlades)
            {
                Send(new GameRolePlayShowChallengeMessage(fight.GetFightCommonInformations()));
            }

            Send(new MapFightCountMessage((ushort)m_fights.Count));
        }
        public void ShowPlacement(Character character)
        {
            character.Client.Send(new DebugClearHighlightCellsMessage());
            character.Client.Send(new DebugHighlightCellsMessage(Color.Red.ToArgb(), character.Map.RedCells.ConvertAll<ushort>(x => (ushort)x).ToArray()));
            character.Client.Send(new DebugHighlightCellsMessage(Color.Blue.ToArgb(), character.Map.BlueCells.ConvertAll<ushort>(x => (ushort)x).ToArray()));
        }
        public void AddPlacement(TeamColorEnum teamColor,short cellId)
        {
            if (teamColor == TeamColorEnum.Blue)
            {
                this.Record.BlueCells.Add(cellId);
            }
            else if (teamColor == TeamColorEnum.Red)
            {
                this.Record.RedCells.Add(cellId);
            }

            this.Record.UpdateElement();
        }
        public bool IsStated(int elementId)
        {
            return m_interactiveElements.Find(x => x.Id == elementId) != null;
        }
        public void RemoveFightSword(Fight fight)
        {
            Send(new GameRolePlayRemoveChallengeMessage(fight.Id));
        }
        public void RemoveFight(Fight fight)
        {
            if (fight.ShowBlades && !fight.Started)
                this.RemoveFightSword(fight);
            m_fights.Remove(fight);
            Send(new MapFightCountMessage((ushort)m_fights.Count));
        }
        public void MapFightCount(WorldClient client)
        {
            client.Send(new MapFightCountMessage((ushort)m_fights.Count));
        }
        public FightExternalInformations[] GetFightsExternalInformations()
        {
            List<FightExternalInformations> informations = new List<FightExternalInformations>();

            for (int i = 0; i < m_fights.Count; i++)
            {
                informations.Add(m_fights[i].GetExternalInformations());
            }
            return informations.ToArray();
        }

        public void AddDroppedItem(CharacterItemRecord record, ushort quantity, ushort cellid)
        {
            DropItem item = DropItem.Create(record, quantity, cellid, this);
            m_droppedItems.Add(item);
            this.Send(new ObjectGroundAddedMessage(cellid, record.GId));
        }

        public void ToggleMute()
        {
            Mute = !Mute;
        }


        public void RemoveDropItem(DropItem dropItem)
        {
            m_droppedItems.Remove(dropItem);
            m_dropItemIdPopper.Push(dropItem.Id);
            this.Send(new ObjectGroundRemovedMessage(dropItem.CellId));

        }

        public DropItem GetDroppedItem(ushort cellId)
        {
            return m_droppedItems.FirstOrDefault(x => x.CellId == cellId);
        }

        public DropItem[] GetDroppedItems()
        {
            return m_droppedItems.ToArray();
        }

        public void OnElementsUpdated()
        {
            foreach (var character in GetEntities<Character>())
            {
                var elements = Array.ConvertAll(m_interactiveElements.ToArray(), x => x.GetInteractiveElement(character));

                foreach (var element in elements)
                {
                    character.Client.Send(new InteractiveElementUpdatedMessage(element));
                }
            }
        }
    }
}
