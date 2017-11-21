using Symbioz.Core.DesignPattern.StartupEngine;
using Symbioz.ORM;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Records.Breeds
{
    [Table("Breeds")]
    public class BreedRecord : ITable
    {
        public static List<BreedRecord> Breeds = new List<BreedRecord>();

        public static uint AvailableBreedsFlags
        {
            get
            {
                return (uint)AvailableBreeds.Aggregate(0, (int current, PlayableBreedEnum breedEnum) => current | 1 << breedEnum - PlayableBreedEnum.Feca);
            }
        }
        public static readonly List<PlayableBreedEnum> AvailableBreeds = new List<PlayableBreedEnum>
        {
            PlayableBreedEnum.Feca ,
            PlayableBreedEnum.Enutrof ,
            PlayableBreedEnum.Sram,
            PlayableBreedEnum.Ecaflip,
            PlayableBreedEnum.Eniripsa ,
            PlayableBreedEnum.Iop ,
            PlayableBreedEnum.Cra ,
            PlayableBreedEnum.Sacrieur ,
            PlayableBreedEnum.Pandawa ,
            PlayableBreedEnum.Sadida,
            PlayableBreedEnum.Zobal ,
            PlayableBreedEnum.Eliotrope,
            PlayableBreedEnum.Huppermage,

           PlayableBreedEnum.Osamodas,
          PlayableBreedEnum.Xelor,
            PlayableBreedEnum.Roublard,
         // PlayableBreedEnum.Steamer,
        };

        public sbyte Id;
        public string Name;
        public string MaleLook;
        public string FemaleLook;
        public List<int> MaleColors;
        public List<int> FemaleColors;

        public CSVDoubleArray SPForIntelligence;
        public CSVDoubleArray SPForAgility;
        public CSVDoubleArray SPForStrength;
        public CSVDoubleArray SPForVitality;
        public CSVDoubleArray SPForWisdom;
        public CSVDoubleArray SPForChance;
        public short StartLifePoints;
        public short StartProspecting;

        public BreedRecord(sbyte id, string name, string malelook, string femalelook, List<int> malecolors, List<int> femalecolors,
            CSVDoubleArray spforintelligence, CSVDoubleArray spforagility, CSVDoubleArray SPForStrength, CSVDoubleArray spforvitality,
            CSVDoubleArray spforwisdom, CSVDoubleArray spforchance, short startlifepoints, short startprospecting)
        {
            this.Id = id;
            this.Name = name;
            this.MaleLook = malelook;
            this.FemaleLook = femalelook;
            this.MaleColors = malecolors;
            this.FemaleColors = femalecolors;
            this.StartLifePoints = startlifepoints;
            this.StartProspecting = startprospecting;
            this.SPForIntelligence = spforintelligence;
            this.SPForAgility = spforagility;
            this.SPForStrength = SPForStrength;
            this.SPForVitality = spforvitality;
            this.SPForWisdom = spforwisdom;
            this.SPForChance = spforchance;
        }

        public uint[] GetThreshold(short actualpoints, StatsBoostEnum statsid)
        {
            uint[][] thresholds = this.GetThresholds(statsid);
            return thresholds[this.GetThresholdIndex((int)actualpoints, thresholds)];
        }
        public int GetThresholdIndex(int actualpoints, uint[][] thresholds)
        {
            int result;
            for (int i = 0; i < thresholds.Length - 1; i++)
            {
                if ((ulong)thresholds[i][0] <= (ulong)((long)actualpoints) && (ulong)thresholds[i + 1][0] > (ulong)((long)actualpoints))
                {
                    result = i;
                    return result;
                }
            }
            result = thresholds.Length - 1;
            return result;
        }
        public uint[][] GetThresholds(StatsBoostEnum statsid)
        {
            uint[][] result = null;
            switch (statsid)
            {
                case StatsBoostEnum.Strength:
                    result = this.SPForStrength.Values;
                    break;
                case StatsBoostEnum.Vitality:
                    result = this.SPForVitality.Values;
                    break;
                case StatsBoostEnum.Wisdom:
                    result = this.SPForWisdom.Values;
                    break;
                case StatsBoostEnum.Chance:
                    result = this.SPForChance.Values;
                    break;
                case StatsBoostEnum.Agility:
                    result = this.SPForAgility.Values;
                    break;
                case StatsBoostEnum.Intelligence:
                    result = this.SPForIntelligence.Values;
                    break;
            }
            return result;
        }
        public static BreedRecord GetBreed(int id)
        {
            return Breeds.Find(x => x.Id == id);
        }

        public static ContextActorLook GetBreedLook(int breedid, bool sex, int cosmeticid, IEnumerable<int> colors)
        {
            var breed = GetBreed(breedid);
            ContextActorLook result = sex ? ContextActorLook.Parse(breed.FemaleLook) : ContextActorLook.Parse(breed.MaleLook);
            result.AddSkin(HeadRecord.GetSkin(cosmeticid));

            int[] simpleColors = VerifiyColors(colors, sex, breed);

            result.SetColors(ContextActorLook.GetConvertedColors(simpleColors));

            return result;
        }
        public static int[] VerifiyColors(IEnumerable<int> colors, bool sex, BreedRecord breed)
        {
            List<int> defaultColors = (!sex) ? breed.MaleColors : breed.FemaleColors;

            if (colors.Count() == 0)
            {
                return defaultColors.ToArray();
            }

            int num = 0;

            List<int> simpleColors = new List<int>();
            foreach (int current in colors)
            {
                if (defaultColors.Count > num)
                {
                    simpleColors.Add((current == -1) ? (int)defaultColors[num] : current);
                }
                num++;
            }

            return simpleColors.ToArray();
        }
        public static ushort GetSkinFromCosmeticId(int cosmecticId)
        {
            return HeadRecord.GetSkin(cosmecticId);
        }
        public IEnumerable<ushort> GetSpellsForLevel(ushort level, List<CharacterSpell> actualspells)
        {
            List<BreedSpellRecord> breedSpells = BreedSpellRecord.GetBreedSpells(this.Id).FindAll(x => x.ObtainLevel <= level);

            foreach (var spell in breedSpells)
            {
                if (actualspells.Find(x => x.SpellId == spell.SpellId) == null)
                    yield return spell.SpellId;
            }

        }

    }

}
