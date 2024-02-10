#%%

import sounddevice as sd
import soundfile as sf
import sys
import numpy as np
import kbhit_pygame as kbhit

SRATE = 48000
CHUNK = 1024

last=0
class Osc:
    def __init__(self, freq, vol, phase):
        self.freq = freq
        self.vol = vol
        self.phase = phase
    
    def next(self):
        global last # para modificar la var last global (y no una local)
        # el arange se genera desde last en adelante:
        # arange(last, last+CHUNK) -> [last,last+1,last+2...
        # y sumamos last
        data = self.vol*np.sin(2*np.pi*(np.arange(last,last+CHUNK))*self.freq/SRATE)

        last += CHUNK # actualizamos ultimo generado
        return np.float32(data)

    
    def getFreq(self):
        return self.freq
    def setFreq(self, freq):
        self.freq = freq

    def getVol(self):
        return self.vol
    def setVol(self, vol):
        if (vol > self.vol):
            self.vol = min(vol, 1)
        else:
            self.vol = max(vol, 0)
            

def testOsc(osc):
    stream = sd.OutputStream(samplerate=SRATE, blocksize=CHUNK, channels=1)
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
    
    kb.quit()
    stream.stop()
    stream.close()



#%%
osc = Osc(261.63, 1, 0)

testOsc(osc)