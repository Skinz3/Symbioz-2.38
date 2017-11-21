using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Effects;
using Symbioz.World.Models.Fights.Fighters;
using Symbioz.World.Models.Fights.Results;
using Symbioz.World.Records.Characters;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Monsters;
using System;
using Symbioz.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Enums;
using Symbioz.Core.DesignPattern.StartupEngine;
using System.IO;

namespace Symbioz.World.Providers.Fights.Results
{
    /// <summary>
    /// Symbioz 2.34 2016   //4275
    /// </summary>
    [CustomLoot("PL>5", FightTypeEnum.FIGHT_TYPE_PvM)]
    public class MinationLoot : CustomLoot
    {
        public const string FileName = "mination.xml";

        private static MinationDataFile DataFile;

        private static string FilePath
        {
            get
            {
                return Environment.CurrentDirectory + "/" + FileName;
            }
        }
        [StartupInvoke("Mination Loot", StartupInvokePriority.Eighth)]
        public static void Initialize()
        {

            if (!File.Exists(FilePath))
            {
                DataFile = new MinationDataFile();
                DataFile.ForbiddenMonsters = new List<ushort>();
                string file = DataFile.XMLSerialize();
                File.WriteAllText(FilePath, file);
            }

            DataFile = File.ReadAllText(FilePath).XMLDeserialize<MinationDataFile>();

        }
        private static ushort[] ForbiddenMonsters
        {
            get
            {
                return DataFile.ForbiddenMonsters.ToArray();
            }
        }

        public static void AddForbiddenMonster(ushort id)
        {
            DataFile.ForbiddenMonsters.Add(id);
            File.WriteAllText(FilePath, DataFile.XMLSerialize());
        }
        /// <summary>
        /// GenericId de l'item qui sera drop a la fin d'un combat PVM si le critère définit par 
        /// l'attribut [CustomLoot] est vérifié.
        /// </summary>
        public const ushort MinationItemId = 14989;

        public const ushort MinationMiniBossItemId = 14984;

        public const ushort MinationBossItemId = 14986;

        public const sbyte MinationDropPercentage = 10;

        /// <summary>
        /// <summary>
        /// Template de l'item Mination , en fonction de la constante MinationItemId.
        /// </summary>
        private static ItemRecord MinationItemTemplate;
        private static ItemRecord MinationItemMiniBossTemplate;
        private static ItemRecord MinationItemBossTemplate;

        public static bool CantBeEquiped(ushort monsterId)
        {
            return ForbiddenMonsters.Contains(monsterId);
        }
        /// <summary>
        /// Chargement des Templates.
        /// </summary>
        static MinationLoot()
        {
            MinationItemMiniBossTemplate = ItemRecord.GetItem(MinationMiniBossItemId);
            MinationItemBossTemplate = ItemRecord.GetItem(MinationBossItemId);
            MinationItemTemplate = ItemRecord.GetItem(MinationItemId);
        }


        public MinationLoot(FightPlayerResult result)
            : base(result)
        {
        }

        public override void Apply()
        {
            try
            {
                MonsterFighter monster = Result.Fighter.OposedTeam().GetFighters<MonsterFighter>(false).FindAll(x => !x.Template.IsBoss && !x.Template.IsMiniBoss && !ForbiddenMonsters.Contains(x.Template.Id)).Random();
                if (monster != null)
                {
                    if (new AsyncRandom().TriggerAleat(MinationDropPercentage))
                    {
                        AddMinationItem(monster);
                    }
                }

                var boss = Result.Fighter.OposedTeam().GetFighters<MonsterFighter>(false).FindAll(x => x.Template.IsBoss || x.Template.IsMiniBoss).Random();

                if (boss != null && new AsyncRandom().TriggerAleat(6))
                {
                    AddMinationItem(boss);
                }

            }
            catch (Exception ex)
            {
                Logger.Write<MinationLoot>(ex.ToString(), ConsoleColor.DarkRed);
            }
        }
        private void AddMinationItem(MonsterFighter monster)
        {
            CharacterItemRecord minationItem = CreateMinationItem(MinationItemTemplate, 1, Result.Character.Id, monster.Template, monster.GradeId);
            this.Add(minationItem);
        }
        /// <summary>
        /// Créer un item mination lié a un joueur.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="quantity"></param>
        /// <param name="characterId"></param>
        /// <param name="monster"></param>
        /// <param name="monsterGradeId"></param>
        /// <returns></returns>
        public static CharacterItemRecord CreateMinationItem(ItemRecord template, uint quantity, long characterId, MonsterRecord monster, sbyte monsterGradeId)
        {
            CharacterItemRecord item;
            if (monster.IsMiniBoss)
                item = MinationItemMiniBossTemplate.GetCharacterItem(characterId, quantity, true);
            else if (monster.IsBoss)
                item = MinationItemBossTemplate.GetCharacterItem(characterId, quantity, true);
            else
                item = MinationItemTemplate.GetCharacterItem(characterId, quantity, true);
            item.Effects.Clear();
            item.AddEffect(new EffectMination(monster.Id, monster.Name, monsterGradeId));
            item.AddEffect(new EffectMinationLevel(1, 0, 0));
            item.AddEffectInteger(EffectsEnum.Effect_Followed, monster.Id);
            return item;
        }
        public class MinationDataFile
        {
            public List<ushort> ForbiddenMonsters
            {
                get;
                set;
            }
        }
    }
}
