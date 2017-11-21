package CommonUI 
{
	import flash.display.Sprite;
	import flash.utils.*;
	/**
	 * Représente une boite de dialogue DOFUS 
	 * @author Skinz
	 */
	public class Popup 
	{
		private var Title:String;
		
		private var Content:String;
		
		private var Buttons:Array;
		
		private var ButtonFuncs:Array;
		
		private var OnExitFunc:Function;
		
		public function Popup(title:String, content:String, buttons:Array, buttonsFuncs:Array = null, onExitFunc:Function = null)
		{
			this.Title = title;
			this.Content = content;
			this.Buttons = buttons;
			this.ButtonFuncs = buttonsFuncs;
			this.OnExitFunc = onExitFunc;
		}
		public function Open():void
		{
			var mod:Object = getDefinitionByName("com.ankamagames.berilia.managers::UiModuleManager");
			var commonMod:Object = mod.getInstance().getModule("Ankama_Admin").mainClass;
			commonMod.openPopup(Title, Content, Buttons, ButtonFuncs, null, OnExitFunc, null, null, null);
		}
		
		
		
	}

}