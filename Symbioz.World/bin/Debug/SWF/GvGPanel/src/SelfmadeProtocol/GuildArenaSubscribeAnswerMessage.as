package SelfmadeProtocol 
{
	import flash.utils.ByteArray;
	/**
	 * ...
	 * @author Skinz
	 */
	public class GuildArenaSubscribeAnswerMessage extends RawMessage
	{
		private var Register:Boolean;
		
		public function GuildArenaSubscribeAnswerMessage(register:Boolean) 
		{
			this.Register = register;
		}
		override public function getMessageId():uint
		{
			return 1;
		}
		override public function serialize(byteArray:ByteArray) : void
		{
			byteArray.writeBoolean(Register);
		}
		
	}

}