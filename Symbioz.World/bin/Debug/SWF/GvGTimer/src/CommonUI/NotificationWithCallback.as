package CommonUI
{
	import flash.utils.*;
	
	/**
	 * ...
	 * @author Skinz
	 */
	public class NotificationWithCallback
	{
		private var Title:String;
		
		private var Content:String;
		
		private var Type:int;
		
		private var Delay:int;
		
		public function NotificationWithCallback(title:String,content:String,type:int,delay:int)
		{
			this.Title = title;
			this.Content = content;
			this.Type = type;
			this.Delay = delay;
		}
		
		public function Open(): void
		{
			var mod:Object = getDefinitionByName("com.ankamagames.dofus.logic.common.managers::NotificationManager");
			var instance:Object = mod.getInstance();
			var notifId:uint = instance.prepareNotification(Title, Content, Type,"",true,true);
			instance.addTimerToNotification(notifId, Delay, false, false, true);
			instance.sendNotification(notifId);
		}
	
	}

}