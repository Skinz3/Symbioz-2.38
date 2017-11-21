


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 153878787
npcId = 108
doneObjectives = {}
notDoneObjectives = {} 
criteria = "PL>9"
--------

function TalkToNpc()

env:sayNpc("Je vois que tu as acquis un minimum d'experience alors je vais t'avouer ce que j'ai découvert, sur ce serveur, on peut dropper des <b>âmes de monstres</b>, on peut ensuite les contrôler en combat comme des companions!");

end

function Execute()

end

function CriteriaWrong()

env:sayNpc("Quand tu seras niveau 10, je t'avourais un secret!");

end
