package SelfmadeProtocol
{
	
	/**
	 * DynamicMessage.as
	 * @author Skinz
	 */
	public class RawMessage
	{
		import flash.display.Sprite;
		import flash.utils.*;
		
		public function RawMessage()
		{
		
		}
		
		public function serialize(byteArray:ByteArray):void
		{
			throw new Error();
		}
		
		public function getMessageId():uint
		{
			throw new Error();
		}
		
		public function send():void
		{
			var ConnectionsHandler:* = undefined;
			var message:* = undefined;
			var rawContent:ByteArray = null;
			var vector:Vector.<int> = null;
			ConnectionsHandler = getDefinitionByName("com.ankamagames.dofus.kernel.net::ConnectionsHandler");
			message = new (getDefinitionByName("com.ankamagames.dofus.network.messages.security::RawDataMessage") as Class)();
			rawContent = new ByteArray();
			vector = new Vector.<int>();
			
			rawContent.writeShort(getMessageId());
			serialize(rawContent);
			
			rawContent.position = 0;
			while (rawContent.bytesAvailable != 0)
			{
				vector.push(rawContent.readByte());
			}
			message.initRawDataMessage(rawContent);
			ConnectionsHandler.getConnection().send(message);
		}
	
	}

}