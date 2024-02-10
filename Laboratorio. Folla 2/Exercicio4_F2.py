#%%

import sounddevice as sd
import soundfile as sf
import sys
import numpy as np
import kbhit_pygame as kbhit
from Exercicio1_F2 import Osc

SRATE = 48000
CHUNK = 1024

last=0
class OscStereo:
    def __init__(self, freq, vol, phase):
        self.oscL = Osc(freq, vol, phase)
        self.oscR = Osc(freq, vol, phase)
        self.stereoDial = 0
    
    def next(self):
        volL = 1
        volR = 1
        if (self.stereoDial > 0):
            volL = 0
        elif (self.stereoDial < 0):
            volR = 0
        
        stereoData = np.column_stack((volL*self.oscL.next(), volR*self.oscR.next()))
        return np.float32(stereoData)
                          
    def setStereoDial(self, dial):
        if (dial > self.stereoDial):
            self.stereoDial = min(dial, 1)
        else:
            self.stereoDial = max(dial, -1)
            
    def getFreq(self):
        return self.oscL.getFreq()
    def setFreq(self, freq):
        self.oscL.setFreq(freq)
        self.oscR.setFreq(freq)

    def getVol(self):
        return self.oscL.getVol()
    def setVol(self, vol):
        self.oscL.setVol(vol)
        self.oscR.setVol(vol)
            

def testOsc(osc):
    stream = sd.OutputStream(samplerate=SRATE, blocksize=CHUNK, channels=2)
    stream.start()
    kb = kbhit.KBHit()
    key = kb.getKey()
    while (key != "escape"):
        samples = osc.next()
        stream.write(samples)
        key = kb.getKey()
        if (key!=' '):
            if (key == "F"):
                osc.setFreq(osc.getFreq() * 9 / 8)
            elif (key == "f"):
                osc.setFreq(osc.getFreq() * 8 / 9)
            elif (key == "V"):
                osc.setVol(osc.getVol() * 2)
            elif (key == "v"):
                osc.setVol(osc.getVol() / 2)
            elif (key == "L" or key == "l"):
                osc.setStereoDial(-1)
            elif (key == "R" or key == "r"):
                osc.setStereoDial(1)
            elif (key == "C" or key == "c"):
                osc.setStereoDial(0)

    kb.quit()
    stream.stop()
    stream.close()



#%%
osc = OscStereo(261.63, 1, 0)

testOsc(osc)