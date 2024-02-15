#%%
from Osc import Osc
import numpy as np
import sounddevice as sd
import kbhit_pygame as kbhit


SRATE = 48000
CHUNK = 1024
NOTAS = {
    "C": 523.251,
    "D": 587.33,
    "E": 659.255,
    "F": 698.456,
    "G": 783.991,
    "A": 880,
    "B": 987.767,
    "c": 523.251 * 2,
    "d": 587.33 * 2,
    "e": 659.255 * 2,
    "f": 698.456 * 2,
    "g": 783.991 * 2
}


CORCHEA = SRATE * 0.25
NEGRA = SRATE * 0.5
BLANCA = SRATE

happyBirthday = [("G",CORCHEA),("G",CORCHEA),("A",NEGRA),("G",NEGRA),("c",NEGRA),("B",BLANCA),("G",CORCHEA),("G",CORCHEA),("A",NEGRA),("G",NEGRA),("d",NEGRA),("c",BLANCA),("G",CORCHEA),("G",CORCHEA),("g",NEGRA),("e",NEGRA),("c",NEGRA),("B",NEGRA),("A",NEGRA),("f",CORCHEA),("f",CORCHEA),("e",NEGRA),("c",NEGRA),("d",NEGRA),("c",BLANCA)]

class Partitura:
    def __init__(self, notasPartitura):
        self.partitura = notasPartitura.copy()

    def next(self):
        firstNota = self.partitura[0]
        self.partitura.pop(0)
        return firstNota
    
    def finish(self):
        return not self.partitura
    


def reproductor(partitura):
    kb = kbhit.KBHit()
    stream = sd.OutputStream(samplerate=SRATE, blocksize=CHUNK, channels=1)
    stream.start()
    osc = Osc(0, 0.5, 0)
    tiempoPasadoNota = 0
    duracionNota = 0
    while ((not partitura.finish()) or tiempoPasadoNota < duracionNota):
        if (tiempoPasadoNota >= duracionNota):
            nota = partitura.next()
            osc.setFreq(NOTAS[nota[0]])
            duracionNota = nota[1]
            tiempoPasadoNota = 0
        
        samples = osc.next()
        stream.write(samples)
        tiempoPasadoNota += CHUNK
    
    kb.quit()
    stream.stop()
    stream.close()
    return


#%%
part = Partitura(happyBirthday)
reproductor(part)