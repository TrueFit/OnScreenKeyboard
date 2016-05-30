import sys, getopt, os.path, codecs, time, locale

def showCommandLineOptions():
    print("OnScreenKeyboard.py --source <Source File> --paths <Paths>")
    print("                     [--traversal <Infinite or Bounded>] [--keys <Keys File>]")
    print("                     [--startchar] <Character To Start At>")
    print("     -source: Specifies the source file containing the list of strings.")
    print("              This must be a text file that is UTF-8 encoded.")
    print("")
    print("      -paths: Specifies the output file into which navigation paths will be written.")
    print("              This file, if it previously exists, will not be appended. It will be overwritten.")
    print("")
    print(" [-traversal:] Dictates whether or not the cursor can move freely beyond the boundary of the grid")
    print("               and automatically wrap-around. When in \"Infinite\" mode, a cursor on the left-most")
    print("               coordinate can move left and the cursor will move to the far right. This behavior")
    print("               carries over to all bounds. A \"Bounded\" grid will not permit cursor movement past the")
    print("               four boundaries. (Default = Infinite)")
    print("")
    print("      [-keys:] Specifies the name of a file containing an alternate mapping of keys. This file")
    print("               must contain both the mappings along with definitions for what \"alternate\"")
    print("               characters should be mapped to.")
    print("")
    print(" [-startchar:] Specifies the character on the grid where the cursor will start (default = A)") 

def getCommandLineOptions():
    options = {
        "source": "",
        "paths": "",
        "isInfinite": True,
        "keys": "",
        "startChar": "A"    
    }

    try:
        opts, args = getopt.getopt(sys.argv[1:],"hs:p:t:k:s:",["source=","paths=","traversal=","keys=","startchar="])
    except getopt.GetoptError as err:
        showCommandLineOptions()
        print(str(err))
        sys.exit(2)

    for opt, arg in opts:       
        if opt == '-h':
            showCommandLineOptions()
            sys.exit()
        elif opt in ("-s", "--source"):
            options["source"] = arg
        elif opt in ("-p", "--paths"):
            options["paths"] = arg
        elif opt in ("-t", "--traversal"):
            s = arg.lower()
            if (s == "infinite"):
                options["isInfinite"] = True
            elif (s == "bounded"):
                options["isInfinite"] = False
            else:
                options["isInfinite"] = None
        elif opt in ("-k", "--keys"):
            options["keys"] = arg
        elif opt in ("-s", "--startchar"):
            if (len(arg) != 1):
                showCommandLineOptions()
                sys.exit()
            options["startChar"] = arg
        
    return options
    
def loadKeysFile(keyFile):
    lines = []
    with codecs.open(keyFile, "r", "utf-8") as txt_file:
        for line in txt_file:
            lines.append(line)
        
    gridData = {
        "gridWidth": 0,
        "gridHeight": 0,
        "maxTravelRows": 0,
        "maxTravelCols": 0,
        "isInfinite": None,
        "startChar": None,
        "keyMappings": {},
        "altKeys": {}
    }
        
    cnt = len(lines)
    delimiter = ""
    isKeySection = True
    for y in range(0, cnt):
        lines[y] = lines[y].strip()
        linelen = len(lines[y])
        if (isKeySection):
            if (y == 0):
                gridData["gridWidth"] = linelen
                for x in range(0, linelen): delimiter += "-"
            elif (gridData["gridWidth"] != linelen):
                print ("Invalidate key mapping on line " + str(y + 1))
                return None
            
            if (lines[y] == delimiter):
                gridData["gridHeight"] = y
                isKeySection = False
                continue
            
            for x in range(0, linelen):
                c = lines[y][x]
                if (c in gridData["keyMappings"]):
                    print ("Duplicate key mapping (" + str(c) + ") on line " + str(y + 1))
                    return None
                gridData["keyMappings"][c] = [ y, x ]
        else:
            if (linelen != 2):
                if (y == cnt - 1 and linelen == 0): break
                print ("Invalidate alternate character mapping on line " + str(y + 1))
                return None
            
            c = lines[y][0]
            if (c in gridData["altKeys"]):
                print ("Duplicate alternate key (" + str(c) + ") on line " + str(y + 1))
                return None
                
            gridData["altKeys"][c] = lines[y][1]    
    
    if (isKeySection):
        gridData["gridHeight"] = cnt
        
    gridData["maxTravelRows"] = gridData["gridHeight"] // 2 + gridData["gridHeight"] % 2
    gridData["maxTravelCols"] = gridData["gridWidth"] // 2 + gridData["gridWidth"] % 2
    
    return gridData

