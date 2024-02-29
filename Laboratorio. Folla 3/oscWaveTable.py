from consts import *
import numpy as np

class OscWaveTable:
    def __init__(self, frec, vol, size):
        self.frec = frec
        self.vol = vol
        self.targetVol = vol
        self.size = size
        self.interpRate=.05
        # un ciclo completo de seno en [0,2pi)
        t = np.linspace(0, 1, num=size)
        self.waveTable = np.sin(2 * np.pi * t)
        # arranca en 0
        self.fase = 0
        # paso en la wavetable en funcion de frec y RATE
        self.step = self.size/(SRATE/self.frec)
    def setFrec(self,frec):
        self.frec = frec
        self.step = self.size/(SRATE/self.frec)
    def getFrec(self):
        return self.frec
    def setVol(self, vol):
        self.targetVol = vol
    def getChunk(self):
        samples = np.zeros(CHUNK,dtype=np.float32)
        cont = 0
        if (self.vol != self.targetVol):
            self.vol = self.vol + ((self.targetVol - self.vol)*self.interpRate)
        while cont < CHUNK:
            self.fase = (self.fase + self.step) % self.size
            # samples[cont] = self.waveTable[int(self.fase)]
            # con redondeo
            #x = round(self.fase) % self.size
            #samples[cont] = self.waveTable[x]

            # con interpolacion lineal
            x0 = int(self.fase) % self.size
            x1 = (x0 + 1) % self.size
            y0, y1 = self.waveTable[x0], self.waveTable[x1]
            samples[cont] = y0 + (self.fase-x0)*(y1-y0)/(x1-x0)
            cont = cont+1

        return self.vol*samples