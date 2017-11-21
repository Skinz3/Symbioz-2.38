using Symbioz.Core.DesignPattern;
using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.World.Models.Entities;
using Symbioz.Core;
using Symbioz.World.Models.Fights;
using Symbioz.World.Models.Guilds;
using Symbioz.World.Records.Guilds;
using Symbioz.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Providers.Fights;
using Symbioz.World.Network;

namespace Symbioz.World.Providers.Guilds
{
    public struct GuildFightDescription
    {
        public string FirstGuildName;

        public string SecondGuildName;

        public int FightHour;

        public int FightMinute;

        public GuildFightDescription(string firstGuildName,string secondGuildName,int fightHour,int fightMinute)
        {
            this.FirstGuildName = firstGuildName;
            this.SecondGuildName = secondGuildName;
            this.FightHour = fightHour;
            this.FightMinute = fightMinute;
        }
    }
    public struct BasicTime
    {
        public int Hour;

        public int Minutes;

        public BasicTime(int hour, int minutes)
        {
            this.Hour = hour;
            this.Minutes = minutes;
        }
    }
    class GuildArenaProvider
    {
        public static int[] GVG_MAPS = new int[]
        {
            134479872, 94634509
        };

        public const int GVG_HUB_MAP_ID = 154142209;

        public static DayOfWeek[] FIGHT_DAYS = new DayOfWeek[]
        {
            DayOfWeek.Saturday,
            DayOfWeek.Sunday,
        };
        public static BasicTime[] FIGHT_TIMES = new BasicTime[]
        {
             new BasicTime(13,33),
             new BasicTime(18,0),
        };

        public static DayOfWeek DRESS_ARRAY_DAY = DayOfWeek.Thursday;

        public const int MAX_GUILDS_FIGHTING = 4;

        public const int MINUTES_BEFORE_GVG_START = 1;

        public static MapRecord HubMapRecord;