def getDefaultMappings():
    return {
        "gridWidth": 6,
        "gridHeight": 6,
        "maxTravelRows": 3,
        "maxTravelCols": 3,
        "isInfinite": None,
        "startChar": None,
        "keyMappings": {
            "A": [ 0, 0 ],
            "B": [ 0, 1 ],
            "C": [ 0, 2 ],
            "D": [ 0, 3 ],
            "E": [ 0, 4 ],
            "F": [ 0, 5 ],
            
            "G": [ 1, 0 ],
            "H": [ 1, 1 ],
            "I": [ 1, 2 ],
            "J": [ 1, 3 ],
            "K": [ 1, 4 ],
            "L": [ 1, 5 ],
            
            "M": [ 2, 0 ],
            "N": [ 2, 1 ],
            "O": [ 2, 2 ],
            "P": [ 2, 3 ],
            "Q": [ 2, 4 ],
            "R": [ 2, 5 ],
            
            "S": [ 3, 0 ],
            "T": [ 3, 1 ],
            "U": [ 3, 2 ],
            "V": [ 3, 3 ],
            "W": [ 3, 4 ],
            "X": [ 3, 5 ],
            
            "Y": [ 4, 0 ],
            "Z": [ 4, 1 ],
            "1": [ 4, 2 ],
            "2": [ 4, 3 ],
            "3": [ 4, 4 ],
            "4": [ 4, 5 ],
            
            "5": [ 5, 0 ],
            "6": [ 5, 1 ],
            "7": [ 5, 2 ],
            "8": [ 5, 3 ],
            "9": [ 5, 4 ],
            "0": [ 5, 5 ]
        },
        "altKeys": {
            "a": "A",
            "b": "B",
            "c": "C",
            "d": "D",
            "e": "E",
            "f": "F",
            "g": "G",
            "h": "H",
            "i": "I",
            "j": "J",
            "k": "K",
            "l": "L",
            "l": "M",
            "m": "N",
            "n": "O",
            "o": "P",
            "q": "Q",
            "r": "R",
            "s": "S",
            "t": "T",
            "u": "U",
            "v": "V",
            "w": "W",
            "x": "X",
            "y": "Y",
            "z": "Z"
        }
    }

def addToPath(path, addInfo):
    if (len(addInfo) == 0): return path
    
    if (len(path) > 0):
        path = path + ","
    path = path + addInfo
    return path
    
def getSingleLateral(currPos, newPos, extent, maxTravel, posChar, negChar):
    diff = newPos - currPos
    absDiff = abs(diff)

    if (absDiff > maxTravel):
        diff = (extent - absDiff) * (-1 if diff > 0 else 1);
        absDiff = abs(diff);

    lateral = ""
    for y in range(0, absDiff):
        lateral = addToPath(lateral, (posChar if diff > 0 else negChar))
    
    return lateral
        
def getPath(term, mappings):
    row = 0
    col = 0
    sc = mappings["startChar"][0] 
    if (sc not in mappings["keyMappings"]):
        if (sc in mappings["altKeys"]):
            sc = mappings["altKeys"][sc]

    if (sc in mappings["keyMappings"]):
        scEntry = mappings["keyMappings"][sc]
        row = scEntry[0]
        col = scEntry[1]

    path = ""
    termlen = len(term)
    for x in range(0, termlen):   
        c = term[x]
        
        if ((x == termlen - 1 and c == '\n') or (x == termlen - 2 and c == '\r')): continue
        
        if (c == ' '):
            path = addToPath(path, "S")
            continue
            
        if (c in mappings["altKeys"]):
            c = mappings["altKeys"][c]
        
        if (c not in mappings["keyMappings"]):
            path = addToPath(path, "X")
            continue

        mapEntry = mappings["keyMappings"][c] 
        newRow = mapEntry[0]
        newCol = mapEntry[1]
        
        path = addToPath(path, getSingleLateral(row, newRow, mappings["gridHeight"], maxTravelRows if mappings["isInfinite"] else mappings["gridHeight"], "D", "U"))
        path = addToPath(path, getSingleLateral(col, newCol, mappings["gridWidth"], maxTravelCols if mappings["isInfinite"] else mappings["gridWidth"], "R", "L"))                
        path = addToPath(path, "#")
        
        row = newRow
        col = newCol
    
    return path
    
def formatnd(n, decimals = 0):
  return locale.format("%." + str(decimals) + "f", n, grouping=True)

locale.setlocale(locale.LC_ALL, '')
start_time = time.time()

options = getCommandLineOptions()
if (len(options["source"]) == 0 or not os.path.isfile(options["source"]) or len(options["paths"]) == 0 or options["isInfinite"] is None):
    showCommandLineOptions()
    sys.exit(3)

if (len(options["keys"]) > 0):
    mappings = None if not os.path.isfile(options["keys"]) else loadKeysFile(options["keys"])
    if (mappings == None):
        print("Failed to load keys file")
        print("Load default keys mapping?")
        print("")
        print("  ABCDEF")
        print("  GHIJKL")
        print("  MNOPQR")
        print("  STUVWX")
        print("  YZ1234")
        print("  567890")
        print("")
        useDefault = ""
        while (useDefault != "y" and useDefault != "n"):
            useDefault = input("(y/n)").lower()

        if (useDefault == "n"): sys.exit(4)
        mappings = getDefaultMappings()
else:
    mappings = getDefaultMappings()

mappings["isInfinite"] = options["isInfinite"]
mappings["startChar"] = options["startChar"]

lines = []
with codecs.open(options["source"], "r", "utf-8") as txt_infile:
    for line in txt_infile:
        lines.append(line)
    
maxTravelRows = mappings["gridHeight"] // 2 + mappings["gridHeight"] % 2
maxTravelCols = mappings["gridWidth"] // 2 + mappings["gridWidth"] % 2
cnt = len(lines)
with open(options["paths"], "w") as txt_outfile:
    for l in range(0, cnt):
        path = getPath(lines[l], mappings)
        txt_outfile.write(path + "\n")
        if (((l + 1) % 100000) == 0 or (l % 99974) == 0):    
            print("path for " + lines[l].rstrip() + " at row " + formatnd(l + 1) + " = " + path)

secs = time.time() - start_time
print("Total seconds: " + formatnd(secs, 1))
print("Average rows per second: " + formatnd(cnt if secs == 0 else cnt / secs, 1))