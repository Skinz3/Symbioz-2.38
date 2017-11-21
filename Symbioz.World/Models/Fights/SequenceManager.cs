using Symbioz.Core;
using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Fights
{
    public class SequenceManager
    {
        private UniqueIdProvider SequenceIdPopper
        {
            get;
            set;
        }

        private Fight Fight
        {
            get;
            set;
        }
        private List<Sequence> PendingSequences
        {
            get;
            set;
        }
        public bool Sequencing
        {
            get
            {
                return PendingSequences.Count > 0;
            }
        }
        public SequenceManager(Fight fight)
        {
            this.Fight = fight;
            this.PendingSequences = new List<Sequence>();
            this.SequenceIdPopper = new UniqueIdProvider();
        }
        private ushort PopNextId()
        {
            return (ushort)SequenceIdPopper.Pop();
        }

        public void EndSequence(SequenceTypeEnum sequenceType)
        {
            var seq = PendingSequences.FirstOrDefault();
            if (seq != null)
            {
                seq.End(Fight);
                PendingSequences.Remove(seq);
            }
        }
        public void EndAllSequences()
        {
            foreach (var sequence in new List<Sequence>(PendingSequences))
            {
                if (!sequence.Ended)
                {
                    sequence.End(Fight);
                    SequenceIdPopper.Push(sequence.Id);
                }
            }

            PendingSequences.Clear();
        }
        public bool StartSequence(SequenceTypeEnum sequencetype)
        {
            if (PendingSequences.Find(x => x.SequenceType == sequencetype) != null)
                return false;

            var newSequence = new Sequence(PopNextId(), Fight, Fight.FighterPlaying, sequencetype);

            newSequence.Start(Fight);
            PendingSequences.Add(newSequence);
            return true;
        }
    }
    public class Sequence
    {
        public ushort Id
        {
            get;
            private set;
        }
        public bool Started
        {
            get;
            private set;
        }
        public bool Ended
        {
            get;
            private set;
        }
        public Fighter Source
        {
            get;
            private set;
        }
        public SequenceTypeEnum SequenceType
        {
            get;
            private set;
        }
        public bool Acknowleged
        {
            get;
            private set;
        }
        public Sequence(ushort id, Fight fight, Fighter source, SequenceTypeEnum sequenceType)
        {
            this.Id = id;
            this.Source = source;
            this.SequenceType = sequenceType;
            this.Started = false;
            this.Ended = false;
            this.Acknowleged = false;
        }

        public void End(Fight fight)
        {
            this.Ended = true;
            fight.Send(new SequenceEndMessage(Id, Source.Id, (sbyte)SequenceType));
        }

        public void Start(Fight fight)
        {
            this.Started = true;
            fight.Send(new SequenceStartMessage((sbyte)SequenceType, Source.Id));
        }
    }

}
