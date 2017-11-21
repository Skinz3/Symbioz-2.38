using Symbioz.Core.DesignPattern;
using Symbioz.Protocol.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Models.Entities.Shortcuts;
using Symbioz.World.Records;
using Symbioz.World.Records.Breeds;
using Symbioz.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Providers
{
    public class CharacterRemodelingProvider : Singleton<CharacterRemodelingProvider>
    {
        public ushort[] BreedSpecialSpells = new ushort[]
        {
            410,412,413,414,416,418,420,421,422,423,424,425,426,430,5847,5392,3277
        };
        public void RemodelCosmetic(CharacterRecord record, ushort cosmeticId)
        {
            var skinId = HeadRecord.GetSkin(record.CosmeticId);
            record.Look.RemoveSkin(skinId);
            record.Look.AddSkin(HeadRecord.GetSkin(cosmeticId));
            record.CosmeticId = cosmeticId;
        }
        public void RemodelColors(CharacterRecord record, int[] colors)
        {
            var dic = ContextActorLook.GetConvertedColorsWithIndex(colors);
            var col = ContextActorLook.GetConvertedColorSortedByIndex(dic);
            col = BreedRecord.VerifiyColors(col, record.Sex, BreedRecord.GetBreed(record.BreedId)).ToList();
            record.Look.SetColors(ContextActorLook.GetConvertedColors(col));
        }
        public bool RemodelName(CharacterRecord record, string newName)
        {
            if (CharacterRecord.NameExist(newName) == false || newName == record.Name)
            {
                record.Name = newName;
                return true;
            }
            return false;
        }
        public void RemodelBreed(CharacterRecord record, sbyte breedId, ushort cosmecticId)
        {
            var currentBreed = BreedRecord.GetBreed(record.BreedId);

            var newBreed = BreedRecord.GetBreed(breedId);

            var level = ExperienceRecord.GetCharacterLevel(record.Exp);

            var shortcuts = record.Shortcuts.OfType<CharacterSpellShortcut>();

            List<ushort> spells = currentBreed.GetSpellsForLevel(200, new List<CharacterSpell>()).ToList();
            spells.AddRange(BreedSpecialSpells);


            foreach (var spell in spells)
            {
                record.Spells.RemoveAll(x => x.SpellId == spell);
                var shortcut = shortcuts.FirstOrDefault(x => x.SpellId == spell);
                record.Shortcuts.Remove(shortcut);
            }

            foreach (var spell in newBreed.GetSpellsForLevel(level, new List<CharacterSpell>()))
            {
                record.Spells.Add(new CharacterSpell(spell, 1));
            }

            var look = record.Sex ? ContextActorLook.Parse(currentBreed.FemaleLook) : ContextActorLook.Parse(currentBreed.MaleLook);
            var newLook = record.Sex ? ContextActorLook.Parse(newBreed.FemaleLook) : ContextActorLook.Parse(newBreed.MaleLook);

            record.SpellPoints += (ushort)(level - 1);

            foreach (var skin in look.Skins)
            {
                record.Look.RemoveSkin(skin);
            }
            foreach (var skin in newLook.Skins)
            {
                record.Look.AddSkin(skin);
            }

            record.Look.SetScale(newLook.Scale);

            ushort headSkin = HeadRecord.GetSkin(record.CosmeticId);
            record.Look.RemoveSkin(headSkin);

            record.Look.AddSkin(HeadRecord.GetSkin(cosmecticId));

            record.BreedId = breedId;



        }
    }
}
