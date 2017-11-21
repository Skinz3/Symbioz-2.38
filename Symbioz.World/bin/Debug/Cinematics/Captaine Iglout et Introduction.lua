


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 154010371
npcId = 57
doneObjectives = {} 
notDoneObjectives = {1} 
criteria = ""
--------


function TalkToNpc()

env:sayNpc("Bonjour jeune aventurier! Bienvenue à Incarnam, tu es libre de te mouvoir, mais ne te rend pas au <b>Donjon de Kardorim</b>! Tu pourrais ne pas en revenir.");

end

function Execute()

    env:teleportSameMap(383);
	env:orientation(1);
	env:canInteract(false);
	
	
	env:wait(500);

	env:sayNpc("Bienvenue sur le serveur jeune joueur!");
	env:wait(500);
	env:smileyNpc(4);
	env:wait(2000);
	env:say("Merci :) comment commencer?");

	env:wait(3000);
	

	
	env:wait(500);

	env:sayNpc("Utilise .help dans le chat pour te reseigner sur la liste des commandes disponibles. Tu te trouves dans la zone des débutants! Tu découvrira l'étendue du serveur au fur et a mesure de ta progression. Quand tu seras prêt, va parler a <b>Ameno</b>, prêt de la mongolfière.");

	env:wait(500);
	env:reach(1);

	env:canInteract(true);

end

function CriteriaWrong()

end