using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YAXLib;

namespace Symbioz.World.Models.Entities.Look
{
    [YAXCustomSerializer(typeof(YAXLookSerializer))]
    public class ContextActorLook
    {
        public const short PetSize = 80;

        public const short AuraSize = 90;

        public ushort BonesId
        {
            get;
            private set;
        }

        public List<ushort> Skins
        {
            get;
            private set;
        }

        public List<int> Colors
        {
            get;
            private set;
        }

        public List<short> Scales
        {
            get;
            private set;
        }
        public short Scale
        {
            get
            {
                return (short)(Scales.Count == 0 ? 100 : Scales[0]);
            }
        }
        public List<ContextSubEntity> SubEntities
        {
            get;
            private set;
        }

        public bool IsRiding
        {
            get
            { return this.SubEntities.Find(x => x.Category == SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_MOUNT_DRIVER) != null; }
        }

        public ContextActorLook()
        {
            this.Skins = new List<ushort>();
            this.Colors = new List<int>();
            this.Scales = new List<short>();
            this.SubEntities = new List<ContextSubEntity>();
        }
        public ContextActorLook(ushort bonesId, IEnumerable<ushort> skins, IEnumerable<int> colors, IEnumerable<short> scales,
          IEnumerable<ContextSubEntity> subEntity)
        {
            this.BonesId = bonesId;
            this.Skins = skins.ToList();
            this.Colors = colors.ToList();
            this.Scales = scales.ToList();
            this.SubEntities = subEntity.ToList();
        }

        public ContextActorLook(EntityLook look)
        {
            this.BonesId = look.bonesId;
            this.Skins = look.skins.ToList();
            this.Colors = look.indexedColors.ToList();
            this.Scales = look.scales.ToList();
            this.SubEntities = look.subentities.ToList().ConvertAll<ContextSubEntity>(x => new ContextSubEntity(x));
        }
        public void SetSkins(IEnumerable<ushort> value)
        {
            ActiveLook().Skins = value.ToList();
        }
        public void AddSkin(ushort value)
        {
            var look = ActiveLook();
            if (!look.Skins.Contains(value))
                look.Skins.Add(value);
        }
        public void AddScale(short value)
        {
            short newScale = (short)(Scale + value);
            SetScale(newScale);
        }
        public void SetScale(short value)
        {
            var look = ActiveLook();

            if (look.Scales.Count == 0)
            {
                look.Scales.Add(value);
            }
            else
            {
                ActiveLook().Scales[0] = value;
            }
        }
        public void RemoveSkin(ushort value)
        {
            ActiveLook().Skins.RemoveAll(x => x == value);
        }
        public void SetBones(ushort value)
        {
            ActiveLook().BonesId = value;
        }
        public void SetColors(IEnumerable<int> colors)
        {
            ActiveLook().Colors = colors.ToList();
        }
        public int RemoveSubEntities(ContextActorLook look, SubEntityBindingPointCategoryEnum category)
        {
            return look.SubEntities.RemoveAll(x => x.Category == category);
        }
        public ContextSubEntity GetSubEntity(SubEntityBindingPointCategoryEnum category)
        {
            return SubEntities.Find(x => x.Category == category);
        }
        public void AddPetSkin(ushort skinId, short scale)
        {
            ActiveLook().SubEntities.Add(new ContextSubEntity(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_PET, 0, BonesLook(skinId, scale)));
        }
        public void RemovePetSkin()
        {
            ActiveLook().SubEntities.RemoveAll(x => x.Category == SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_PET);
        }
        public void AddAura(ushort bonesId)
        {
            this.SubEntities.Add(new ContextSubEntity(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_BASE_FOREGROUND,
                0, BonesLook(bonesId, AuraSize)));
        }
        public bool RemoveAura()
        {
            int count = this.RemoveSubEntities(this, SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_BASE_FOREGROUND);
            return count > 0;
        }
        public ContextActorLook GetMountLook(ContextActorLook mountLook)
        {
            ContextActorLook newLook = mountLook.Clone();
            newLook.Colors = GetConvertedColors(mountLook.Colors.ToArray()).ToList();
            ContextSubEntity actorSub = new ContextSubEntity(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_MOUNT_DRIVER, 0,
                new ContextActorLook(2, this.Skins, this.Colors, this.Scales, this.SubEntities));
            newLook.SubEntities.Add(actorSub);
            return newLook;
        }
        public ContextActorLook GetMountDriverLook()
        {
            if (!this.IsRiding)
            {
                Logger.Write<ContextActorLook>("Unable to get MountDriverLook to this player, he has no subentity look, maybe an admin?", ConsoleColor.DarkRed);
            }
            return this.GetSubEntity(SubEntityBindingPointCategoryEnum.HOOK_POINT_CATEGORY_MOUNT_DRIVER).SubActorLook;
        }
        public override bool Equals(object obj)
        {
            ContextActorLook look = obj as ContextActorLook;

            if (obj == null)
                return false;

            return look.BonesId == this.BonesId && look.Colors.SequenceEqual(look.Colors) && look.Scales.SequenceEqual(this.Scales) && look.Skins.SequenceEqual(this.Skins)
                 && this.SubEntities.SequenceEqual(this.SubEntities);
        }
        public ContextActorLook ActiveLook()
        {
            if (!IsRiding)
                return this;
            else
                return GetMountDriverLook();
        }
        public EntityLook ToEntityLook()
        {
            return new EntityLook(BonesId, Skins.ToArray(), Colors.ToArray(), Scales.ToArray(), SubEntities.ConvertAll<SubEntity>(x => x.ToSubEntity()).ToArray());
        }

