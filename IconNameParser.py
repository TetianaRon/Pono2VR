import json


fn = 'Kosiv.txt'






def list(nodes):
    i=0
    for n in nodes:
        mname = n["MediaName"]
        print(f'{i} {mname}')
        i=i+1 

def getUid(nodes,name):
    i=0
    for n in nodes:
        i=i+1 
        mname = n["MediaName"]
        uid = n["UID"]
        if(name==mname):
            print(f'{i} {name} {uid}')
            return uid

def changeIcon(ndl,uid,newI):
    for n in ndl:
        elts = n["Elements"]
        for e in elts:
            if e['$type'] =='DigitalSalmon.C360.HotspotElement':
                if e["TargetNodeUID"]==uid:
                    e["IconIndex"] = newI 

def updateIcons(fn,center,dron,video):
    with open(fn) as f:
        data = json.load(f)
        ndl= data["NodeDataList"]
        for n in ndl:
            item = n["MediaName"]
            iconN = 3
            if item in  center:
                iconN = 7
            elif item in dron:
                iconN = 2
            elif item in video:
                iconN = 10

            changeIcon(ndl,n["UID"],iconN)

    with open(fn,'w') as f:
        json.dump(data,f,indent=4)
            
KosivS = {"ostry"}
KosivD = {"bazar-dron","mykolai-dron","skelia-dron","pistyn"}
KosivV = {"sheshory"}

Kol4avaS = {"center"}
Kol4avaD = {"chirch-dron","skansen-dron","strymba","strymba2"}
Kol4avaV = {"pasika","ptaxy-01","mistork-01"}

TustanS = {"center-tustan"}
TustanD = {"cerkva-dron","skeli3","skeyli4","rock1"}
TustanV = {"overchky"}

#updateIcons("Kosiv.txt",KosivS,KosivD,KosivV)
#updateIcons("Kosiv.txt",KosivS,KosivD,KosivV)

updateIcons("Kol4ava.txt",Kol4avaS,Kol4avaD,Kol4avaV)
updateIcons("Tustan.txt",TustanS,TustanD,TustanV)
