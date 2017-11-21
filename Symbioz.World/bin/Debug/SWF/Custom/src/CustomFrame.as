package 
{
	
    import com.ankamagames.jerakine.messages.Frame;
    import com.ankamagames.jerakine.logger.Logger;
    import com.ankamagames.jerakine.logger.Log;
    import com.ankamagames.jerakine.types.enums.Priority;
    import com.ankamagames.jerakine.messages.Message;
    import messages.CommandLineMessage;
    import utilities.CommandLineHelper;
    import flash.utils.getQualifiedClassName;
    import flash.utils.Dictionary;
    
    public class CustomFrame implements Frame
    {
        private static const _log:Logger = Log.getLogger(getQualifiedClassName(CustomFrame));
        
        private static const _handlers:Dictionary = new Dictionary()
        {
            { CommandLineMessage.protocolId , handleCommandLineMessage }
        };
        
        public function CustomFrame() 
        {
        }
        
        public function get priority() : int
        {
            return Priority.HIGHEST;
        }
        
        public function pushed() : Boolean
        {
            return true;
        }

        public function pulled() : Boolean
        {
            return true;
        }

        public function process(msg:Message) : Boolean
        {
            if (!_handlers[msg.getMessageId()])
            {
                return false;
            }
            
            return _handlers[msg.getMessageId()]();
        }
        
        public static function handleCommandLineMessage(message:CommandLineMessage) : Boolean
        {
            CommandLineHelper.excecute(message.parameters);
            return true;
        }
    }
}