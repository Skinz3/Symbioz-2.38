using Symbioz.Protocol.Types;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Symbioz.Protocol.Enums;
using System.Threading.Tasks;
using Symbioz.Protocol.Messages;
using Symbioz.World.Records;
using Symbioz.World.Models.Items;
using Symbioz.World.Models.Effects;
using Symbioz.World.Providers;
using Symbioz.World.Providers.Criterias;
using Symbioz.World.Providers.Items;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities.Look;
using Symbioz.World.Records.Items;
using Symbioz.World.Records.Characters;
using Symbioz.World.Models.Fights.FightModels;
using Symbioz.World.Records.Monsters;
using Symbioz.World.Providers.Fights.Results;

namespace Symbioz.World.Models.Entities
{
    /// <summary>
    /// Représente le panel inventaire d'un joueur
    /// </summary>
    public class Inventory : ItemCollection<CharacterItemRecord>
    {
        /// <summary>
        /// Taille du familier équipé.
        /// </summary>
        public const short PetSize = 80;
        /// <summary>
        /// Taille du montilier équipé.
        /// </summary>
        public const short PetMountSize = 100;
        /// <summary>
        /// Quantitée maximum de kamas autorisé dans l'inventaire
        /// </summary>
        public const int MaxKamas = 2000000000;

        /// <summary>
        /// Position d'équipement des dofus
        /// </summary>
        public static CharacterInventoryPositionEnum[] DofusPositions = new CharacterInventoryPositionEnum[]
        {
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_1,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_2,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_3,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_4,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_5,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_6,
        };

        /// <summary>
        /// Positions d'équipement des anneaux
        /// </summary>
        public static CharacterInventoryPositionEnum[] RingPositions = new CharacterInventoryPositionEnum[]
        {
            CharacterInventoryPositionEnum.INVENTORY_POSITION_RING_LEFT,
            CharacterInventoryPositionEnum.INVENTORY_POSITION_RING_RIGHT,
        };

        /// <summary>
        /// Personnage lié a cet inventaire
        /// </summary>
        private Character Character
        {
            get;
            set;
        }

        /// <summary>
        /// Poid actuel des objets dans l'inventaire
        /// </summary>
        public uint CurrentWeight
        {
            get
            {
                uint currWeight = 0;
                foreach (CharacterItemRecord item in GetItems().ToList())
                {
                    currWeight += (uint)(ItemRecord.GetItem(item.GId).Weight * item.Quantity);
                }
                return currWeight;
            }
        }

        public bool HasWeaponEquiped
        {
            get
            {
                return GetWeapon() != null;
            }
        }
        /// <summary>
        /// Poid maximal autorisé
        /// </summary>
        public uint TotalWeight
        {
            get
            {
                return FormulasProvider.Instance.TotalWeight(Character);
            }
        }
        public List<CharacterMountRecord> Mounts
        {
            get;
            private set;
        }
        public CharacterMountRecord Mount
        {
            get
            {
                return Mounts.FirstOrDefault(x => x.Setted);
            }
        }
        public bool HasMountEquiped
        {
            get
            {
                return Mount != null;
            }
        }

        /// <summary>
        /// Constructeur 
        /// </summary>
        /// <param name="character"></param>
        public Inventory(Character character)
            : base(CharacterItemRecord.GetCharacterItems(character.Id))
        {
            this.Character = character;
            this.OnItemAdded += Inventory_OnItemAdded;
            this.OnItemRemoved += Inventory_OnItemRemoved;

            this.OnItemsAdded += Inventory_OnItemsAdded;
            this.OnItemsRemoved += Inventory_OnItemsRemoved;

            this.OnItemQuantityChanged += Inventory_OnItemQuantityChanged;
            this.OnItemsQuantityChanged += Inventory_OnItemsQuantityChanged;

            this.Mounts = CharacterMountRecord.GetCharacterMounts(Character.Id);

        }

        void Inventory_OnItemsQuantityChanged(IEnumerable<CharacterItemRecord> obj)
        {
            foreach (var item in obj)
            {
                item.UpdateElement();
            }

            Character.Client.Send(new ObjectsQuantityMessage(Array.ConvertAll(obj.ToArray(), x => x.GetObjectItemQuantity())));
            RefreshWeight();
        }

