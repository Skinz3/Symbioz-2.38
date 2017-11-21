package
{
	import CommonUI.IllustratedPopup;
	import flash.display.Sprite;
	import flash.utils.*;
	
	/// DOFUS v 2.38.0.113902.1
	/// RawDataMessage permettant de gérer les interfaces pour le module DOFUS de la GvG
	/// RDM Dynamique
	/// Possible de créer des modules d'UI de manière dynamique également? via la RDM? trop volumineux? 
	public class Main extends Sprite
	{
		public static var GVG_TUTORIAL_URL:String = "http://amnesia-server.com"; // URL tutoriel qui explique le fonctionnement du GvG
		
		public static var SUCCES_PANEL_IMAGE_NAME:String = "login_background.png"; // image de fond du panel
	
		public static var SUCCES_PANEL_TEXT:String = "Votre guilde a été enregistrée pour participer à la guerre des guildes, le tableau des combats et les horaires des matchs seront disponibles à partir de jeudi. Cliquez sur 'en savoir plus' pour plus d'informations sur ce mode de jeu.";
		
		public function Main()
		{
			super();
			
			var popup:IllustratedPopup = new IllustratedPopup("Félicitations !",SUCCES_PANEL_TEXT , GVG_TUTORIAL_URL, SUCCES_PANEL_IMAGE_NAME);
			popup.Open(); // ouvre le popup illustré  (c'est ici que le lien avec le client est faite de manière dynamique et non avec une API)
		
		}
	}
}
