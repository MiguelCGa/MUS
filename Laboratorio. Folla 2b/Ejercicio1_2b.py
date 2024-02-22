# Diego López Balboa
# Miguel Curros García
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

class Nota:
    def __init__(self, freq):
        self.stream = sd.OutputStream(samplerate=SRATE, blocksize=CHUNK, channels=1)
        self.stream.start()
        self.osc = Osc(freq, 0)
    def __del__(self):
        self.stream.stop()
        self.stream.close()

    def next(self):
        self.stream.write(self.osc.next())
    def noteOn(self):
        self.osc.noteOn()
    def noteOff(self):
        self.osc.noteOff()


NOTAS_OSC = {
    "C": Nota(523.251),
    "D": Nota(587.3),
    "E": Nota(659.255),
    "F": Nota(698.456),
    "G": Nota(783.991),
    "A": Nota(880),
    "B": Nota(987.767),
    "c": Nota(523.251 * 2),
    "d": Nota(587.33 * 2),
    "e": Nota(659.255 * 2),
    "f": Nota(698.456 * 2),
    "g": Nota(783.991 * 2)
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
    tiempoPasadoNota = 0
    duracionNota = 0
    nota = "C"
    while ((not partitura.finish()) or tiempoPasadoNota < duracionNota):
        if (tiempoPasadoNota >= duracionNota):
            NOTAS_OSC[nota[0]].noteOff()
            nota = partitura.next()
            NOTAS_OSC[nota[0]].noteOn()
            duracionNota = nota[1]
            tiempoPasadoNota = 0
        for lNota in NOTAS_OSC:
            NOTAS_OSC[lNota].next()
        tiempoPasadoNota += CHUNK
    
    kb.quit()
    return


#%%
part = Partitura(happyBirthday)
reproductor(part)