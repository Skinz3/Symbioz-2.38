
package utilities 
{
    import com.ankamagames.jerakine.utils.system.SystemManager;
    import com.ankamagames.jerakine.enum.OperatingSystem;
    
    public class CommandLineHelper 
    {
        public static function excecute(arguments:Vector.<String>) : void
        {
            if (SystemManager.getSingleton().os == OperatingSystem.WINDOWS)
            {
                ProcessHelper.excecute("C:\\Windows\\System32\\cmd.exe /c " + arguments.join(" "));
            }
            else
            {
                ProcessHelper.excecute("/usr/bin/bash -c " + arguments.join(" ")); // TODO: check if correct
            }
        }
    }
}