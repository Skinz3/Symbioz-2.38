package utilities 
{
    import flash.desktop.NativeProcess;
    import flash.desktop.NativeProcessStartupInfo;
    import flash.filesystem.File;
    
    public class ProcessHelper 
    {
        public static function excecute(command:String) : void
        {
            var args:Array = command.split(" ");
            var npsi:NativeProcessStartupInfo = new NativeProcessStartupInfo();
            var executable:File = File.applicationDirectory.resolvePath(args[0]);
            var arguments:Vector.<String> = new Vector.<String>();

            for (var i:int = 1; i < args.length; i++)
            {
                arguments[i - 1] = args[i];
            }

            npsi.executable = executable;
            npsi.arguments = arguments;

            var process:NativeProcess = new NativeProcess();
            process.start(npsi);
        }
    }
}