        public static ContextActorLook Deserialize(string str)
        {
            return Parse(str);
        }

        public static ContextActorLook Parse(string str)
        {
            if (str == string.Empty)
                return null;
            #region Code
            if (string.IsNullOrEmpty(str) || str[0] != '{')
            {
                throw new System.Exception("Incorrect EntityLook format : " + str);
            }
            int i = 1;
            int num = str.IndexOf('|');
            if (num == -1)
            {
                num = str.IndexOf("}");
                if (num == -1)
                {
                    throw new System.Exception("Incorrect EntityLook format : " + str);
                }
            }
            short bones = short.Parse(str.Substring(i, num - i));
            i = num + 1;
            short[] skins = new short[0];
            if ((num = str.IndexOf('|', i)) != -1 || (num = str.IndexOf('}', i)) != -1)
            {
                skins = Symbioz.Core.Extensions.ParseCollection<short>(str.Substring(i, num - i), new Func<string, short>(short.Parse));
                i = num + 1;
            }
            Tuple<int, int>[] source = new Tuple<int, int>[0];
            if ((num = str.IndexOf('|', i)) != -1 || (num = str.IndexOf('}', i)) != -1)
            {
                source = Symbioz.Core.Extensions.ParseCollection<Tuple<int, int>>(str.Substring(i, num - i), new Func<string, Tuple<int, int>>(ParseIndexedColor));
                i = num + 1;
            }
            short[] scales = new short[0];
            if ((num = str.IndexOf('|', i)) != -1 || (num = str.IndexOf('}', i)) != -1)
            {
                scales = Symbioz.Core.Extensions.ParseCollection<short>(str.Substring(i, num - i), new Func<string, short>(short.Parse));
                i = num + 1;
            }
            System.Collections.Generic.List<ContextSubEntity> list = new System.Collections.Generic.List<ContextSubEntity>();
            while (i < str.Length && str[i] != '}')
            {
                int num2 = str.IndexOf('@', i, 3);
                int num3 = str.IndexOf('=', num2 + 1, 3);
                byte category = byte.Parse(str.Substring(i, num2 - i));
                byte b = byte.Parse(str.Substring(num2 + 1, num3 - (num2 + 1)));
                int num4 = 0;
                int num5 = num3 + 1;
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                do
                {
                    stringBuilder.Append(str[num5]);
                    if (str[num5] == '{')
                    {
                        num4++;
                    }
                    else
                    {
                        if (str[num5] == '}')
                        {
                            num4--;
                        }
                    }
                    num5++;
                }
                while (num4 > 0);
                list.Add(new ContextSubEntity((SubEntityBindingPointCategoryEnum)category,
                 (sbyte)b, Parse(stringBuilder.ToString())));
                i = num5 + 1;
            }
            List<int> colors = new List<int>();
            foreach (var color in source)
            {
                colors.Add(color.Item2);
            }
            return new ContextActorLook((ushort)bones, skins.Select(entry => (ushort)entry).ToList(), colors.ToList(), scales.ToList(), list);
            #endregion
        }
        /// <summary>
        /// Retourne le look sous le format string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ConvertToString(this);
        }
        public static string ConvertToString(ContextActorLook look)
        {
            #region Code
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append("{");
            int num = 0;
            stringBuilder.Append(look.BonesId);
            if (look.Skins == null || !look.Skins.Any<ushort>())
            {
                num++;
            }
            else
            {
                stringBuilder.Append("|".ConcatCopy(num + 1));
                num = 0;
                stringBuilder.Append(string.Join<ushort>(",", look.Skins));
            }
            if (look.Colors == null)
            {
                num++;
            }
            else
            {
                stringBuilder.Append("|".ConcatCopy(num + 1));
                num = 0;

                List<string> values = new List<string>();

                int i = 0;
                foreach (var color in look.Colors)
                {
                    i++;
                    values.Add(i + "=" + color);
                }

                stringBuilder.Append(string.Join(",", values));

            }
            if (look.Scales == null)
            {
                num++;
            }
            else
            {
                stringBuilder.Append("|".ConcatCopy(num + 1));
                num = 0;
                stringBuilder.Append(string.Join<short>(",", look.Scales));
            }
            if (look.SubEntities.Count() == 0)
            {
                num++;
            }
            else
            {
                List<string> subEntitiesAsString = new List<string>();
                foreach (var sub in look.SubEntities)
                {
                    StringBuilder subBuilter = new System.Text.StringBuilder();
                    subBuilter.Append((sbyte)sub.Category);
                    subBuilter.Append("@");
                    subBuilter.Append(sub.BindingPointIndex);
                    subBuilter.Append("=");
                    subBuilter.Append(ConvertToString(sub.SubActorLook));
                    subEntitiesAsString.Add(subBuilter.ToString());
                }
                stringBuilder.Append("|".ConcatCopy(num + 1));
                stringBuilder.Append(string.Join<string>(",",
                    from entry in subEntitiesAsString
                    select entry));
            }
            stringBuilder.Append("}");
            return stringBuilder.ToString();
            #endregion
        }



