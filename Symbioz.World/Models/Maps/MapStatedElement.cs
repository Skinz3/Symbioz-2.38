using Symbioz.Protocol.Types;
using Symbioz.World.Records.Interactives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Symbioz.Protocol.Selfmade.Enums;
using Symbioz.World.Models.Entities;
using Symbioz.Core;
using System.Timers;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities.Jobs;
using Symbioz.World.Providers;
using Symbioz.World.Models.Maps.Instances;

namespace Symbioz.World.Models.Maps
{
    public class MapStatedElement : MapInteractiveElement
    {
        /// <summary>
        /// Exprimé en secondes.
        /// </summary>
        public const int GrowInterval = 120;

        /// <summary>
        /// Représente le timer d'utilisation de l'élement.
        /// </summary>
        private Timer OnUsedCallback;

        /// <summary>
        /// Représente le timer de repousse de l'élement.
        /// </summary>
        private ActionTimer GrowCallback;

        /// <summary>
        /// GfxId de l'élement lié au skill.
        /// </summary>
        public int GfxId
        {
            get
            {
                return Record.GfxBonesId;
            }
        }
        /// <summary>
        /// Personnage collecteur.
        /// </summary>
        private Character Character
        {
            get;
            set;
        }
        /// <summary>
        /// Skill utilisé.
        /// </summary>
        protected MapInteractiveElementSkill Skill
        {
            get
            {
                return EnabledSkills.Count > 0 ? EnabledSkills.First() : DisabledSkills.First();
            }
        }
        /// <summary>
        /// Etat actuel de l'élement.
        /// </summary>
        public StatedElementStatesType State
        {
            get;
            set;
        }

        public MapStatedElement(AbstractMapInstance mapInstance, InteractiveElementRecord record)
            : base(mapInstance, record)
        {

        }
        public void UseStated(Character character)
        {
            this.Character = character;

            CharacterJob job = Character.GetJob(Skill.Template.ParentJobIdEnum);

            if (job != null && job.Level < Skill.Template.MinLevel)
            {
                return;
            }
            if (State == StatedElementStatesType.Active)
            {
                this.Character.Collecting = true;
                this.UpdateState(StatedElementStatesType.Used);
                this.GrowCallback = new ActionTimer(GrowInterval * 1000, Grow, false);
                OnUsedCallback = new Timer(Skill.Template.Duration * 100);
                OnUsedCallback.Elapsed += OnUsedCallback_Elapsed;
                OnUsedCallback.Start();
            }
        }
        void OnUsedCallback_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnUsedCallback.Dispose();
            Character.Client.Send(new InteractiveUseEndedMessage((uint)Record.ElementId, Skill.Id));
            this.Collect();
            UpdateState(StatedElementStatesType.Unactive);
            GrowCallback.Start();

        }
        public void Grow()
        {
            if (State == StatedElementStatesType.Unactive)
                UpdateState(StatedElementStatesType.Active);
        }
        public void Collect()
        {
            if (State == StatedElementStatesType.Used)
            {
                CharacterJob job = Character.GetJob(Skill.Template.ParentJobIdEnum);

                uint quantity = FormulasProvider.Instance.GetCollectedItemQuantity((short)(job != null ? job.Level : 1), Skill.Template);

                this.Character.Inventory.AddItem((ushort)Skill.Template.GatheredRessourceItem, quantity);
                this.Character.Client.Send(new ObtainedItemMessage((ushort)Skill.Template.GatheredRessourceItem, quantity));

                if (job != null)
                    this.Character.AddJobExp(Skill.Template.ParentJobIdEnum, (ulong)(5 * Skill.Template.MinLevel));
                this.Character.Collecting = false;
                this.Character = null;
            }
        }
        public void UpdateState(StatedElementStatesType state)
        {
            switch (state)
            {
                case StatedElementStatesType.Active:
                    this.Skill.Enable();
                    break;
                case StatedElementStatesType.Unactive:
                    break;
                case StatedElementStatesType.Used:
                    this.Skill.Disable();
                    break;
            }
            this.State = state;
            base.MapInstance.Send(new StatedElementUpdatedMessage(GetStatedElement()));
        }
        public StatedElement GetStatedElement()
        {
            return new StatedElement(Record.ElementId, Record.CellId, (uint)State, true);
        }
    }
}
