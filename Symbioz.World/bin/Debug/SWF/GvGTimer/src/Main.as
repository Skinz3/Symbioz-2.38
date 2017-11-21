package
{
	import CommonUI.NotificationWithCallback;
	import flash.display.Sprite;
	import flash.utils.*;
	
	/// DOFUS v 2.38.0.113902.1
	/// RawDataMessage permettant de gérer les interfaces pour le module DOFUS de la GvG
	/// RDM Dynamique
	/// Possible de créer des modules d'UI de manière dynamique également? via la RDM? trop volumineux? 
	public class Main extends Sprite
	{
		public static var MINUTES_BEFORE_GVG_START:int = 1; // URL tutoriel qui explique le fonctionnement du GvG
		
		public static var NOTIF_TEXT:String = "Vous combattez avec votre guilde dans moins de "+MINUTES_BEFORE_GVG_START+" minute(s)! Rendez vous a Astrub, parlez a Arod afin de vous téléporter au HUB et et préparez vous au combat!";
		
		public function Main()
		{
			super();
			
			var popup:NotificationWithCallback = new NotificationWithCallback("Combat de GvG!", NOTIF_TEXT, 2, 60 * MINUTES_BEFORE_GVG_START);
			
			popup.Open(); // ouvre le popup illustré  (c'est ici que le lien avec le client est faite de manière dynamique et non avec une API)
		
		}
	
	}
}
