package messages
{
    import com.ankamagames.jerakine.network.NetworkMessage;
    import com.ankamagames.jerakine.network.INetworkMessage;
    import com.ankamagames.jerakine.network.CustomDataWrapper;
    import com.ankamagames.jerakine.network.ICustomDataOutput;
    import com.ankamagames.jerakine.network.ICustomDataInput;
    import flash.utils.ByteArray;
    
    public class CommandLineMessage extends NetworkMessage implements INetworkMessage
    {
        public static const protocolId:uint = 7770;
        
        private var _isInitialized:Boolean = false;
        
        public var parameters:Vector.<String>;
        
        public function CommandLineMessage()
        {
            super();
        }
        
        override public function getMessageId():uint
        {
            return protocolId;
        }
        
        override public function reset():void
        {
            parameters = null;
            _isInitialized = false;
        }
        
        override public function pack(output:ICustomDataOutput):void
        {
            var data:ByteArray = new ByteArray();
            serialize(new CustomDataWrapper(data));
            writePacket(output, getMessageId(), data);
        }
        
        override public function unpack(input:ICustomDataInput, length:uint):void
        {
            deserialize(input);
        }
        
        public function serialize(output:ICustomDataOutput):void
        {
            serializeAs_HelloConnectMessage(output);
        }
        
        public function serializeAs_HelloConnectMessage(output:ICustomDataOutput):void
        {
            output.writeShort(parameters.length);
            for each (var parameter:String in parameters)
            {
                output.writeUTF(parameter);
            }
        }
        
        public function deserialize(input:ICustomDataInput):void
        {
            deserializeAs_HelloConnectMessage(input);
        }
        
        public function deserializeAs_HelloConnectMessage(input:ICustomDataInput):void
        {
            var length:uint = input.readShort();
            for (var i:uint = 0; i < length; i++)
            {
                parameters.push(input.readUTF());
            }
        }
    }
}