using Symbioz.World.Models.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Protocol.Enums;
using System.Threading.Tasks;
using Symbioz.Protocol.Messages;
using SSync.Messages;
using Symbioz.World.Models.Fights;
using Symbioz.Core;
using Symbioz.World.Providers.Fights;
using Symbioz.World.Records.Maps;

namespace Symbioz.World.Providers.Arena
{
    public class ArenaGroup
    {
        public const ushort LEVEL_SHIFT = 10;

        public CharacterInventoryPositionEnum[] NoPositions = new CharacterInventoryPositionEnum[]
        {
            CharacterInventoryPositionEnum.INVENTORY_POSITION_COMPANION,
            CharacterInventoryPositionEnum.ACCESSORY_POSITION_SHIELD,
        };


        public virtual PvpArenaTypeEnum Type
        {
            get
            {
                return PvpArenaTypeEnum.ARENA_TYPE_1VS1;
            }
        }
        public virtual ushort RequestDuration
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Le joueur a t-il le niveau pour rejoindre ce groupe
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool CanChallenge(Character character)
        {
            if (Empty) // Si le groupe est vide, ... il a forcement le niveau
            {
                return true;
            }
            double mediumLevel = 1; 
            double med = Extensions.Med(Array.ConvertAll(BlueTeam.GetMembers(), x => x.Character.Level)); // niveau moyen de l'équipe bleue
            double med2 = Extensions.Med(Array.ConvertAll(RedTeam.GetMembers(), x => x.Character.Level)); // niveau moyen de l'équipe rouge

            if (med2 == 0) // si il n'y a aucun membre dans l'équipe rouge
            {
                mediumLevel = med;
            }
            else if (med == 0) // si il n'y a aucun membre dans l'équipe bleue
            {
                mediumLevel = med2;
            }
            else
            {
                mediumLevel = Extensions.Med(new double[] { med, med2 }); // on calcule la moyenne de niveau entre les deux groupes

            }
            return character.Level > mediumLevel - LEVEL_SHIFT && character.Level < mediumLevel + LEVEL_SHIFT; // mediumLevel - shift < x < mediumLevel + shift

        }

        public bool Ready
        {
            get
            {
                return BlueTeam.IsFull && RedTeam.IsFull;
            }
        }
        public bool Accepted
        {
            get
            {
                return BlueTeam.Accepted && RedTeam.Accepted;
            }
        }
        private bool IsTeamFull(List<Character> team)
        {
            return team.Count == (int)Type;
        }
        public ArenaGroup()
        {
            BlueTeam = new ArenaMemberCollection(this);
            RedTeam = new ArenaMemberCollection(this);
        }

        protected ArenaMemberCollection BlueTeam
        {
            get;
            private set;
        }
        protected ArenaMemberCollection RedTeam
        {
            get;
            private set;
        }
        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        protected ArenaMemberCollection SelectTeam(Character character)
        {
            if (!BlueTeam.IsFull)
            {
                return BlueTeam;
            }
            else if (!RedTeam.IsFull)
            {
                return RedTeam;
            }
            else
            {
                throw new Exception("Both teams are full, cannot add character");
            }
        }


        public ArenaMember AddCharacter(Character character)
        {
            return SelectTeam(character).AddMember(this, character);
        }
        public void Send(Message message)
        {
            ForEach(x => x.Send(message));
        }
        public void ForEach(Action<ArenaMember> action)
        {
            foreach (var member in BlueTeam.GetMembers())
            {
                action(member);
            }
            foreach (var member in RedTeam.GetMembers())
            {
                action(member);
            }
        }
        public void Request()
        {
            ForEach(x => x.Request());
            ForEach(x => x.UpdateStep(false, PvpArenaStepEnum.ARENA_STEP_WAITING_FIGHT));
        }
        public bool Empty
        {
            get
            {
                return BlueTeam.Empty && RedTeam.Empty;
            }
        }
        public ArenaMember[] GetAllMembers()
        {
            return BlueTeam.GetMembers().Concat(RedTeam.GetMembers()).ToArray();
        }
        public void CheckIntegrity(ArenaMember member)
        {
            foreach (var noPos in NoPositions)
            {
                if (member.Character.Inventory.Unequip(noPos))
                {
                    member.Character.OnItemUnequipedArena();
                }
            }

        }

        public void StartFighting()
        {
            ForEach(x => x.Character.PreviousRoleplayMapId = x.Character.Record.MapId);
            ForEach(x => x.UpdateStep(false, PvpArenaStepEnum.ARENA_STEP_STARTING_FIGHT));

            MapRecord map = ArenaMapRecord.GetArenaMap();

            foreach (var member in BlueTeam.GetMembers())
            {
                member.Character.Teleport(map.Id, null, true);
                CheckIntegrity(member);

            }
            foreach (var member in RedTeam.GetMembers())
            {
                member.Character.Teleport(map.Id, null, true);
                CheckIntegrity(member);
            }
            FightArena fight = FightProvider.Instance.CreateFightArena(map);

            foreach (var member in BlueTeam.GetMembers())
            {
                fight.BlueTeam.AddFighter(member.Character.CreateFighter(fight.BlueTeam));
            }

            foreach (var member in RedTeam.GetMembers())
            {
                fight.RedTeam.AddFighter(member.Character.CreateFighter(fight.RedTeam));
            }


            fight.StartPlacement();
            this.Dispose();
        }

        public bool ContainsIp(string ip)
        {
            return GetAllMembers().Any(x => x.Character.Client.Ip == ip);
        }
        public void Dispose()
        {
            ArenaProvider.Instance.ArenaGroups.Remove(this);
            ForEach(x => x.Character.UnregisterArena());
            ForEach(x => x.UpdateStep(false, PvpArenaStepEnum.ARENA_STEP_UNREGISTER));
            this.RedTeam = null;
            this.BlueTeam = null;
        }
    }
}
