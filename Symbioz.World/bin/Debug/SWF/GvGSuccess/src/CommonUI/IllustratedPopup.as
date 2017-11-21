package CommonUI
{
	import flash.display.Sprite;
	import flash.utils.*;
	
	/**
	 * Représente une boite de dialogue Illustré DOFUS 
	 * @author Skinz
	 */
	public class IllustratedPopup
	{
		public var Title:String;
		
		public var Content:String;
		
		public var Link:String;
		
		public var ImageName:String;
		
		public function Open() : void
		{
			var mod:Object = getDefinitionByName("com.ankamagames.berilia.managers::UiModuleManager");
			var commonMod:Object = mod.getInstance().getModule("Ankama_Common").mainClass;
			commonMod.openIllustratedWithLinkPopup(Title, Content,Link, ImageName);
		}
		
		public function IllustratedPopup(title:String,content:String,link:String,imageName:String)
		{
			this.Title = title;
			this.Content = content;
			this.Link = link;
			this.ImageName = imageName;
		}
	
	}

}