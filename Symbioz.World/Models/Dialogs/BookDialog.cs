using Symbioz.Protocol.Enums;
using Symbioz.Protocol.Messages;
using Symbioz.World.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Symbioz.World.Models.Dialogs
{
    public class BookDialog : Dialog
    {
        public override DialogTypeEnum DialogType
        {
            get
            {
                return DialogTypeEnum.DIALOG_BOOK;
            }
        }

        public ushort DocumentId { get; set; }

        public BookDialog(Character character, ushort documentId)
            : base(character)
        {
            this.DocumentId = documentId;
        }
        public override void Open()
        {
            Character.Client.Send(new DocumentReadingBeginMessage(DocumentId));
        }
    }
}
