


 -- Script De cinématique.
---------
author = "Skinz"
mapId = 156240386
npcId = 173
doneObjectives = {}
notDoneObjectives = {} 
criteria = ""
--------

function TalkToNpc()

env:sayNpc(string.format("Je suis le blop collant, cette année, j'ai decidé de me coller... au zaap du <b>Lac de Cania</b>!,ici la rate d'experience est multipliée par <b>%s</b>!",env:getZoneExperienceRate()));

end

function Execute()

end

function CriteriaWrong()



end