        void Inventory_OnItemQuantityChanged(CharacterItemRecord arg1, uint arg2)
        {
            arg1.UpdateElement();
            UpdateItemQuantity(arg1);
            RefreshWeight();
        }
        /// <summary>
        /// Lorsque plusieurs objects sont supprimés
        /// </summary>
        /// <param name="obj"></param>
        void Inventory_OnItemsRemoved(IEnumerable<CharacterItemRecord> obj)
        {
            foreach (var item in obj)
            {
                item.RemoveElement();
                Character.GeneralShortcutBar.OnItemRemoved(item);
            }
            Character.Client.Send(new ObjectsDeletedMessage(Array.ConvertAll(obj.ToArray(), x => x.UId)));
            RefreshWeight();
            Character.RefreshShortcuts();
        }
        /// <summary>
        /// Lorsque plusieurs objects sont ajoutés
        /// </summary>
        /// <param name="obj"></param>
        void Inventory_OnItemsAdded(IEnumerable<CharacterItemRecord> obj)
        {
            foreach (var item in obj)
            {
                item.UId = ItemUIdPopper.PopUID();
                item.AddElement();

            }
            Character.Client.Send(new ObjectsAddedMessage(Array.ConvertAll(obj.ToArray(), x => x.GetObjectItem())));
            RefreshWeight();

        }
        /// <summary>
        /// Lorsqu'un objet est supprimé
        /// </summary>
        /// <param name="obj"></param>
        void Inventory_OnItemRemoved(CharacterItemRecord obj)
        {

            obj.RemoveElement();
            Character.Client.Send(new ObjectDeletedMessage(obj.UId));
            Character.GeneralShortcutBar.OnItemRemoved(obj);
            RefreshWeight();
        }
        /// <summary>
        /// Lorsqu'un objet est ajouté
        /// </summary>
        /// <param name="obj"></param>
        void Inventory_OnItemAdded(CharacterItemRecord obj)
        {
            obj.UId = ItemUIdPopper.PopUID();

            if (ItemGenerationProvider.IsHandled(obj.Template.TypeEnum))
                ItemGenerationProvider.InitItem(obj, Character);

            obj.AddElement();
            Character.Client.Send(new ObjectAddedMessage(obj.GetObjectItem()));
            RefreshWeight();
        }
        public void UnequipAll()
        {
            foreach (var item in GetEquipedItems())
            {
                SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item.Quantity);
            }
        }
        public bool Unequip(CharacterInventoryPositionEnum position)
        {
            var item = GetEquipedItem(position);

            if (item != null)
            {
                SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item.Quantity);
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Ajoute un Item
        /// </summary>
        /// <param name="gid">Id de l'item</param>
        /// <param name="quantity">Quantitée</param>
        /// <returns></returns>
        public CharacterItemRecord AddItem(ushort gid, uint quantity, bool perfect = false)
        {
            var template = ItemRecord.GetItem(gid);
            if (template != null)
            {
                var obj = template.GetCharacterItem(Character.Id, quantity, perfect);
                base.AddItem(obj);
                return obj;
            }
            else
                return null;

        }
        public CharacterItemRecord GetFirstItem(ItemTypeEnum type)
        {
            return GetItems().FirstOrDefault(x => x.Template.TypeEnum == type);
        }
        /// <summary>
        /// Permet de récuperer le premier item de l'inventaire possédant le GId indiqué
        /// </summary>
        /// <param name="gid"></param>
        //s/ <returns></returns>
        public CharacterItemRecord GetFirstItem(ushort gid, uint minimumQuantity)
        {
            return GetItems().FirstOrDefault(x => x.GId == gid && x.Quantity >= minimumQuantity);
        }
        /// <summary>
        /// Rafraichit l'inventaire (Lent)
        /// </summary>
        public void Refresh()
        {
            Character.Client.Send(new InventoryContentMessage(this.GetObjectsItems(), (uint)Character.Record.Kamas));
            RefreshWeight();
        }
        /// <summary>
        /// Rafraichit les données des items et le poid de l'inventaire
        /// </summary>
        public void RefreshWeight()
        {
            Character.Client.Send(new InventoryWeightMessage(CurrentWeight, TotalWeight));

        }
        /// <summary>
        /// Rafraichit la quantitée de kamas dans l'inventaire
        /// </summary>
        public void RefreshKamas()
        {
            Character.Client.Send(new KamasUpdateMessage(Character.Record.Kamas));
        }
        /// <summary>
        /// Permet d'obtenir un item supposé équivalent a un deuxieme item en fonction de ses effets (compare donc les effets de deux items)
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="effects"></param>
        /// <returns></returns>
        protected override CharacterItemRecord GetSameItem(ushort gid, List<Effect> effects)
        {
            var items = GetItems();
            return items.FirstOrDefault(x => x.GId == gid && SameEffects(effects, x.Effects) && x.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
        }
        public void RemoveForbiddenItems() /// TEMPORARY
        {
            foreach (var item in GetItems())
            {
                if (item.HasEffect<EffectMination>())
                {
                    EffectMination effect = item.FirstEffect<EffectMination>();

                    if (effect != null)
                    {
                        if (MinationLoot.CantBeEquiped(effect.MonsterId))
                        {
                            if (item.IsEquiped())
                            {
                                UnequipItem(item, item.Quantity);
                            }
                            RemoveItem(item, item.Quantity);
                            return;
                        }
                    }

                }
            }

        }
        /// <summary>
        /// Appelée lorsque la position d'un item a été modifié
        /// </summary>
        /// <param name="item"></param>
        /// <param name="lastPosition"></param>
        private void OnItemMoved(CharacterItemRecord item, CharacterInventoryPositionEnum lastPosition)
        {
            bool flag = lastPosition != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
            bool flag2 = item.IsEquiped();


            if (flag2 && !flag)
                ItemEffectsProvider.AddEffects(Character, item.Effects);

            if (!flag2 && flag)
                ItemEffectsProvider.RemoveEffects(Character, item.Effects);

            if (item.AppearanceId != 0)
            {
                UpdateLook(item, flag2);
            }

            if (item.Template.HasSet) // Si il y a une panoplie
            {
                int num = CountItemSetEquiped(item.Template.ItemSet);

                if (flag2)  //Si on vient d'équiper l'objet
                {
                    ApplyItemSetEffects(item.Template.ItemSet, num, flag2);
                }
                else
                {
                    ApplyItemSetEffects(item.Template.ItemSet, num, flag2);
                }
            }
        }
        /// <summary>
        /// Met a jour le look du joueur ayant équipé un objet possédant une AppearenceId.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="equiped"></param>
        private void UpdateLook(CharacterItemRecord item, bool equiped)
        {
            switch (item.Template.TypeEnum)
            {
                case ItemTypeEnum.FAMILIER:
                    UpdatePetLook(item, equiped);
                    break;
                case ItemTypeEnum.MONTILIER:
                    UpdatePetMountLook(item, equiped);
                    break;
                default:
                    if (equiped)
                        Character.Look.AddSkin(item.AppearanceId);
                    else
                        Character.Look.RemoveSkin(item.AppearanceId);
                    break;
            }
        }
        private void UpdatePetLook(CharacterItemRecord item, bool equiped)
        {
            if (equiped)
            {
                Character.Look.AddPetSkin(item.AppearanceId, PetSize);
            }
            else
            {
                Character.Look.RemovePetSkin();
            }
        }
        public void OnItemModified(CharacterItemRecord item)
        {
            Character.Client.Send(new ObjectModifiedMessage(item.GetObjectItem()));
        }
        private void UpdatePetMountLook(CharacterItemRecord item, bool equiped)
        {
            if (equiped)
            {
                ContextActorLook look = ContextActorLook.BonesLook(item.AppearanceId, PetMountSize);
                look.SetColors(Character.Look.Colors.Skip(2).Take(3));
                Character.Look = Character.Look.GetMountLook(look);
            }
            else
            {
                Character.Look = Character.Look.GetMountDriverLook();
                Character.Look.SetBones(1);
            }
        }
        private void ApplyItemSetEffects(ItemSetRecord itemSet, int count, bool equiped)
        {
            if (equiped)
            {
                if (count >= 2)
                {
                    if (count >= 3)
                    {
                        ItemEffectsProvider.RemoveEffects(Character, itemSet.GetSetEffects(count - 2));
                    }
                    ItemEffectsProvider.AddEffects(Character, itemSet.GetSetEffects(count - 1));
                    OnSetUpdated(itemSet, count - 1);
                }
            }
            else
            {
                if (count >= 1)
                {
                    if (count >= 2)
                    {
                        ItemEffectsProvider.AddEffects(Character, itemSet.GetSetEffects(count - 1));
                    }
                    ItemEffectsProvider.RemoveEffects(Character, itemSet.GetSetEffects(count));
                    OnSetUpdated(itemSet, count);
                }

            }
        }
        public CharacterItemRecord GetWeapon()
        {
            return GetEquipedItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON);
        }
        /// <summary>
        /// Lorsque la panoplie a été modifiée
        /// </summary>
        /// <param name="set"></param>
        /// <param name="num"></param>
        private void OnSetUpdated(ItemSetRecord set, int num)
        {
            Character.Client.Send(new SetUpdateMessage((ushort)set.Id, set.Items.ToArray(),
                set.GetSetEffects(num).ConvertAll<ObjectEffectInteger>(x => (ObjectEffectInteger)x.GetObjectEffect()).ToArray()));
        }
        /// <summary>
        /// Permet d'équiper un item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="position"></param>
        /// <param name="quantity"></param>
        private void EquipItem(CharacterItemRecord item, CharacterInventoryPositionEnum position, uint quantity)
        {
            CharacterItemRecord equiped = GetEquipedItem(position);

            CharacterInventoryPositionEnum lastPosition = item.PositionEnum;

            if (equiped != null)
            {
                UnequipItem(equiped, quantity);
                OnObjectMoved(equiped, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
            }

            if (item.Quantity == 1)
            {
                item.PositionEnum = position;
            }
            else
            {
                CharacterItemRecord newItem = item.Cut(quantity, position);
                AddItem(newItem);
                UpdateItemQuantity(item);
            }

            item.UpdateElement();

            OnItemMoved(item, lastPosition);

            foreach (var item2 in GetEquipedItems())
            {
                if (item != item2 && !CriteriaProvider.EvaluateCriterias(Character.Client, item2.Template.Criteria))
                    SetItemPosition(item2.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item2.Quantity);
            }

        }
        /// <summary>
        /// Permet de déséquiper un item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        private void UnequipItem(CharacterItemRecord item, uint quantity)
        {
            if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
            {
                CharacterItemRecord sameItem = GetSameItem(item.GId, item.Effects);
                CharacterInventoryPositionEnum lastPosition = item.PositionEnum;

                if (sameItem != null)
                {
                    if (item.UId != sameItem.UId)
                    {
                        item.PositionEnum = CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
                        sameItem.Quantity += quantity;
                        sameItem.UpdateElement();
                        this.UpdateItemQuantity(sameItem);
                        this.RemoveItem(item.UId, item.Quantity);
                    }
                    else
                    {
                        Character.ReplyError("Error while moving item");
                    }

                }
                else
                {
                    item.PositionEnum = CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED;
                    item.UpdateElement();
                }

                OnItemMoved(item, lastPosition);
            }
        }
        /// <summary>
        /// Verifie que deux items (Anneaux , Dofus) n'ont pas été équipée en même temps
        /// </summary>
        /// <param name="item"></param>
        /// <param name="position"></param>
        /// <param name="checker"></param>
        /// <returns></returns>
        bool CheckStacks(CharacterItemRecord item, CharacterInventoryPositionEnum position, CharacterInventoryPositionEnum[] checker)
        {
            foreach (CharacterInventoryPositionEnum pos in checker)
            {
                var current = GetEquipedItem(pos);
                if (current != null && current.GId == item.GId)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Compte le nombre d'items équipés d'une panoplie
        /// </summary>
        /// <param name="itemSet"></param>
        /// <returns></returns>
        private int CountItemSetEquiped(ItemSetRecord itemSet)
        {
            return this.GetEquipedItems().Count((CharacterItemRecord entry) => itemSet.Items.Contains(entry.GId));
        }
        public int MaximumItemSetCount()
        {
            int max = 0;
            foreach (var item in GetEquipedItems())
            {
                var itemSet = ItemSetRecord.GetItemSet(item.GId);

                if (itemSet != null)
                {
                    int current = CountItemSetEquiped(itemSet);

                    if (current > max)
                    {
                        max = current;
                    }
                }
            }
            return max - 1;
        }
        /// <summary>
        /// Récupère tout les items équipés
        /// </summary>
        /// <returns></returns>
        public CharacterItemRecord[] GetEquipedItems()
        {
            return GetItems().Where(x => x.IsEquiped()).ToArray();
        }
        /// <summary>
        /// Récupère un item équipé en fonction de sa position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private CharacterItemRecord GetEquipedItem(CharacterInventoryPositionEnum position)
        {
            return GetItems().FirstOrDefault(x => x.PositionEnum == position);
        }
        /// <summary>
        /// Lorsque la quantité d'un item a été modifiée
        /// </summary>
        /// <param name="item"></param>
        private void UpdateItemQuantity(CharacterItemRecord item)
        {
            Character.Client.Send(new ObjectQuantityMessage(item.UId, item.Quantity));

        }
        /// <summary>
        /// Lors d'une erreur lié a l'inventaire
        /// </summary>
       // / <param name="error"></param>
        void OnError(ObjectErrorEnum error)
        {
            Character.Client.Send(new ObjectErrorMessage((sbyte)error));
        }
        void OnLivingObjectEquipedDirectly()
        {
            Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 161);
        }
        /// <summary>
        /// Retourne la somme de l'effet donné pour les objets équipés
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        public ushort GetCumulatedIntegerEffect(EffectsEnum effect)
        {
            ushort result = 0;
            foreach (var item in GetEquipedItems())
            {
                result += item.FirstEffect<EffectInteger>(effect).Value;
            }
            return result;
        }
        /// <summary>
        /// Permet de définir la nouvelle position d'un item
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="position"></param>
        /// <param name="quantity"></param>
        public void SetItemPosition(uint uid, CharacterInventoryPositionEnum position, uint quantity)
        {
            var item = GetItem(uid);
            if (item != null)
            {
                if (position != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                {
                    if (Character.Level < item.Template.Level)
                    {
                        OnError(ObjectErrorEnum.LEVEL_TOO_LOW);
                        return;
                    }
                    if (!CriteriaProvider.EvaluateCriterias(Character.Client, item.Template.Criteria))
                    {
                        OnError(ObjectErrorEnum.CRITERIONS);
                        return;
                    }
                    if (DofusPositions.Contains((CharacterInventoryPositionEnum)item.Position) && DofusPositions.Contains((CharacterInventoryPositionEnum)position))
                        return;

                    if (CheckStacks(item, position, RingPositions) && item.Template.HasSet)
                    {
                        OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE);
                        return;
                    }
                    if (CheckStacks(item, position, DofusPositions))
                    {
                        OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE);
                        return;
                    }
                    if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    {
                        OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE); 
                        return;
                    }

                    if (item.Template.TypeEnum == ItemTypeEnum.OBJET_VIVANT)
                    {
                        ItemTypeEnum livingObjectCategory = (ItemTypeEnum)(item.FirstEffect<EffectInteger>(EffectsEnum.Effect_LivingObjectCategory).Value);

                        var targeted = GetEquipedItem(position);

                        if (targeted == null)
                        {
                            OnLivingObjectEquipedDirectly();
                            return;
                        }
                        if (targeted.Template.TypeEnum != livingObjectCategory)
                        {
                            OnError(ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR);
                            return;
                        }
                        if (targeted.IsAssociated)
                        {
                            OnError(ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR);
                            return;
                        }
                        if (item.Quantity > 1)
                        {
                            CharacterItemRecord newItem = item.Cut(1, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
                            this.AssociateLiving(item, targeted);
                            AddItem(newItem);
                            UpdateItemQuantity(item);
                        }
                        else
                            this.AssociateLiving(item, targeted);

                        return;
                    }
                    if (item.Template.TypeEnum == ItemTypeEnum.OBJET_D_APPARAT)
                    {
                        var targeted = GetEquipedItem(position);

                        if (targeted == null)
                        {
                            OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE);
                            return;
                        }
                        if (targeted.Template.TypeId != item.FirstEffect<EffectInteger>(EffectsEnum.Effect_Associate).Value)
                        {
                            return;
                        }
                        if (targeted.IsAssociated)
                        {
                            OnError(ObjectErrorEnum.SYMBIOTIC_OBJECT_ERROR);
                            return;
                        }
                        if (item.Quantity > 1)
                        {
                            CharacterItemRecord newItem = item.Cut(1, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
                            this.AssociateCompatibility(item, targeted);
                            AddItem(newItem);
                            UpdateItemQuantity(item);
                        }
                        else
                            this.AssociateCompatibility(item, targeted);
                        return;
                    }
                    if (item.HasEffect<EffectMination>())
                    {
                        EffectMination effect = item.FirstEffect<EffectMination>();

                        var monster = effect.GetTemplate();


                        var level = monster.GetGrade(effect.GradeId).Level;
                        level = (ushort)(level > 200 ? 200 : level);

                        if (level > Character.Level)
                        {
                            Character.Reply("Vous devez être niveau " + level + " pour équiper cette pierre.");
                            return;
                        }
                        if (monster.IsBoss && Character.Level < 180)
                        {
                            Character.Reply("Vous devez être niveau 180 pour équiper un boss de donjon.");
                            return;
                        }
                        if (monster.IsMiniBoss && Character.Level < 150)
                        {
                            Character.Reply("Vous devez être niveau 150 pour équiper un miniboss.");
                            return;
                        }
                        Character.SpellAnim(6021);
                    }
                    if (item.Template.TypeEnum == ItemTypeEnum.MONTILIER || item.Template.TypeEnum == ItemTypeEnum.FAMILIER)
                    {
                        if (HasMountEquiped && Mount.Toggled)
                        {
                            ToggleMount();
                        }
                    }
                    this.CheckTwoHandeds(position, item);

                    EquipItem(item, position, quantity);
                }
                else
                {
                    if (item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    {
                        OnError(ObjectErrorEnum.CANNOT_EQUIP_HERE);
                        return;
                    }
                    else
                    {
                        UnequipItem(item, quantity);
                    }

                }
                OnObjectMoved(item, position);
                RefreshWeight();
                Character.RefreshActorOnMap();
                Character.RefreshStats();
            }
        }

        private void AssociateCompatibility(CharacterItemRecord item, CharacterItemRecord targeted)
        {
            RemoveItem(item, item.Quantity);
            UpdateItemAppearence(targeted, item.AppearanceId);
            targeted.AddEffectInteger(EffectsEnum.Effect_ChangeApparence1176, item.Template.Id);
            OnItemModified(targeted);
            RefreshWeight();
            Character.RefreshActorOnMap();
            Character.RefreshStats();
            Character.Client.Send(new WrapperObjectAssociatedMessage(targeted.UId));

        }
        private bool CheckPassiveStacking(CharacterItemRecord item)
        {
            var item2 = GetItems().FirstOrDefault(x => x.UId != item.UId && x.GId == item.GId && SameEffects(item.Effects, x.Effects) && x.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);

            if (item2 != null)
            {
                item2.Quantity += item.Quantity;
                item2.UpdateElement();
                OnItemModified(item2);
                RemoveItem(item, item.Quantity);
                return true;

            }
            return false;
        }
        public void DissociateCompatibility(CharacterItemRecord item, CharacterInventoryPositionEnum pos)
        {
            EffectInteger effect = item.FirstEffect<EffectInteger>(EffectsEnum.Effect_ChangeApparence1176);

            if (effect != null)
            {
                AddItem(effect.Value, item.Quantity);

                if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    UpdateItemAppearence(item, item.Template.AppearanceId);
                else
                    item.AppearanceId = item.Template.AppearanceId;

                item.RemoveAllEffects(EffectsEnum.Effect_ChangeApparence1176);

                if (item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                {
                    if (!CheckPassiveStacking(item))
                        OnItemModified(item);
                }
                else
                    OnItemModified(item);

                RefreshWeight();
                Character.RefreshActorOnMap();
                Character.RefreshStats();
            }

        }
        private void AssociateLiving(CharacterItemRecord livingObject, CharacterItemRecord targeted)
        {
            LivingObjectRecord record = LivingObjectRecord.GetLivingObjectDatas(livingObject.GId);

            if (record != null)
            {
                if (record.ItemType == targeted.Template.TypeId)
                {
                    targeted.AddEffectDice(EffectsEnum.Effect_LivingObjectId, livingObject.Template.Id,
                        livingObject.Template.Id, livingObject.Template.Id);

                    foreach (var effect in livingObject.Effects)
                    {
                        targeted.AddEffect(effect);
                    }

                    OnItemModified(targeted);
                    RefreshWeight();

                    if (targeted.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    {
                        UpdateItemAppearence(targeted, livingObject.AppearanceId);
                        Character.RefreshActorOnMap();
                    }

                    RemoveItem(livingObject, livingObject.Quantity);

                }
            }
        }
        public void ToggleMount()
        {
            var mount = Mount;
            if (mount != null)
            {

                if (!mount.Toggled)
                {
                    Unequip(CharacterInventoryPositionEnum.ACCESSORY_POSITION_PETS);
                    Character.Look = Character.Look.GetMountLook(mount.Template.Look);
                    ItemEffectsProvider.AddEffects(Character, mount.Effects.ToList<Effect>());
                }
                else
                {
                    Character.Look = Character.Look.GetMountDriverLook();
                    Character.Look.SetBones(1);
                    ItemEffectsProvider.RemoveEffects(Character, mount.Effects.ToList<Effect>());
                }

                mount.Toggled = !mount.Toggled;
                mount.UpdateElement();
                Character.Client.Send(new MountRidingMessage(mount.Toggled));
                Character.RefreshStats();
                Character.RefreshActorOnMap();
            }
        }
        public CharacterMountRecord GetMount(long uid)
        {
            return Mounts.FirstOrDefault(x => x.UId == uid);
        }
        public CharacterMountRecord GetMount(CharacterItemRecord certificate)
        {
            return GetMount(certificate.FirstEffect<EffectMount>(EffectsEnum.Effect_MountDefinition).MountId);
        }
        public void SetMount(CharacterMountRecord mount, CharacterItemRecord certificate)
        {
            RemoveItem(certificate, 1);
            mount.Setted = true;
            mount.UpdateElement();
            Character.Client.Send(new MountSetMessage(mount.GetMountClientData()));
        }
        public void UnsetMount()
        {
            var mount = Mount;

            if (mount.Toggled)
            {
                ToggleMount();
            }
            mount.Setted = false;
            mount.UpdateElement();
            Character.Client.Send(new MountUnSetMessage());
        }
        public CharacterItemRecord[] GetEquipedMinationItems()
        {
            return Array.FindAll(GetEquipedItems(), x => x.HasEffect<EffectMination>()); 
        }
        public void DissociateLiving(CharacterItemRecord item)
        {
            EffectDice effect = item.FirstEffect<EffectDice>(EffectsEnum.Effect_LivingObjectId);

            if (effect != null)
            {
                var levelEffect = item.FirstEffect<Effect>(EffectsEnum.Effect_LivingObjectLevel);
                var categoryEffect = item.FirstEffect<Effect>(EffectsEnum.Effect_LivingObjectCategory);
                var moodEffect = item.FirstEffect<Effect>(EffectsEnum.Effect_LivingObjectMood);
                var skinEffect = item.FirstEffect<Effect>(EffectsEnum.Effect_LivingObjectSkin);

                var newItem = ItemRecord.GetItem(effect.Const).GetCharacterItem(Character.Id, item.Quantity, false);
                newItem.Effects.Clear();   //RemoveAllEffects
                newItem.AddEffect(levelEffect);
                newItem.AddEffect(categoryEffect);
                newItem.AddEffect(moodEffect);
                newItem.AddEffect(skinEffect);
                newItem.AppearanceId = item.AppearanceId;
                item.RemoveAllEffects(EffectsEnum.Effect_LivingObjectId);
                item.RemoveAllEffects(EffectsEnum.Effect_LivingObjectLevel);
                item.RemoveAllEffects(EffectsEnum.Effect_LivingObjectCategory);
                item.RemoveAllEffects(EffectsEnum.Effect_LivingObjectMood);
                item.RemoveAllEffects(EffectsEnum.Effect_LivingObjectSkin);

                AddItem(newItem);

                if (item.PositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                {
                    UpdateItemAppearence(item, item.Template.AppearanceId);
                    OnItemModified(item);
                }
                else
                {
                    item.AppearanceId = item.Template.AppearanceId;

                    if (item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    {
                        if (!CheckPassiveStacking(item))
                            OnItemModified(item);
                    }
                    else
                    {
                        OnItemModified(item);
                    }

                }
                Character.RefreshActorOnMap();
                item.UpdateElement();
                RefreshWeight();
            }
        }
        /// <summary>
        /// A trouver = TextInformationMessage (Vous avez dus lacher votre arme a deux mains pour pouvoir équiper)
        /// </summary>
        /// <param name="position"></param>
        /// <param name="item"></param>
        public void CheckTwoHandeds(CharacterInventoryPositionEnum position, CharacterItemRecord item)
        {
            if (position == CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON)
            {
                if (WeaponRecord.GetWeapon(item.GId).TwoHanded)
                {
                    CharacterItemRecord shield = GetEquipedItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_SHIELD);

                    if (shield != null)
                    {
                        UnequipItem(shield, shield.Quantity);
                        OnObjectMoved(shield, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);

                        Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 79);
                    }
                }
            }
            else if (position == CharacterInventoryPositionEnum.ACCESSORY_POSITION_SHIELD)
            {
                CharacterItemRecord weapon = GetEquipedItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON);

                if (weapon != null && WeaponRecord.GetWeapon(weapon.GId).TwoHanded)
                {
                    UnequipItem(weapon, weapon.Quantity);
                    OnObjectMoved(weapon, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);

                    Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 78);
                }
            }
        }
        private void OnObjectMoved(CharacterItemRecord item, CharacterInventoryPositionEnum newPosition)
        {
            Character.Client.Send(new ObjectMovementMessage(item.UId, (byte)newPosition));
        }
        public void DecrementEtherals()
        {
            foreach (var item in GetEquipedItems())
            {
                EffectDice effect = item.FirstEffect<EffectDice>(EffectsEnum.Effect_RemainingEtheral);

                if (effect != null)
                {
                    effect.Min--;

                    if ((effect.Max -= 1) == 0)
                    {
                        SetItemPosition(item.UId, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED, item.Quantity);
                        RemoveItem(item, item.Quantity);
                    }
                    else
                    {
                        this.OnItemModified(item);
                        item.UpdateElement();
                    }
                }
            }
        }

        public void ChangeLivingObjectSkin(CharacterItemRecord item, ushort skinIndex, CharacterInventoryPositionEnum characterInventoryPositionEnum)
        {
            if (item.Quantity > 1)
            {
                return;
            }

            EffectInteger effect = item.FirstEffect<EffectInteger>(EffectsEnum.Effect_LivingObjectSkin);

            if (effect != null)
            {


                LivingObjectRecord record = LivingObjectRecord.IsLivingObject(item.GId) ? LivingObjectRecord.GetLivingObjectDatas(item.GId) : LivingObjectRecord.GetLivingObjectDatas(item.FirstEffect<EffectDice>(EffectsEnum.Effect_LivingObjectId).Const);


                if (record.Skinnable)
                {
                    ushort skin = record.GetSkin(skinIndex);

                    if (characterInventoryPositionEnum != CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
                    {
                        this.UpdateItemAppearence(item, skin);
                    }
                    else
                    {
                        item.AppearanceId = skin;
                    }

                    Character.RefreshActorOnMap();
                }


                effect.Value = skinIndex;
                OnItemModified(item);
                RefreshWeight();
                item.UpdateElement();
            }
            else
            {
                Character.ReplyError("unable to modify item skin..he is not a valid living object.");
            }
        }
        /// <summary>
        /// Met a jour la skin d'un objet équipé+
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="newApparenceId"></param>
        private void UpdateItemAppearence(CharacterItemRecord item, ushort newApparenceId)
        {
            UpdateLook(item, false);
            item.AppearanceId = newApparenceId;
            UpdateLook(item, true);
        }

        public void DropItem(uint uid, uint quantity)
        {

            CharacterItemRecord item = GetItem(uid);
            if (item != null && item.CanBeExchanged() && item.Quantity >= quantity && item.PositionEnum == CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)
            {
                ushort targetCell = Character.Map.CloseCellForDropItem(Character.CellId);

                if (targetCell != 0)
                {
                    this.RemoveItem(item, quantity);
                    this.Character.Map.Instance.AddDroppedItem(item, (ushort)quantity, targetCell);
                }
            }
        }

    }
}



