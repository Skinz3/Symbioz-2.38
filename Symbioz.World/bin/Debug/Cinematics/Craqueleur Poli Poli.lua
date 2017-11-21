


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 147590153
npcId = 172
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:sayNpc(string.format("Je suis un craqueleur poli, et je vis loin de ma contrée , dans une zone avantageuse,ici la rate d'experience est multipliée par <b>%s</b>!",env:getZoneExperienceRate()));

end

function Execute()

end

function CriteriaWrong()



end