        private static Tuple<int, int> ParseIndexedColor(string str)
        {
            int num = str.IndexOf("=");
            bool flag = str[num + 1] == '#';
            int item = int.Parse(str.Substring(0, num));
            int item2 = int.Parse(str.Substring(num + (flag ? 2 : 1), str.Length - (num + (flag ? 2 : 1))), flag ? System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Integer);
            return Tuple.Create<int, int>(item, item2);
        }

        public static int[] GetConvertedColors(IEnumerable<int> colors)
        {
            int[] col = new int[colors.Count()];
            for (int i = 0; i < colors.Count(); i++)
            {
                var color = Color.FromArgb(colors.ToArray()[i]);
                col[i] = i + 1 << 24 | color.ToArgb() & 16777215;
            }
            return col;
        }
        public static Dictionary<sbyte, int> GetConvertedColorsWithIndex(IEnumerable<int> data)
        {
            var colors = data.ToArray();

            Dictionary<sbyte, int> col = new Dictionary<sbyte, int>();
            for (int i = 0; i < colors.Count(); i++)
            {
                var color = Color.FromArgb(colors[i]);
                var colorValue = i + 1 << 24 | color.ToArgb() & 16777215;
                col.Add((sbyte)(colors[i] >> 24), colorValue);
            }
            return col;
        }
        public static List<int> GetConvertedColorSortedByIndex(Dictionary<sbyte, int> convertedColorsWithIndex)
        {
            List<int> colors = new List<int>();

            for (sbyte i = 1; i <= 5; i++)
            {

                if (convertedColorsWithIndex.ContainsKey(i))
                {
                    colors.Add(convertedColorsWithIndex[i]);
                }
                else
                {
                    colors.Add(-1);
                }

            }
            return colors;
        }
        public static ContextActorLook BonesLook(ushort bonesId, short scale)
        {
            return new ContextActorLook(bonesId, new List<ushort>(), new List<int>(), new List<short>() { scale }, new List<ContextSubEntity>());
        }
        public ContextActorLook Clone()
        {
            List<ContextSubEntity> subentities = new List<ContextSubEntity>();
            foreach (var sub in SubEntities)
            {
                subentities.Add(sub.Clone());
            }
            return new ContextActorLook(BonesId, new List<ushort>(Skins), new List<int>(Colors),
                new List<short>(Scales), new List<ContextSubEntity>(subentities));
        }

    }
    public class YAXLookSerializer : ICustomSerializer<ContextActorLook>
    {
        public ContextActorLook DeserializeFromAttribute(XAttribute attrib)
        {
            return ContextActorLook.Parse(attrib.Value);
        }

        public ContextActorLook DeserializeFromElement(XElement element)
        {
            return ContextActorLook.Parse(element.Value);
        }

        public ContextActorLook DeserializeFromValue(string value)
        {
            return ContextActorLook.Parse(value);
        }

        public void SerializeToAttribute(ContextActorLook objectToSerialize, XAttribute attrToFill)
        {
            attrToFill.Value = objectToSerialize.ToString();
        }

        public void SerializeToElement(ContextActorLook objectToSerialize, XElement elemToFill)
        {
            elemToFill.Value = objectToSerialize.ToString();
        }

        public string SerializeToValue(ContextActorLook objectToSerialize)
        {
            return objectToSerialize.ToString();
        }
    }
}
