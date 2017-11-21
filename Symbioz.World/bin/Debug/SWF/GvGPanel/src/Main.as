package
{
	import CommonUI.Popup;
	import SelfmadeProtocol.GuildArenaSubscribeAnswerMessage;
	import flash.display.Sprite;
	import flash.utils.*;
	
	/// DOFUS v 2.38.0.113902.1
	/// RawDataMessage permettant de gérer les interfaces pour le module DOFUS de la GvG
	/// RDM Dynamique
	/// Possible de créer des modules d'UI de manière dynamique également? via la RDM? trop volumineux? 
	public class Main extends Sprite
	{
	
		
		public function Main()
		{
			super();
			
			
			var buttons:Array = new Array(); // liste des boutons
			buttons.push("Oui");
			buttons.push("Non");
			
			
			var functions:Array = new Array(); // liste des fonctions liés au boutons dans l'ordre
			functions.push(Accept); // l'utilisateur a accepté l'inscription a la GvG ("Oui")
			functions.push(Deny); // l'utilisateur a refusé ("Non")
			
			
			var pop:Popup = new Popup("Inscription GvG", "Voulez vous enregistrer votre guilde dans la GvG?", buttons, functions, Deny);
			pop.Open(); // ouvre le popup (c'est ici que le lien avec le client est faite de manière dynamique et non avec une API)
		
		}
		
		public function Accept():void
		{
			var msg:GuildArenaSubscribeAnswerMessage = new GuildArenaSubscribeAnswerMessage(true); // on renvoit un message au serveur via le rawdatamessage signalant que le joueur a accepté
			msg.send();
		}
		
		public function Deny():void
		{
			var msg:GuildArenaSubscribeAnswerMessage = new GuildArenaSubscribeAnswerMessage(false); // on renvoit un message au serveur via le rawdatamessage signalant que le joueur a refusé
			msg.send();
		}
	
	}
}