        private static bool IsFightDay
        {
            get
            {
                return FIGHT_DAYS.Contains(DateTime.Now.DayOfWeek);
            }
        }
        private static bool SortNeeded
        {
            get
            {
                return GuildArenaRecord.Sorted() == false && (DRESS_ARRAY_DAY >= DateTime.Now.DayOfWeek || FIGHT_DAYS.Contains(DRESS_ARRAY_DAY));
            }
        }
        [StartupInvoke("GuildArenas", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {
            HubMapRecord = MapRecord.GetMap(GVG_HUB_MAP_ID);
            CheckTheDay();

        }
        public static void CheckTheDay()
        {
            if (IsFightDay)
            {
                foreach (var fightTime in FIGHT_TIMES)
                {
                    if (DateTime.Now.Hour <= fightTime.Hour && DateTime.Now.Minute <= fightTime.Minutes)
                    {
                        DateTime fightDateTime = DateTime.Today.AddHours(fightTime.Hour).AddMinutes(fightTime.Minutes);
                        var delay = (DateTime.Now - fightDateTime);
                        ActionTimer timer = new ActionTimer((int)-delay.TotalMilliseconds, Fight, false);
                        timer.Start();
                    }
                }
            }
            if (SortNeeded)
            {
                Sort();
            }
        }
        public static void Register(Character character)
        {
            if (CanRegisterGvG(character))
            {
                GuildArenaRecord guildArena = GuildArenaRecord.CreateGuildArena(character.Guild.Id, -1);
                guildArena.AddElement();
                character.Client.SendRaw(RawDataManager.GetRawData("gvgsuccess"));
            }

        }
        private static bool CanRegisterGvG(Character character)
        {
            if (character.HasGuild == false)
            {
                character.Reply("Vous ne possédez pas de guilde.");
                return false;
            }
            if (character.GuildMember.IsBoss == false)  // et le bras droit
            {
                character.Reply("Vous n'êtes pas le meneur de la guilde, impossible de vous enregistrer.");
                return false;
            }
            if (IsRegister(character.Guild) == true)
            {
                character.Reply("Votre guilde est déja enregistrée à la GvG.");
                return false;
            }
            if ((FIGHT_DAYS.Contains((DayOfWeek)DateTime.Now.Day) || GuildArenaRecord.Sorted()))
            {
                character.Reply("Impossible de vous inscrire à la GvG après que les tableaux ai été dressé.");
                return false;
            }
            if (GuildArenaRecord.GuildsArena.Count >= MAX_GUILDS_FIGHTING)
            {
                character.Reply("Le nombre maxium de guilde inscrite a déja été atteint.");
                return false;
            }
            return true;
        }

        public static void Unregister(Character character)
        {
            GuildArenaRecord guildArena = GuildArenaRecord.GetGuildArena(character.Guild.Id);

            if (guildArena != null)
            {
                guildArena.RemoveElement();
                character.Client.Character.OpenPopup(0, "GvG", "Vous êtes desinscrit de la GvG");
            }
        }

        public static void Sort()
        {
            if (GuildArenaRecord.GuildsArena.Count > 1)
            {
                var arenas = GuildArenaRecord.GuildsArena.Shuffle().ToArray();

                for (int i = 0; i < arenas.Length; i++)
                {
                    if (arenas.Length == i + 1)
                    {
                        break;
                    }
                    arenas[i].SecondGuildId = arenas[i + 1].FirstGuildId;
                }

                var newArenas = arenas.Take(8);

                GuildArenaRecord.GuildsArena = newArenas.ToList();
            }


        }
        public static bool IsRegister(GuildInstance guild)
        {
            var guildArena = GuildArenaRecord.GuildsArena.Find(x => x.FirstGuildId == guild.Id);

            if (guildArena == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void Fight() // ajout de la notification delai avec le timer
        {
            var guilds = GuildArenaRecord.GuildsArena.Take(GuildArenaRecord.GuildsArena.Count / 2);
            foreach (var guild in guilds.ToArray())
            {
                var clients = WorldServer.Instance.GetOnlineClients().FindAll(x => x.Character.Guild.Id == guild.FirstGuildId || x.Character.Guild.Id == guild.SecondGuildId);

                foreach (var client in clients)
                {
                    client.SendRaw(RawDataManager.GetRawData("gvgtimer"));
                }
                guild.RemoveElement();
            }

            ActionTimer timer = new ActionTimer(MINUTES_BEFORE_GVG_START * 60000, StartFighting, false);
            timer.Start();

        }
        private static void StartFighting()
        {
            var guilds = GuildArenaRecord.GuildsArena.Take(GuildArenaRecord.GuildsArena.Count / 2);

            MapRecord mapTarget = MapRecord.GetMap(GVG_MAPS.Random());
            foreach (var guildArena in guilds)
            {
                var fight = FightProvider.Instance.CreateFightGvG(mapTarget);

                List<Character> blueTeam = new List<Character>();
                List<Character> redTeam = new List<Character>();
                foreach (var character in HubMapRecord.Instance.GetEntities<Character>())
                {
                    if (character.Guild.Id == guildArena.FirstGuildId)
                    {
                        blueTeam.Add(character);
                    }
                    else if (character.Guild.Id == guildArena.SecondGuildId)
                    {
                        redTeam.Add(character);
                    }
                }
                if (blueTeam.Count == 0 || redTeam.Count == 0)
                {
                    foreach (var character in HubMapRecord.Instance.GetEntities<Character>())
                    {
                        character.Reply("Le match opposant les deux guildes n'aura pas lieu car, il n'ya pas assez de membre pour combattre");
                    }
                    continue;
                }
                foreach (var character in blueTeam)
                {
                    character.Teleport(mapTarget.Id);
                    fight.BlueTeam.AddFighter(character.CreateFighter(fight.BlueTeam));
                }
                foreach (var character in redTeam)
                {
                    character.Teleport(mapTarget.Id);
                    fight.RedTeam.AddFighter(character.CreateFighter(fight.RedTeam));
                }
                guildArena.RemoveElement();
                fight.StartPlacement();

            }
        }
    }
}

