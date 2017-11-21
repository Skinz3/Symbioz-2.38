


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 165152263
npcId = 174
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:sayNpc(string.format("Cette zone est décidément magnifique, l'experience est propice a mon développement,  elle est multipliée par <b>%s</b>!",env:getZoneExperienceRate()));

end

function Execute()

end

function CriteriaWrong()



end
