# Miguel Curros García
# Diego López Balboa

#%%
import sys
import numpy as np
from Osc import Osc

SRATE = 48000
CHUNK = 1024

## La clase contiene dos osciladores para los canales izquierdo y derecho
## y una variable stereoDial que varia entre -1 y 1 ajustando el volumen de cada oscilador
class OscStereo:
    def __init__(self, freq, vol, phase):
        self.oscL = Osc(freq, vol, phase)
        self.oscR = Osc(freq, vol, phase)
        self.stereoDial = 0
    
    def next(self):
        # Convertir los valores de -1 a 1 del dial para balancear el volumen 
        # de cada canal manteniendo siempre una suma total de 1
        volR = self.stereoDial * 0.5 + 0.5
        volL = 1 - volR
        
        ## Generamos el bloque de los canales izquierdo y derecho
        dataL = volL*self.oscL.next()
        dataR = volR*self.oscR.next()

        ## Combinamos los canales de los osciladores
        stereoData = np.column_stack((dataL, dataR))
        return np.float32(stereoData)
    
    def setStereoDial(self, dial):
        if (dial > self.stereoDial):
            self.stereoDial = min(dial, 1)
        else:
            self.stereoDial = max(dial, -1)
    def getStereoDial(self):
        return self.stereoDial
    
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