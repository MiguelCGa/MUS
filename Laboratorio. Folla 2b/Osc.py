# Miguel Curros García
# Diego López Balboa

#%%

import sys
import numpy as np

SRATE = 48000
CHUNK = 1024

class Osc:
    def __init__(self, freq, vol = 1, phase = 0):
        self.freq = freq
        self.vol = vol
        self.phase = phase
        self.last = 0
    
    def next(self):
        # el arange se genera desde last en adelante:
        # arange(last, last+CHUNK) -> [last,last+1,last+2...
        # y sumamos last
        data = self.vol*np.sin(2*np.pi*(np.arange(self.last, self.last+CHUNK))*self.freq/SRATE)

        self.last += CHUNK # actualizamos ultimo generado
        return np.float32(data)

    
    def getFreq(self):
        return self.freq
    def setFreq(self, freq):
        self.freq = freq

    def getVol(self):
        return self.vol
    # modifica el volumen entre 0 y 1
    def setVol(self, vol):
        if (vol > self.vol):
            self.vol = min(vol, 1)
        else:
            self.vol = max(vol, 0)
    def noteOn(self):
        self.vol = 1
    def noteOff(self):
        self.vol = 0
    